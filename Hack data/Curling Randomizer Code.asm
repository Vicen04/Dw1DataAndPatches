This code makes the curling matches with penguinmon load a random digimon to play against. There's some extra changes not related to the code at the end of the file.


void CurlingInit(int isTutorial) // original
{
  // code skipped
  numNeutralStones = rand() % 3;
  // code skipped
}


void CurlingInit(int isTutorial) // only to show the change
{
  // code skipped
  numNeutralStones = RandomizePenguinmon() % 3;
  // code skipped
}


int FUN_80058de8(int currentTextLocation) // original 
{
   // code ignored

      if (cGpffff971c == 2) 
      {
        iVar2 = LoadPenguinmonCurlingText(currentTextLocation,currentTextValue);
        cVar3 = iVar2;
      }
      else
      {
        iVar2 = LoadMetalmamemonCurlingText(currentTextLocation,currentTextValue);
        cVar3 = iVar2;
      }

// code ignored
}


int FUN_80058de8(int currentTextLocation) // changed
{
   // code ignored

      if (cGpffff971c == 2) 
      {
        iVar2 = LoadCurlingTextBetter(currentTextLocation,currentTextValue);
        cVar3 = iVar2;
      }
      else
      {
        iVar2 = LoadCurlingTextBetter(currentTextLocation,currentTextValue);
        cVar3 = iVar2;
      }

// code ignored
}


// Originally located where the penguinmon load text was

int RandomizePenguinmon() // code to randomize the digimon that will appear in a match against penguinmon

{
  int iVar1;
  
  if (*OpponentDigimonValue == 57) // run the code if the current opponent loaded is penguinmon
  {
    iVar1 = ReturnRandom(114); // returns a random number between 0 and the value input

    if (iVar1 == 0) // if the tamer is choosen
      iVar1 = 115; // set the digimon to machinedramon
    
    if (iVar1 == 62) // if weregarurumon is choosen
      iVar1 = 117; // set the digimon to jijimon
    
    FUN_80106c08(57);  // Free the pointer and memory used by the current digimon 

    DAT_8013cb50 = iVar1; // set the value to a check that will be done later to the new digimon

    FUN_801069c0(iVar1); // read and set digimon model data

    FUN_801062f8(0xff); 

    FUN_800b6118(iVar1,0,0); // delete the old model and load the new one

    memcpy(ptr_penguinmonText, (DigimonNamesPointer + (*OpponentDigimonValue % 255) * 54),14); // Copy the name of the new digimon and overwrite penguinmon's name
  }
  iVar1 = rand();
  return iVar1;
}

//Fused both load text functions (penguinmon and metalmamemon) into one to have enough space to put the randomizer code, this one is located where the old metalmamemon load text function was

int CurlingTextBetter(int currentTextLocation,uint currentTextValue)  // currentTextLocation is used to determine which text should be loaded, currentTextValue is used to determine the line where it should be loaded

{
  char cVar1;
  uint uVar2;
  vector<int> textAddresses;
  vector<byte> textLocations;
  ptr pointerText;
  ptr pointerLoaction;
  int iVar7;
  
 
  if (cGpffff971c == 2) // checks if the opponent is penguinmon or not
    pointerText = &ptr_penguinmonText; // It is located at 8005af58 in the code
  
  else 
    pointerText = &ptr_metalmamemonText;  // It is located at 8005b318 in the code

  textAdresses = new vector<int>;
  for (int i = 0; i < 61; i++)
  {
   textAdresses.push_back(*pointerText); // value in that address
   pointerLocation = pointerText + 4;   // gets the next pointer
  }

  if (cGpffff971c == 2) // checks if the opponent is penguinmon or not
    pointerLocation = &ptr_penguinmonLocations; // It is located at 8005b04c in the code
  
  else 
    pointerLocation = &ptr_metalmamemonLocations; // It is located at 8005b40c in the code
  
  for (int i = 0; i < 10; i++)
  {
   textLocations.push_back(*pointerLocation); // value in that address
   pointerLocation = pointerLocation + 1;   // gets the next pointer
  }
 
  uVar2 = currentTextValue % 3;
  if (((int)currentTextValue < 0) && (currentTextValue != 0)) {
    uVar2 = uVar2 - 4;
  }
  if (uVar2 == 0) {
    SetTextColor(7); // SetTextColor(byte color) from sydMontague github
  }
  else {
    SetTextColor(1);
  }
  cVar1 = textLocations(currentTextLocation);
  RenderString(textAdresses[(int)cVar1 + currentTextValue],0,currentTextValue * 13 + 1); // RenderString(stringPtr, xPos, yPos) from sydMontague github
  iVar7 = (int)textLocations(currentTextLocation + 1) - (int)cVar1;
  if (iVar7 < 0) {
    iVar7 = iVar7 + 3;
  }
  return (int)(char)(iVar7 >> 2);
}           

        Offset       Hex         Command        
             
 
                             CurlingInit    // original

        800538a8 9f 44 02 0c     jal        0x8009127c  // Rand


                             CurlingInit    // changed

        800538a8 a3 67 01 0c     jal        0x80059e8c  // RandomizePenguinmon


                            FUN_80058de8
        80058ef0 a3 67 01 0c     jal        0x80059e8c   // LoadPenguinmonCurlingText                      

        80058f10 ea 67 01 0c     jal        0x80059fa8   // LoadMetalmamemonCurlingText    


                            FUN_80058de8
        80058ef0 de 67 01 0c     jal        0x80059f78   // LoadCurlingTextBetter                      

        80058f10 de 67 01 0c     jal        0x80059f78   // LoadCurlingTextBetter                  




                             RandomizePenguinmon 
                            
        80059e8c e0 fe bd 27     addiu      sp,sp,-0x120
        80059e90 1c 00 bf af     sw         ra,0x1c(sp)
        80059e94 08 00 a4 af     sw         a0,0x8(sp)
        80059e98 13 80 02 3c     lui        v0,0x8013
        80059e9c 4c f3 42 8c     lw         v0,-0xcb4(v0)
        80059ea0 00 00 00 00     nop
        80059ea4 00 00 42 8c     lw         v0,0x0(v0)
        80059ea8 00 00 00 00     nop
        80059eac 39 00 01 24     li         at,0x39
        80059eb0 2b 00 22 14     bne        at,v0,0x80059f60 // LAB_80059f60
        80059eb4 00 00 00 00     _nop
        80059eb8 b5 8d 02 0c     jal        0x800a36d4  // ReturnRandom                                   
        80059ebc 72 00 04 24     _li        a0,0x72
        80059ec0 02 00 02 14     bne        zero,v0,0x80059ecc // LAB_80059ecc
        80059ec4 3e 00 01 24     _li        at,0x3e
        80059ec8 73 00 02 24     li         v0,0x73
                             LAB_80059ecc                                  
        80059ecc 02 00 22 14     bne        at,v0,0x80059ed8 // LAB_80059ed8
        80059ed0 00 00 00 00     _nop
        80059ed4 75 00 02 24     li         v0,0x75
                             LAB_80059ed8                                    
        80059ed8 02 00 a2 a3     sb         v0,0x2(sp)
        80059edc 02 1b 04 0c     jal        0x80106c08    // FUN_80106c08                                  
        80059ee0 39 00 04 24     _li        a0,0x39
        80059ee4 02 00 a2 83     lb         v0,0x2(sp)
        80059ee8 14 80 04 3c     lui        a0,0x8014
        80059eec 50 cb 82 a0     sb         v0,-0x34b0(a0)
        80059ef0 70 1a 04 0c     jal        0x801069c0  //  FUN_801069c0                                   
        80059ef4 21 20 02 00     _move      a0,v0
        80059ef8 be 18 04 0c     jal        0x801062f8  //  FUN_801062f8                                
        80059efc ff 00 04 24     _li        a0,0xff
        80059f00 02 00 a4 83     lb         a0,0x2(sp)
        80059f04 21 30 00 00     clear      a2
        80059f08 46 d8 02 0c     jal        0x800b6118  //  FUN_800b6118                                 
        80059f0c 21 28 00 00     _clear     a1
        80059f10 08 00 a3 8f     lw         v1,0x8(sp)
        80059f14 13 80 02 3c     lui        v0,0x8013
        80059f18 4c f3 42 8c     lw         v0,-0xcb4(v0)
        80059f1c 00 00 00 00     nop
        80059f20 00 00 42 8c     lw         v0,0x0(v0)
        80059f24 06 80 04 3c     lui        a0,0x8006
        80059f28 d0 ab 84 24     addiu      a0,a0,-0x5430
        80059f2c ff 00 45 30     andi       a1,v0,0xff
        80059f30 40 10 05 00     sll        v0,a1,0x1
        80059f34 20 10 45 00     add        v0,v0,a1
        80059f38 80 10 02 00     sll        v0,v0,0x2
        80059f3c 20 10 45 00     add        v0,v0,a1
        80059f40 80 28 02 00     sll        a1,v0,0x2
        80059f44 13 80 02 3c     lui        v0,0x8013
        80059f48 b4 ce 42 24     addiu      v0,v0,-0x314c
        80059f4c 21 28 45 00     addu       a1,v0,a1
        80059f50 93 44 02 0c     jal        0x8009124c  // memcpy                                        
        80059f54 0e 00 06 24     _li        a2,0xe
        80059f58 08 00 a4 8f     lw         a0,0x8(sp)
        80059f5c 00 00 00 00     nop
                             LAB_80059f60                                     
        80059f60 9f 44 02 0c     jal        0x8009127c  // rand                                             
        80059f64 00 00 00 00     _nop
        80059f68 1c 00 bf 8f     lw         ra,0x1c(sp)
        80059f6c 00 00 00 00     nop
        80059f70 08 00 e0 03     jr         ra
        80059f74 20 01 bd 27     _addiu     sp,sp,0x120


                             LoadCurlingTextBetter                         

        80059f78 e0 fe bd 27     addiu      sp,sp,-0x120
        80059f7c 1c 00 bf af     sw         ra,0x1c(sp)
        80059f80 18 00 b2 af     sw         s2,0x18(sp)
        80059f84 14 00 b1 af     sw         s1,0x14(sp)
        80059f88 06 80 18 3c     lui        t8,0x8006
        80059f8c 10 00 b0 af     sw         s0,0x10(sp)
        80059f90 21 88 04 00     move       s1,a0
        80059f94 1c 97 82 93     lbu        v0,-0x68e4(gp)
        80059f98 02 00 01 24     li         at,0x2
        80059f9c 04 00 41 14     bne        v0,at,0x80059fb0 // LAB_80059fb0
        80059fa0 00 00 00 00     _nop
        80059fa4 58 af 18 27     addiu      t8,t8,-0x50a8
        80059fa8 02 00 00 10     b          0x80059fb4 //  LAB_80059fb4
        80059fac 00 00 00 00     _nop
                             LAB_80059fb0                                   
        80059fb0 18 b3 18 27     addiu      t8,t8,-0x4ce8
                             LAB_80059fb4                                   
        80059fb4 20 00 af 27     addiu      t7,sp,0x20
        80059fb8 3d 00 19 24     li         t9,0x3d
                             LAB_80059fbc                                  
        80059fbc 00 00 0e 8f     lw         t6,0x0(t8)
        80059fc0 ff ff 39 27     addiu      t9,t9,-0x1
        80059fc4 00 00 ee ad     sw         t6,0x0(t7)
        80059fc8 04 00 18 27     addiu      t8,t8,0x4
        80059fcc fb ff 20 1f     bgtz       t9,0x80059fbc // LAB_80059fbc
        80059fd0 04 00 ef 25     _addiu     t7,t7,0x4
        80059fd4 06 80 18 3c     lui        t8,0x8006
        80059fd8 04 00 41 14     bne        v0,at,0x80059fec // LAB_80059fec
        80059fdc 00 00 00 00     _nop
        80059fe0 4c b0 18 27     addiu      t8,t8,-0x4fb4
        80059fe4 02 00 00 10     b          0x80059ff0  // LAB_80059ff0
        80059fe8 00 00 00 00     _nop
                             LAB_80059fec                                   
        80059fec 0c b4 18 27     addiu      t8,t8,-0x4bf4
                             LAB_80059ff0                                  
        80059ff0 14 01 af 27     addiu      t7,sp,0x114
        80059ff4 0a 00 19 24     li         t9,0xa
                             LAB_80059ff8                                  
        80059ff8 00 00 0e 83     lb         t6,0x0(t8)  // DAT_8005b40c                        
        80059ffc ff ff 39 27     addiu      t9,t9,-0x1
        8005a000 00 00 ee a1     sb         t6,0x0(t7)
        8005a004 01 00 18 27     addiu      t8,t8,0x1
        8005a008 fb ff 20 1f     bgtz       t9,0x80059ff8 // LAB_80059ff8
        8005a00c 01 00 ef 25     _addiu     t7,t7,0x1
        8005a010 21 80 a0 00     move       s0,a1
        8005a014 04 00 a1 04     bgez       a1,0x8005a028 // LAB_8005a028
        8005a018 03 00 a2 30     _andi      v0,a1,0x3
        8005a01c 02 00 40 10     beq        v0,zero,0x8005a028 // LAB_8005a028
        8005a020 00 00 00 00     _nop
        8005a024 fc ff 42 24     addiu      v0,v0,-0x4
                             LAB_8005a028                                   
        8005a028 05 00 40 14     bne        v0,zero,0x8005a040  // LAB_8005a040
        8005a02c 00 00 00 00     _nop
        8005a030 03 33 04 0c     jal        0x8010cc0c   // SetTextColor                                 
        8005a034 07 00 04 24     _li        a0,0x7
        8005a038 04 00 00 10     b          0x8005a04c  // LAB_8005a04c
        8005a03c 21 10 b1 03     _addu      v0,sp,s1
                             LAB_8005a040                                   
        8005a040 03 33 04 0c     jal        0x8010cc0c   // SetTextColor                                  
        8005a044 01 00 04 24     _li        a0,0x1
        8005a048 21 10 b1 03     addu       v0,sp,s1
                             LAB_8005a04c                                    
        8005a04c 14 01 42 80     lb         v0,0x114(v0)
        8005a050 21 28 00 00     clear      a1
        8005a054 21 90 40 00     move       s2,v0
        8005a058 20 10 50 00     add        v0,v0,s0
        8005a05c 80 10 02 00     sll        v0,v0,0x2
        8005a060 21 18 a2 03     addu       v1,sp,v0
        8005a064 40 10 10 00     sll        v0,s0,0x1
        8005a068 20 10 50 00     add        v0,v0,s0
        8005a06c 80 10 02 00     sll        v0,v0,0x2
        8005a070 20 10 50 00     add        v0,v0,s0
        8005a074 20 00 64 8c     lw         a0,0x20(v1)
        8005a078 c9 33 04 0c     jal        0x8010cf24   // RenderString                                   
        8005a07c 01 00 46 20     _addi      a2,v0,0x1
        8005a080 01 00 22 22     addi       v0,s1,0x1
        8005a084 21 10 a2 03     addu       v0,sp,v0
        8005a088 14 01 42 80     lb         v0,0x114(v0)
        8005a08c 00 00 00 00     nop
        8005a090 22 10 52 00     sub        v0,v0,s2
        8005a094 03 00 41 04     bgez       v0,0x8005a0a4 // LAB_8005a0a4
        8005a098 83 c8 02 00     _sra       t9,v0,0x2
        8005a09c 03 00 42 24     addiu      v0,v0,0x3
        8005a0a0 83 c8 02 00     sra        t9,v0,0x2
                             LAB_8005a0a4                                   
        8005a0a4 00 16 19 00     sll        v0,t9,0x18
        8005a0a8 1c 00 bf 8f     lw         ra,0x1c(sp)
        8005a0ac 18 00 b2 8f     lw         s2,0x18(sp)
        8005a0b0 14 00 b1 8f     lw         s1,0x14(sp)
        8005a0b4 10 00 b0 8f     lw         s0,0x10(sp)
        8005a0b8 03 16 02 00     sra        v0,v0,0x18
        8005a0bc 08 00 e0 03     jr         ra
        8005a0c0 20 01 bd 27     _addiu     sp,sp,0x120



Miscellaneous change to put the text "You're not that great!" in the right line: 

14BB6D84 38 4A 13 80 to E8 AE 05 80
14BB6EB8 E8 AE 05 80 to 38 4A 13 80


Animations set so the digimon don't stay idle or break the game due to having an animation that moves them away from the starting point:

6342B0 00 00 00 00 to 52 36 00 00     botamon animation

6EB968 00 00 00 00 to 88 27 00 00     koromon animation
6EB970 00 00 00 00 to B4 3C 00 00     koromon animation

5CA170 00 00 00 00 to 0A 83 00 00     agumon animation
5CA174 00 00 00 00 to 60 BB 00 00     agumon animation

60C5D8 00 00 00 00 to 90 5A 00 00     betamon animation
60C5DC 00 00 00 00 to 18 A7 00 00     betamon animation
60C5E0 00 00 00 00 to AE 85 00 00     betamon animation

6ADD30 00 00 00 00 to 84 78 00 00     greymon animation
6ADD34 00 00 00 00 to F4 0F 01 00     greymon animation
6ADD38 00 00 00 00 to AC EF 00 00     greymon animation

64E6A0 5E 86 00 00 to 82 D0 00 00     devimon animation
64E6A4 00 00 00 00 to F4 B3 00 00     devimon animation

5DDF64 78 12 00 00 to 4C BA 00 00     airdramon animation
5DDF68 A8 76 00 00 to 00 00 00 00     airdramon animation
5DDF6C DC 7A 00 00 to 4A AF 00 00     airdramon animation

7BB310 B4 99 00 00 to F0 FF 00 00     tyrannomon animation
7BB318 00 00 00 00 to 00 DA 00 00     tyrannomon animation

70EBC0 00 00 00 00 to A4 EA 00 00     meramon animation
70EBC8 00 00 00 00 to B8 DD 00 00     meramon animation

78C22C 00 00 00 00 to 38 3F 00 00     seadramon animation
78C234 00 00 00 00 to 32 7C 00 00     seadramon animation

numemon has an animation

735ACC 34 7C 00 00 to 6A FC 00 00     metal greymon animation
735AD0 16 82 00 00 to 00 00 00 00     metal greymon animation
735AD4 00 00 00 00 to 02 CB 00 00     metal greymon animation

6F59E0 00 00 00 00 to 14 A3 00 00     mamemon animation

725164 00 00 00 00 to 68 70 00 00     monzaemon animation
72516C 00 00 00 00 to 6C 56 00 00     monzaemon animation

7832B8 00 00 00 00 to 7A 32 00 00     punimon animation

7B2840 00 00 00 00 to EE 17 00 00     tsunomon animation
7B2848 00 00 00 00 to 12 23 00 00     tsunomon animation

67E610 00 00 00 00 to 0A 83 00 00     gabumon animation
67E614 00 00 00 00 to 18 A5 00 00     gabumon animation

666F14 00 00 00 00 to F4 C4 00 00     elecmon animation
666F1C 00 00 00 00 to 28 B2 00 00     elecmon animation

6D467C 00 00 00 00 to 58 F6 00 00     kabuterimon animation
6D4684 00 00 00 00 to EE 08 01 00     kabuterimon animation

5F2F38 DA 75 00 00 to 86 F4 00 00     angemon animation
5F2F3C 54 7D 00 00 to 00 00 00 00     angemon animation
5F2F40 00 00 00 00 to C4 D3 00 00     angemon animation 

61FA5C 0E 89 00 00 to 68 B8 00 00     birdramon animation

693414 00 00 00 00 to AC DA 00 00     garurumon animation
69341C 00 00 00 00 to BC C7 00 00     garurumon animation

7F4F0C 00 00 00 00 to 94 A5 00 00     frigimon animation
7F4F14 00 00 00 00 to EC 7B 00 00     frigimon animation

6C6C04 00 00 00 00 to 10 5C 00 00     whamon animation
6C6C0C 00 00 00 00 to BA 47 00 00     whamon animation

7E31C8 00 00 00 00 to 24 97 00 00     vegiemon animation
7E31CC 00 00 00 00 to 8E 77 00 00     vegiemon animation

79EDC4 28 75 00 00 to EC BE 00 00     skullgreymon animation
79EDC8 00 00 00 00 to 8A 9B 00 00     skullgreymon animation

(change to fit penguinmon curling animations, it does not affect the metal mamemon NPC neither its curling)
74EFDC 94 6C 00 00 to B4 7F 00 00     metal mamemon animation 
74EFE0 68 74 00 00 to D8 81 00 00     metal mamemon animation 
74EFE4 B4 7F 00 00 to FA 84 00 00     metal mamemon animation 

7D4404 00 00 00 00 to 6A 7B 00 00     vademon animation

77CF70 00 00 00 00 to 68 31 00 00     poyomon animation

A74260 00 00 00 00 to 96 3D 00 00     tokomon animation
A74268 00 00 00 00 to B8 2A 00 00     tokomon animation

9CE308 00 00 00 00 to A2 D7 00 00     patamon animation
9CE310 00 00 00 00 to 52 BD 00 00     patamon animation

8FD3A0 FA 80 00 00 to BE D7 00 00     kunemon animation
8FD3A4 2C 85 00 00 to 00 00 00 00     kunemon animation
8FD3A8 92 87 00 00 to E2 C3 00 00     kunemon animation

A803FC 00 00 00 00 to E0 E3 00 00     unimon animation
A80404 00 00 00 00 to 66 C2 00 00     unimon animation

9A60E4 00 00 00 00 to 26 BE 00 00     ogremon animation
9A60EC 00 00 00 00 to AA C2 00 00     ogremon animation

A40490 00 00 00 00 to 7C 9F 00 00     shellmon animation
A40498 00 00 00 00 to 60 76 00 00     shellmon animation

839E18 00 00 00 00 to 78 0D 01 00     centarumon animation
839E20 00 00 00 00 to 72 B0 00 00     centarumon animation

81FECC 38 79 00 00 to C8 00 01 00     bakemon animation
81FED0 36 7A 00 00 to 00 00 00 00     bakemon animation
81FED0 00 00 00 00 to A8 B3 00 00     bakemon animation

8879A0 5A 65 00 00 to 20 E1 00 00     drimogemon animation
8879A8 00 00 00 00 to C6 AE 00 00     drimogemon animation

sukamon has animations

808884 00 00 00 00 to 4A FB 00 00     andromon animation
808888 00 00 00 00 to 68 C2 00 00     andromon animation

8BB4D0 16 34 00 00 to 92 7F 00 00     giromon animation
8BB4D4 50 3A 00 00 to 00 00 00 00     giromon animation
8BB4D8 00 00 00 00 to 9A 71 00 00     giromon animation

8A1320 00 00 00 00 to FA DE 00 00     etemon animation
8A1328 00 00 00 00 to 18 04 01 00     etemon animation

A96E98 00 00 00 00 to AA 3C 00 00     yuramon animation

A681CC 00 00 00 00 to 92 47 00 00     tanemon animation
A681D4 00 00 00 00 to 30 72 00 00     tanemon animation

A15248 00 00 00 00 to 36 A4 00 00     biyomon animation 
A1524C 00 00 00 00 to DE 9C 00 00     biyomon animation 

9BA8DC 00 00 00 00 to D0 B3 00 00     palmon animation
9BA8E4 00 00 00 00 to 1A A0 00 00     palmon animation

975930 4E A8 00 00 to 2A F1 00 00     monochromon animation

93002C 84 72 00 00 to DE 7C 00 00     leomon animation
930030 DE 7C 00 00 to DA D7 00 00     leomon animation
930034 00 00 00 00 to 7C BC 00 00     leomon animation

A52790 A6 79 00 00 to F8 C8 00 00     coelamon animation
A52798 00 00 00 00 to 2A DD 00 00     coelamon animation

854E7C 00 00 00 00 to 34 E4 00 00     kokatorimon animation
854E84 00 00 00 00 to 28 C9 00 00     kokatorimon animation

914510 00 00 00 00 to DE C6 00 00     kuwagamon animation
914518 00 00 00 00 to 06 E7 00 00     kuwagamon animation

mojyamon has animations 

9900E0 00 00 00 00 to C2 97 00 00     nanimon animation

947DA4 00 00 00 00 to 0A C1 00 00     megadramon animation
947DAC 00 00 00 00 to 46 7A 00 00     megadramon animation

9FFBF4 00 00 00 00 to 2A B3 00 00     piximon animation
9FFBFC 00 00 00 00 to 78 7E 00 00     piximon animation

87046C 00 00 00 00 to 36 D8 00 00     digitamamon animation
870474 00 00 00 00 to 48 C8 00 00     digitamamon animation

penguinmon does not need animations

8E6DC0 00 00 00 00 to 10 B7 00 00     ninjamon animation
8E6DC8 00 00 00 00 to 14 A4 00 00     ninjamon animation

8CB924 00 00 00 00 to AA 0F 01 00     phoenixmon animation
8CB92C 00 00 00 00 to F6 E0 00 00     phoenixmon animation

B17058 00 00 00 00 to C0 CE 00 00     H-kabuterimon animation
B17060 00 00 00 00 to 0E DA 00 00     H-kabuterimon animation

B4C01C 00 00 00 00 to 64 9C 00 00     megaseadramon animation
B4C024 00 00 00 00 to 94 96 00 00     megaseadramon animation

weregarurumon does not exist

B94B3C 00 00 00 00 to F8 C6 00 00     panjamon animation
B94B44 00 00 00 00 to 9A AB 00 00     panjamon animation

AE5224 00 00 00 00 to 0A C1 00 00     gigadramon animation
AE522C 00 00 00 00 to 46 7A 00 00     gigadramon animation

B6A6F8 00 00 00 00 to 48 D9 00 00     metal etemon animation
B6A700 00 00 00 00 to 66 FE 00 00     metal etemon animation

C119E0 00 00 00 00 to 78 79 00 00     myotismon animation
C119E8 00 00 00 00 to 36 5E 00 00     myotismon animation

C3EAB0 00 00 00 00 to 70 88 00 00     yanmamon animation
C3EAB8 00 00 00 00 to DE 9A 00 00     yanmamon animation

B0A1DC 00 00 00 00 to 4A 3C 00 00     gotsumon animation

ABB744 00 00 00 00 to 0E 59 00 00     flarerizamon animation
ABB74C 00 00 00 00 to C6 44 00 00     flarerizamon animation

C32884 00 00 00 00 to 74 49 00 00     warumonzaemon animation
C329B8 00 00 00 00 to 74 49 00 00     warumonzaemon animation
C329BC 00 00 00 00 to CA 40 00 00     warumonzaemon animation

C51A3C 00 00 00 00 to 32 45 00 00     snow agumon animation
C51A44 00 00 00 00 to BA 4A 00 00     snow agumon animation

B319F4 00 00 00 00 to A6 60 00 00     hyogamon animation
B319FC 00 00 00 00 to 24 4D 00 00     hyogamon animation

BAB578 00 00 00 00 to E8 4A 00 00     platinum sukamon animation

AAE008 00 00 00 00 to 36 57 00 00     dokunemon animation
AAE010 00 00 00 00 to A4 48 00 00     dokunemon animation

BDE7EC 00 00 00 00 to FE 60 00 00     shinma unimon animation
BDE7F4 00 00 00 00 to A2 59 00 00     shinma unimon animation

BEE530 00 00 00 00 to F4 2F 00 00     tankmon animation
BEE538 00 00 00 00 to 08 34 00 00     tankmon animation

BD25AC 00 00 00 00 to 60 52 00 00     red vegiemon animation
BD25B4 00 00 00 00 to E2 36 00 00     red vegiemon animation

B3F67C 00 00 00 00 to E0 52 00 00     J-mojyamon animation
B3F684 00 00 00 00 to 7A 37 00 00     J-mojyamon animation

B859C0 00 00 00 00 to 7A 6D 00 00     nisedrimogemon animation
B859C8 00 00 00 00 to 94 4A 00 00     nisedrimogemon animation

AFBB80 00 00 00 00 to E6 58 00 00     goburimon animation
AFBB88 00 00 00 00 to F6 4C 00 00     goburimon animation

C04D9C 00 00 00 00 to C8 48 00 00     mud frigimon animation
C04DA4 00 00 00 00 to C0 36 00 00     mud frigimon animation

BC73CC 00 00 00 00 to 6E 50 00 00     psychemon animation
BC73D4 00 00 00 00 to 52 36 00 00     psychemon animation

B5E698 00 00 00 00 to 94 44 00 00     modokibetamon animation
B5E6A0 00 00 00 00 to E2 33 00 00     modokibetamon animation

BF8BA4 00 00 00 00 to 5E 5F 00 00     toy agumon animation
BF8BAC 00 00 00 00 to 3E 58 00 00     toy agumon animation

BB7598 00 00 00 00 to AA 71 00 00     piddomon animation
BB759C 00 00 00 00 to 10 7A 00 00     piddomon animation

AA163C 00 00 00 00 to 52 50 00 00     aruraumon animation
AA1644 00 00 00 00 to 5A 48 00 00     aruraumon animation

AD5F2C 00 00 00 00 to 78 70 00 00     geremon animation

C23C98 00 00 00 00 to A8 5D 00 00     vermilimon animation
C23CA0 00 00 00 00 to F8 65 00 00     vermilimon animation

AC8BE0 00 00 00 00 to 36 48 00 00     fugamon animation
AC8BE4 00 00 00 00 to 2A 51 00 00     fugamon animation

DA01C4 00 00 00 00 to 56 27 00 00     tekkamon animation
DA01CC 00 00 00 00 to DA 1B 00 00     tekkamon animation

D16190 00 00 00 00 to A8 42 00 00     morishellmon animation
D16198 00 00 00 00 to A2 38 00 00     morishellmon animation

C9C808 00 00 00 00 to 6A 76 00 00     guardromon animation

D08924 00 00 00 00 to C0 5B 00 00     muchomon animation
D08928 00 00 00 00 to C0 5B 00 00     muchomon animation
D0892C 00 00 00 00 to EE 4A 00 00     muchomon animation

CE5914 00 00 00 00 to A8 41 00 00     icemon animation
CE5918 00 00 00 00 to A8 41 00 00     icemon animation
CE591C 00 00 00 00 to 4A 3C 00 00     icemon animation

C5F12C 00 00 00 00 to 70 61 00 00     akatorimon animation
C5F134 00 00 00 00 to 76 50 00 00     akatorimon animation

DB70D8 00 00 00 00 to 26 64 00 00     tsukaimon animation
DB70E0 00 00 00 00 to D6 49 00 00     tsukaimon animation

D4F4A8 00 00 00 00 to DE 58 00 00     sharmamon animation
D4F4B0 00 00 00 00 to EE 4C 00 00     sharmamon animation

C81644 00 00 00 00 to 5E 5F 00 00     clear agumon animation
C8164C 00 00 00 00 to 3E 58 00 00     clear agumon animation

DD042C 00 00 00 00 to 60 52 00 00     weedmon animation
DD0434 00 00 00 00 to E2 36 00 00     weedmon animation

CD842C 00 00 00 00 to 50 57 00 00     ice devimon animation
CD8434 00 00 00 00 to EE 46 00 00     ice devimon animation

C8EDE4 00 00 00 00 to 94 4B 00 00     darkizamon animation
C8EDEC 00 00 00 00 to DA 57 00 00     darkizamon animation

D5CA10 00 00 00 00 to CA 7B 00 00     sand yanmamon animation
D5CA14 00 00 00 00 to CA 7B 00 00     sand yanmamon animation
D5CA18 00 00 00 00 to BE 71 00 00     sand yanmamon animation

D6EC48 00 00 00 00 to 68 4C 00 00     snow goburimon animation
D6EC50 00 00 00 00 to 7C 3F 00 00     snow goburimon animation

C74440 00 00 00 00 to 80 40 00 00     blue meramon animation
C74448 00 00 00 00 to 34 55 00 00     blue meramon animation

CC7CDC 00 00 00 00 to 30 65 00 00     gururumon animation
CC7CE4 00 00 00 00 to 40 52 00 00     gururumon animation

D42CF4 00 00 00 00 to 32 54 00 00     saberdramon animation
D42CFC 00 00 00 00 to 68 3B 00 00     saberdramon animation

D7B214 00 00 00 00 to CA 5F 00 00     soulmon animation
D7B218 00 00 00 00 to AA 70 00 00     soulmon animation
D7B21C 00 00 00 00 to CE 50 00 00     soulmon animation

CBBE94 00 00 00 00 to 2C 50 00 00     rockmon animation
CBBE9C 00 00 00 00 to C4 36 00 00     rockmon animation

Otamamon has animations

CAC6EC 00 00 00 00 to EE 6A 00 00     gekomon animation
CAC6F4 00 00 00 00 to 8C 4B 00 00     gekomon animation

D93424 00 00 00 00 to FC 7A 00 00     tentomon animation
D9342C 00 00 00 00 to 26 5E 00 00     tentomon animation

DC684C 00 00 00 00 to A6 31 00 00     waru seadramon animation
DC6854 00 00 00 00 to BC 3F 00 00     waru seadramon animation

Meteoromon has animations

D25F24 00 00 00 00 to 00 85 00 00     machinedramon animation
D25F28 00 00 00 00 to BC 51 00 00     machinedramon animation

jijimon has animations

