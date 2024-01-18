void InitCurling(int isTutorial) // original
{
  // code skipped
  numNeutralStones = rand() % 3;
  // code skipped
}


void InitCurling(int isTutorial) // only to show the change
{
  // code skipped
  numNeutralStones = RandomizeCurling() % 3;
  // code skipped
}


int FUN_8005a6f8(int currentTextLocation) // original 
{
   // code ignored

      if (DAT_8013e4c0 == 2)       
        cVar3 = LoadPenguinmonCurlingText(currentTextLocation,currentTextValue);
      
      else
        cVar3 = LoadMetalmamemonCurlingText(currentTextLocation,currentTextValue);
      

// code ignored
}


int FUN_8005a6f8(int currentTextLocation) // changed
{
   // code ignored

      if (DAT_8013e4c0 == 2) 
        cVar3 = LoadCurlingTextBetter(currentTextLocation,currentTextValue);
      
      else      
        cVar3 = LoadCurlingTextBetter(currentTextLocation,currentTextValue);
      

// code ignored
}


// Originally located where the penguinmon load text was

int RandomizeCurling() // code to randomize the digimon that will appear in a match against penguinmon

{
  int iVar1;
  
  if (*OpponentDigimonValue != 149) // run the code if the current opponent loaded is not MetalMamemon
  {
    iVar1 = ReturnRandom(114); // returns a random number between 0 and the value input (the value input is not included)

    if (iVar1 == 0) // if the tamer is choosen
      iVar1 = 115; // set the digimon to machinedramon
    
    if (iVar1 == 62)  // if weregarurumon is choosen
    {
     iVar1 = ReturnRandom(4); // get a random value between 0 and 3
     iVar1 = 117 + iVar1; //Set the digimon, it can be either Jijimon, Market Manager, Shogun Gekkomon or King Sukamon
    }        
    

    DAT_80145608 = iVar1; // set the value of the first digimon of the map data to the new digimon

    LoadDigimonJP(iVar1); // read and set the digimon model data

    FUN_800f0ae8(0xff); 

    SetDigimonInMapJP(iVar1,0,0); // load the new model and place it in the map

    memcpy(ptr_penguinmonNameTextJP, (DigimonNamesPointerJP + (iVar1 % 255) * 54),10); // Copy the name of the new digimon and overwrite penguinmon's name, this version has a lower character limit
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
  ptr extraText1;
  ptr extraText2;
  int iVar3;
  
 
  if (DAT_8013e4c0 == 2) // checks if the opponent is penguinmon or not
    pointerText = &ptr_penguinmonText; // It is located at 8005d3cc in the code
  
  else 
    pointerText = &ptr_metalMamemonText;  // It is located at 8005d7c8 in the code

  textAdresses = new vector<int>;

  for (int i = 0; i < 61; i++)
  {
    textAdresses.push_back(*pointerText); // value in that address
    pointerLocation = pointerText + 4;   // gets the next pointer
  }

  if (DAT_8013e4c0 == 2) // checks if the opponent is penguinmon or not
  {    
    pointerLocation = &ptr_penguinmonLocations; // It is located at 8005d4dc in the code
    extraText2 = &circleTextPenguinmon;
  }
  
  else 
  {
    pointerLocation = &ptr_metalMamemonLocations; // It is located at 8005d8cc in the code    
    extraText2 = &circleTextMetalMamemon;
  }
  
  extraText1 = &circleText;

  textLocations = new vector<byte>;

  for (int i = 0; i < 10; i++)
  {
    textLocations.push_back(*pointerLocation); // value in that address
    pointerLocation = pointerLocation + 1;   // gets the next pointer
  }
 
  uVar2 = currentTextValue % 3;

  if (((int)currentTextValue < 0) && (currentTextValue != 0)) 
    uVar2 = uVar2 - 4;
  
  if (uVar2 == 0) 
    SetTextColor(7); // SetTextColor(byte color)
  
  else 
    SetTextColor(1);
  

  cVar1 = textLocations(currentTextLocation);
  RenderString(textAdresses[(int)cVar1 + currentTextValue],0,currentTextValue * 13 + 1); // RenderString(stringPtr, initialXPos, yPos)

  if ((currentTextLocation == 1) && (param_2 == 7)) 
  {
    SetTextColor(10);
    RenderString(extraText1,0,0x5c);
  }
  if ((currentTextLocation == 2) && (param_2 == 3))
  {
    SetTextColor(10);
    RenderString(extraText2,0,0x28);
  }

  iVar3 = (int)textLocations(currentTextLocation + 1) - (int)cVar1;
  if (iVar3 < 0) {
    iVar3 = iVar3 + 3;
  }
  return (int)(char)(iVar3 >> 2);
}           



                             InitCurling
Original:

        80053898 20 45 02 0c     jal        0x80091480  //Rand  

Changed:

        80053898 5d 6f 01 0c     jal        0x8005bd74  //RandomizeCurling                              



                             FUN_8005a6f8
Original:

        8005a81c 5d 6f 01 0c     jal        0x8005bd74  //LoadPenguinmonCurlingText                                 


        8005a848 d5 6f 01 0c     jal        0x8005bf54  //LoadMetalMamemonCurlingText                                        

Changed:
        8005a81c c1 6f 01 0c     jal        0x8005bf04  //LoadTextBetterJP       

                        
        8005a848 c1 6f 01 0c     jal        0x8005bf04  //LoadTextBetterJP                                



                             RandomizeCurling                              
        8005bd74 d0 ff bd 27     addiu      sp,sp,-0x30
        8005bd78 1c 00 bf af     sw         ra,0x1c(sp)
        8005bd7c 08 00 a4 af     sw         a0,0x8(sp)
        8005bd80 14 80 02 3c     lui        v0,0x8014
        8005bd84 bc cd 42 8c     lw         v0,-0x3244(v0) //DAT_8013cdbc
        8005bd88 00 00 00 00     nop
        8005bd8c 00 00 42 8c     lw         v0,0x0(v0)
        8005bd90 00 00 00 00     nop
        8005bd94 95 00 01 24     li         at,0x95
        8005bd98 27 00 22 10     beq        at,v0,0x8005be38
        8005bd9c 00 00 00 00     _nop
        8005bda0 9d 92 02 0c     jal        0x800a4a74 //ReturnRandom                                    
        8005bda4 72 00 04 24     _li        a0,0x72
        8005bda8 02 00 02 14     bne        zero,v0,0x8005bdb4
        8005bdac 00 00 00 00     _nop
        8005bdb0 73 00 02 24     li         v0,0x73
                             LAB_8005bdb4                                  
        8005bdb4 3e 00 01 24     li         at,0x3e
        8005bdb8 00 00 00 00     nop
        8005bdbc 04 00 22 14     bne        at,v0,0x8005bdd0
        8005bdc0 00 00 00 00     _nop
        8005bdc4 9d 92 02 0c     jal        0x800a4a74 //ReturnRandom                                  
        8005bdc8 04 00 04 24     _li        a0,0x4
        8005bdcc 75 00 42 24     addiu      v0,v0,0x75
                             LAB_8005bdd0                                  
        8005bdd0 10 00 a2 a3     sb         v0,0x10(sp)
        8005bdd4 14 80 04 3c     lui        a0,0x8014
        8005bdd8 08 56 82 a0     sb         v0,0x5608(a0) //DAT_80145608                    
        8005bddc 00 00 00 00     nop
        8005bde0 64 c5 03 0c     jal        0x800f1590 //LoadDigimonJP                                   
        8005bde4 21 20 02 00     _move      a0,v0
        8005bde8 ba c2 03 0c     jal        0x800f0ae8 //FUN_800f0ae8                                  
        8005bdec ff 00 04 24     _li        a0,0xff
        8005bdf0 10 00 a4 83     lb         a0,0x10(sp)
        8005bdf4 21 30 00 00     clear      a2
        8005bdf8 51 ed 02 0c     jal        0x800bb544 //SetDigimonInMapJP                                  
        8005bdfc 21 28 00 00     _clear     a1
        8005be00 10 00 a2 83     lb         v0,0x10(sp)
        8005be04 06 80 04 3c     lui        a0,0x8006
        8005be08 e8 cf 84 24     addiu      a0,a0,-0x3018  //ptr_penguinmonNameText                  
        8005be0c ff 00 45 30     andi       a1,v0,0xff
        8005be10 40 10 05 00     sll        v0,a1,0x1
        8005be14 20 10 45 00     add        v0,v0,a1
        8005be18 80 10 02 00     sll        v0,v0,0x2
        8005be1c 20 10 45 00     add        v0,v0,a1
        8005be20 80 28 02 00     sll        a1,v0,0x2
        8005be24 14 80 02 3c     lui        v0,0x8014
        8005be28 24 a9 42 24     addiu      v0,v0,-0x56dc
        8005be2c 21 28 45 00     addu       a1,v0,a1
        8005be30 14 45 02 0c     jal        0x80091450 //memcpy                                           
        8005be34 0a 00 06 24     _li        a2,0xa
                             LAB_8005be38 
        8005be38 20 45 02 0c     jal        0x80091480 	//rand                                           
        8005be3c 00 00 00 00     _nop
        8005be40 08 00 a4 8f     lw         a0,0x8(sp)
        8005be44 1c 00 bf 8f     lw         ra,0x1c(sp)                                                             
        8005be48 00 00 00 00     nop
        8005be4c 08 00 e0 03     jr         ra
        8005be50 30 00 bd 27     _addiu     sp,sp,0x30



//This is just leftover commands from all the code rewritten, it can repurposed to do something else
        8005be54 00 00 00 00     nop
        8005be58 00 00 00 00     nop
        8005be5c 00 00 00 00     nop
        8005be60 00 00 00 00     nop
        8005be64 00 00 00 00     nop
        8005be68 00 00 00 00     nop
        8005be6c 00 00 00 00     nop
        8005be70 00 00 00 00     nop
        8005be74 00 00 00 00     nop
        8005be78 00 00 00 00     nop
        8005be7c 00 00 00 00     nop
        8005be80 00 00 00 00     nop
        8005be84 00 00 00 00     nop
        8005be88 00 00 00 00     nop
        8005be8c 00 00 00 00     nop
        8005be90 00 00 00 00     nop
        8005be94 00 00 00 00     nop
        8005be98 00 00 00 00     nop
        8005be9c 00 00 00 00     nop
        8005bea0 00 00 00 00     nop
        8005bea4 00 00 00 00     nop
        8005bea8 00 00 00 00     nop
        8005beac 00 00 00 00     nop
        8005beb0 00 00 00 00     nop
        8005beb4 00 00 00 00     nop
        8005beb8 00 00 00 00     nop
        8005bebc 00 00 00 00     nop
        8005bec0 00 00 00 00     nop
        8005bec4 00 00 00 00     nop
        8005bec8 00 00 00 00     nop
        8005becc 00 00 00 00     nop
        8005bed0 00 00 00 00     nop
        8005bed4 00 00 00 00     nop
        8005bed8 00 00 00 00     nop
        8005bedc 00 00 00 00     nop
        8005bee0 00 00 00 00     nop
        8005bee4 00 00 00 00     nop
        8005bee8 00 00 00 00     nop
        8005beec 00 00 00 00     nop
        8005bef0 00 00 00 00     nop
        8005bef4 00 00 00 00     nop
        8005bef8 00 00 00 00     nop
        8005befc 00 00 00 00     nop
        8005bf00 00 00 00 00     nop
                            



                             LoadTextBetterJP 
                            
        8005bf04 d8 fe bd 27     addiu      sp,sp,-0x128
        8005bf08 18 00 bf af     sw         ra,0x18(sp)
        8005bf0c 14 00 b1 af     sw         s1,0x14(sp)
        8005bf10 10 00 b0 af     sw         s0,0x10(sp)
        8005bf14 21 88 80 00     move       s1,a0
        8005bf18 21 80 a0 00     move       s0,a1
        8005bf1c 06 80 18 3c     lui        t8,0x8006
        8005bf20 b4 96 82 93     lbu        v0,-0x694c(gp) //DAT_8013e4c0
        8005bf24 02 00 01 24     li         at,0x2
        8005bf28 04 00 41 14     bne        v0,at,0x8005bf3c
        8005bf2c 00 00 00 00     _nop
        8005bf30 cc d3 18 27     addiu      t8,t8,-0x2c34 //ptr_penguinmonText
        8005bf34 02 00 00 10     b          0x8005bf40
        8005bf38 00 00 00 00     _nop
                             LAB_8005bf3c                                   
        8005bf3c c8 d7 18 27     addiu      t8,t8,-0x2838 //ptr_metalMamemonText  
                             LAB_8005bf40                                   
        8005bf40 20 00 af 27     addiu      t7,sp,0x20
        8005bf44 3d 00 19 24     li         t9,0x3d
                             LAB_8005bf48                                   
        8005bf48 00 00 0e 8f     lw         t6,0x0(t8)                   
        8005bf4c 04 00 18 27     addiu      t8,t8,0x4
        8005bf50 ff ff 39 27     addiu      t9,t9,-0x1
        8005bf54 00 00 ee ad     sw         t6,0x0(t7)           
        8005bf58 04 00 ef 25     addiu      t7,t7,0x4
        8005bf5c fa ff 20 1f     bgtz       t9,0x8005bf48
        8005bf60 00 00 00 00     _nop
        8005bf64 b4 96 82 93     lbu        v0,-0x694c(gp) //DAT_8013e4c0                    
        8005bf68 02 00 01 24     li         at,0x2
        8005bf6c 0a 00 41 14     bne        v0,at,0x8005bf98
        8005bf70 00 00 00 00     _nop
        8005bf74 44 8e 82 27     addiu      v0,gp,-0x71bc
        8005bf78 00 00 43 8c     lw         v1,0x0(v0) //CircleText                     
        8005bf7c 04 00 42 8c     lw         v0,0x4(v0) //CircleTextPenguinmon                    
        8005bf80 14 01 a3 af     sw         v1,0x114(sp) //ExtraText1                  
        8005bf84 18 01 a2 af     sw         v0,0x118(sp) //ExtraText2                    
        8005bf88 06 80 18 3c     lui        t8,0x8006
        8005bf8c dc d4 18 27     addiu      t8,t8,-0x2b24 //ptr_penguinmonLocations
        8005bf90 08 00 00 10     b          0x8005bfb4
        8005bf94 00 00 00 00     _nop
                             LAB_8005bf98                                    
        8005bf98 4c 8e 82 27     addiu      v0,gp,-0x71b4
        8005bf9c 00 00 43 8c     lw         v1,0x0(v0) //CircleText                        
        8005bfa0 04 00 42 8c     lw         v0,0x4(v0) //CircleTextMetalMamemon                    
        8005bfa4 14 01 a3 af     sw         v1,0x114(sp) //ExtraText1                 
        8005bfa8 18 01 a2 af     sw         v0,0x118(sp) //ExtraText2                 
        8005bfac 06 80 18 3c     lui        t8,0x8006
        8005bfb0 cc d8 18 27     addiu      t8,t8,-0x2734  //ptr_metalMamemonLocations
                             LAB_8005bfb4                                 
        8005bfb4 1c 01 af 27     addiu      t7,sp,0x11c 
        8005bfb8 0a 00 19 24     li         t9,0xa
                             LAB_8005bfbc                                   
        8005bfbc 00 00 0e 83     lb         t6,0x0(t8)                     
        8005bfc0 01 00 18 27     addiu      t8,t8,0x1
        8005bfc4 ff ff 39 27     addiu      t9,t9,-0x1
        8005bfc8 00 00 ee a1     sb         t6,0x0(t7)
        8005bfcc 01 00 ef 25     addiu      t7,t7,0x1
        8005bfd0 fa ff 20 1f     bgtz       t9,0x8005bfbc
        8005bfd4 00 00 00 00     _nop
        8005bfd8 03 00 02 32     andi       v0,s0,0x3
        8005bfdc 04 00 01 06     bgez       s0,0x8005bff0
        8005bfe0 00 00 00 00     _nop
        8005bfe4 02 00 40 10     beq        v0,zero,0x8005bff0
        8005bfe8 00 00 00 00     _nop
        8005bfec fc ff 42 24     addiu      v0,v0,-0x4
                             LAB_8005bff0                                   
        8005bff0 06 00 40 14     bne        v0,zero,0x8005c00c
        8005bff4 00 00 00 00     _nop
        8005bff8 07 00 04 24     li         a0,0x7
        8005bffc ca 95 02 0c     jal        0x800a5728 //SetTextColor                                   
        8005c000 00 00 00 00     _nop
        8005c004 04 00 00 10     b          0x8005c018
        8005c008 00 00 00 00     _nop
                             LAB_8005c00c                                   
        8005c00c 01 00 04 24     li         a0,0x1
        8005c010 ca 95 02 0c     jal        0x800a5728 //SetTextColor                                   
        8005c014 00 00 00 00     _nop
                             LAB_8005c018                                  
        8005c018 21 10 b1 03     addu       v0,sp,s1
        8005c01c 1c 01 42 80     lb         v0,0x11c(v0)
        8005c020 00 00 00 00     nop
        8005c024 20 10 50 00     add        v0,v0,s0
        8005c028 80 10 02 00     sll        v0,v0,0x2
        8005c02c 21 18 a2 03     addu       v1,sp,v0
        8005c030 40 10 10 00     sll        v0,s0,0x1
        8005c034 20 10 50 00     add        v0,v0,s0
        8005c038 80 10 02 00     sll        v0,v0,0x2
        8005c03c 20 10 50 00     add        v0,v0,s0
        8005c040 01 00 46 20     addi       a2,v0,0x1
        8005c044 20 00 64 8c     lw         a0,0x20(v1)
        8005c048 21 28 00 00     clear      a1
        8005c04c d2 96 02 0c     jal        0x800a5728 //RenderString                                    
        8005c050 00 00 00 00     _nop
        8005c054 01 00 01 24     li         at,0x1
        8005c058 10 00 21 16     bne        s1,at,0x8005c09c
        8005c05c 00 00 00 00     _nop
        8005c060 07 00 01 24     li         at,0x7
        8005c064 0d 00 01 16     bne        s0,at,0x8005c09c
        8005c068 00 00 00 00     _nop
        8005c06c 0a 00 04 24     li         a0,0xa
        8005c070 ca 95 02 0c     jal        0x800a5728 //SetTextColor                                    
        8005c074 00 00 00 00     _nop
        8005c078 40 10 10 00     sll        v0,s0,0x1
        8005c07c 20 10 50 00     add        v0,v0,s0
        8005c080 80 10 02 00     sll        v0,v0,0x2
        8005c084 20 10 50 00     add        v0,v0,s0
        8005c088 01 00 46 20     addi       a2,v0,0x1
        8005c08c 14 01 a4 8f     lw         a0,0x114(sp) //ExtraText1                 
        8005c090 21 28 00 00     clear      a1
        8005c094 d2 96 02 0c     jal        0x800a5728 //RenderString                                     
        8005c098 00 00 00 00     _nop
                             LAB_8005c09c                                  
        8005c09c 02 00 01 24     li         at,0x2
        8005c0a0 10 00 21 16     bne        s1,at,0x8005c0e4
        8005c0a4 00 00 00 00     _nop
        8005c0a8 03 00 01 24     li         at,0x3
        8005c0ac 0d 00 01 16     bne        s0,at,0x8005c0e4
        8005c0b0 00 00 00 00     _nop
        8005c0b4 0a 00 04 24     li         a0,0xa
        8005c0b8 ca 95 02 0c     jal        0x800a5728 //SetTextColor                                   
        8005c0bc 00 00 00 00     _nop
        8005c0c0 40 10 10 00     sll        v0,s0,0x1
        8005c0c4 20 10 50 00     add        v0,v0,s0
        8005c0c8 80 10 02 00     sll        v0,v0,0x2
        8005c0cc 20 10 50 00     add        v0,v0,s0
        8005c0d0 01 00 46 20     addi       a2,v0,0x1
        8005c0d4 18 01 a4 8f     lw         a0,0x118(sp) //ExtraText2              
        8005c0d8 21 28 00 00     clear      a1
        8005c0dc d2 96 02 0c     jal        0x800a5728 //RenderString                                  
        8005c0e0 00 00 00 00     _nop
                             LAB_8005c0e4                                  
        8005c0e4 01 00 22 22     addi       v0,s1,0x1
        8005c0e8 21 10 a2 03     addu       v0,sp,v0
        8005c0ec 1c 01 43 80     lb         v1,0x11c(v0)
        8005c0f0 21 10 b1 03     addu       v0,sp,s1
        8005c0f4 1c 01 42 80     lb         v0,0x11c(v0)
        8005c0f8 00 00 00 00     nop
        8005c0fc 22 10 62 00     sub        v0,v1,v0
        8005c100 83 c8 02 00     sra        t9,v0,0x2
        8005c104 03 00 41 04     bgez       v0,0x8005c114
        8005c108 00 00 00 00     _nop
        8005c10c 03 00 42 24     addiu      v0,v0,0x3
        8005c110 83 c8 02 00     sra        t9,v0,0x2
                             LAB_8005c114                                 
        8005c114 00 16 19 00     sll        v0,t9,0x18
        8005c118 03 16 02 00     sra        v0,v0,0x18
        8005c11c 18 00 bf 8f     lw         ra,0x18(sp)
        8005c120 14 00 b1 8f     lw         s1,0x14(sp)
        8005c124 10 00 b0 8f     lw         s0,0x10(sp)
        8005c128 28 01 bd 27     addiu      sp,sp,0x128
        8005c12c 08 00 e0 03     jr         ra
        8005c130 00 00 00 00     _nop
		
		
		

Script changes to stop Penguinmon from loading at all:

JP version:
13F1C25E & 13FDDA8E   72 to 84   

ESP patch version:

14CE4B9A  7E to 90



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

A74B90 00 00 00 00 to 96 3D 00 00     tokomon animation
A74B98 00 00 00 00 to B8 2A 00 00     tokomon animation

9CE308 00 00 00 00 to A2 D7 00 00     patamon animation
9CE310 00 00 00 00 to 52 BD 00 00     patamon animation

8FD3A0 FA 80 00 00 to BE D7 00 00     kunemon animation
8FD3A4 2C 85 00 00 to 00 00 00 00     kunemon animation
8FD3A8 92 87 00 00 to E2 C3 00 00     kunemon animation

A80D2C 00 00 00 00 to E0 E3 00 00     unimon animation
A80D34 00 00 00 00 to 66 C2 00 00     unimon animation

9A60E4 00 00 00 00 to 26 BE 00 00     ogremon animation
9A60EC 00 00 00 00 to AA C2 00 00     ogremon animation

the japanese version has an extra animation for shellmon, so only 1 is needed rather than 2 in the NTSC-U version

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

A977C8 00 00 00 00 to AA 3C 00 00     yuramon animation

A68AFC 00 00 00 00 to 92 47 00 00     tanemon animation
A68B08 00 00 00 00 to 30 72 00 00     tanemon animation

A15248 00 00 00 00 to 36 A4 00 00     biyomon animation 
A1524C 00 00 00 00 to DE 9C 00 00     biyomon animation 

9BA8DC 00 00 00 00 to D0 B3 00 00     palmon animation
9BA8E4 00 00 00 00 to 1A A0 00 00     palmon animation

975930 4E A8 00 00 to 2A F1 00 00     monochromon animation

93002C 84 72 00 00 to DE 7C 00 00     leomon animation
930030 DE 7C 00 00 to DA D7 00 00     leomon animation
930034 00 00 00 00 to 7C BC 00 00     leomon animation

A530C0 A6 79 00 00 to F8 C8 00 00     coelamon animation
A530C8 00 00 00 00 to 2A DD 00 00     coelamon animation

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

the japanese version has an extra animation for ninjamon, so only 1 is needed rather than 2 in the NTSC-U version
8E6DC8 00 00 00 00 to 14 A4 00 00     ninjamon animation

8CB924 00 00 00 00 to AA 0F 01 00     phoenixmon animation
8CB92C 00 00 00 00 to F6 E0 00 00     phoenixmon animation

B17988 00 00 00 00 to C0 CE 00 00     H-kabuterimon animation
B17990 00 00 00 00 to 0E DA 00 00     H-kabuterimon animation

B4C94C 00 00 00 00 to 64 9C 00 00     megaseadramon animation
B4C954 00 00 00 00 to 94 96 00 00     megaseadramon animation

weregarurumon does not exist

B9546C 00 00 00 00 to F8 C6 00 00     panjamon animation
B95474 00 00 00 00 to 9A AB 00 00     panjamon animation

AE5B54 00 00 00 00 to 0A C1 00 00     gigadramon animation
AE5B5C 00 00 00 00 to 46 7A 00 00     gigadramon animation

B6B028 00 00 00 00 to 48 D9 00 00     metal etemon animation
B6B030 00 00 00 00 to 66 FE 00 00     metal etemon animation

C12310 00 00 00 00 to 78 79 00 00     myotismon animation
C12318 00 00 00 00 to 36 5E 00 00     myotismon animation

C3F3E0 00 00 00 00 to 70 88 00 00     yanmamon animation
C3F3E8 00 00 00 00 to DE 9A 00 00     yanmamon animation

B0AB0C 00 00 00 00 to 4A 3C 00 00     gotsumon animation

ABC074 00 00 00 00 to 0E 59 00 00     flarerizamon animation
ABC07C 00 00 00 00 to C6 44 00 00     flarerizamon animation

C32884 00 00 00 00 to 74 49 00 00     warumonzaemon animation
C332E8 00 00 00 00 to 74 49 00 00     warumonzaemon animation
C332EC 00 00 00 00 to CA 40 00 00     warumonzaemon animation

C5236C 00 00 00 00 to 32 45 00 00     snow agumon animation
C52374 00 00 00 00 to BA 4A 00 00     snow agumon animation

B32324 00 00 00 00 to A6 60 00 00     hyogamon animation
B3232C 00 00 00 00 to 24 4D 00 00     hyogamon animation

BABEA8 00 00 00 00 to E8 4A 00 00     platinum sukamon animation

AAE938 00 00 00 00 to 36 57 00 00     dokunemon animation
AAE940 00 00 00 00 to A4 48 00 00     dokunemon animation

BDF11C 00 00 00 00 to FE 60 00 00     shinma unimon animation
BDF124 00 00 00 00 to A2 59 00 00     shinma unimon animation

BEEE60 00 00 00 00 to F4 2F 00 00     tankmon animation
BEEE68 00 00 00 00 to 08 34 00 00     tankmon animation

BD2EDC 00 00 00 00 to 60 52 00 00     red vegiemon animation
BD2EE4 00 00 00 00 to E2 36 00 00     red vegiemon animation

B3FFAC 00 00 00 00 to E0 52 00 00     J-mojyamon animation
B3FFB4 00 00 00 00 to 7A 37 00 00     J-mojyamon animation

B862F0 00 00 00 00 to 7A 6D 00 00     nisedrimogemon animation
B862F8 00 00 00 00 to 94 4A 00 00     nisedrimogemon animation

AFC4B0 00 00 00 00 to E6 58 00 00     goburimon animation
AFC4B8 00 00 00 00 to F6 4C 00 00     goburimon animation

C056CC 00 00 00 00 to C8 48 00 00     mud frigimon animation
C056D4 00 00 00 00 to C0 36 00 00     mud frigimon animation

BC7CFC 00 00 00 00 to 6E 50 00 00     psychemon animation
BC7D04 00 00 00 00 to 52 36 00 00     psychemon animation

B5EFC8 00 00 00 00 to 94 44 00 00     modokibetamon animation
B5EFD0 00 00 00 00 to E2 33 00 00     modokibetamon animation

BF94D4 00 00 00 00 to 5E 5F 00 00     toy agumon animation
BF94DC 00 00 00 00 to 3E 58 00 00     toy agumon animation

BB7EC8 00 00 00 00 to AA 71 00 00     piddomon animation
BB7ECC 00 00 00 00 to 10 7A 00 00     piddomon animation

AA1F6C 00 00 00 00 to 52 50 00 00     aruraumon animation
AA1F74 00 00 00 00 to 5A 48 00 00     aruraumon animation

AD685C 00 00 00 00 to 78 70 00 00     geremon animation

C245C8 00 00 00 00 to A8 5D 00 00     vermilimon animation
C245D0 00 00 00 00 to F8 65 00 00     vermilimon animation

AC9510 00 00 00 00 to 36 48 00 00     fugamon animation
AC9514 00 00 00 00 to 2A 51 00 00     fugamon animation

DA0AF4 00 00 00 00 to 56 27 00 00     tekkamon animation
DA0AFC 00 00 00 00 to DA 1B 00 00     tekkamon animation

D16AC0 00 00 00 00 to A8 42 00 00     morishellmon animation
D16AC8 00 00 00 00 to A2 38 00 00     morishellmon animation

C9D138 00 00 00 00 to 6A 76 00 00     guardromon animation

D09254 00 00 00 00 to C0 5B 00 00     muchomon animation
D09258 00 00 00 00 to C0 5B 00 00     muchomon animation
D0925C 00 00 00 00 to EE 4A 00 00     muchomon animation

CE6244 00 00 00 00 to A8 41 00 00     icemon animation
CE6248 00 00 00 00 to A8 41 00 00     icemon animation
CE624C 00 00 00 00 to 4A 3C 00 00     icemon animation

C5FA5C 00 00 00 00 to 70 61 00 00     akatorimon animation
C5FA64 00 00 00 00 to 76 50 00 00     akatorimon animation

DB7A08 00 00 00 00 to 26 64 00 00     tsukaimon animation
DB7A10 00 00 00 00 to D6 49 00 00     tsukaimon animation

D4FDD8 00 00 00 00 to DE 58 00 00     sharmamon animation
D4FDE0 00 00 00 00 to EE 4C 00 00     sharmamon animation

C81F74 00 00 00 00 to 5E 5F 00 00     clear agumon animation
C81F7C 00 00 00 00 to 3E 58 00 00     clear agumon animation

DD0D5C 00 00 00 00 to 60 52 00 00     weedmon animation
DD0D64 00 00 00 00 to E2 36 00 00     weedmon animation

CD8D5C 00 00 00 00 to 50 57 00 00     ice devimon animation
CD8D64 00 00 00 00 to EE 46 00 00     ice devimon animation

C8F714 00 00 00 00 to 94 4B 00 00     darkizamon animation
C8F71C 00 00 00 00 to DA 57 00 00     darkizamon animation

D5D340 00 00 00 00 to CA 7B 00 00     sand yanmamon animation
D5D344 00 00 00 00 to CA 7B 00 00     sand yanmamon animation
D5D348 00 00 00 00 to BE 71 00 00     sand yanmamon animation

D6F578 00 00 00 00 to 68 4C 00 00     snow goburimon animation
D6F580 00 00 00 00 to 7C 3F 00 00     snow goburimon animation

C74D70 00 00 00 00 to 80 40 00 00     blue meramon animation
C74D78 00 00 00 00 to 34 55 00 00     blue meramon animation

CC860C 00 00 00 00 to 30 65 00 00     gururumon animation
CC8614 00 00 00 00 to 40 52 00 00     gururumon animation

D43624 00 00 00 00 to 32 54 00 00     saberdramon animation
D4362C 00 00 00 00 to 68 3B 00 00     saberdramon animation

D7BB44 00 00 00 00 to CA 5F 00 00     soulmon animation
D7BB48 00 00 00 00 to AA 70 00 00     soulmon animation
D7BB4C 00 00 00 00 to CE 50 00 00     soulmon animation

CBC7C4 00 00 00 00 to 2C 50 00 00     rockmon animation
CBC7CC 00 00 00 00 to C4 36 00 00     rockmon animation

Otamamon has animations

CAD01C 00 00 00 00 to EE 6A 00 00     gekomon animation
CAD024 00 00 00 00 to 8C 4B 00 00     gekomon animation

D93D54 00 00 00 00 to FC 7A 00 00     tentomon animation
D93D5C 00 00 00 00 to 26 5E 00 00     tentomon animation

DC717C 00 00 00 00 to A6 31 00 00     waru seadramon animation
DC7184 00 00 00 00 to BC 3F 00 00     waru seadramon animation

Meteoromon has animations

D26854 00 00 00 00 to 00 85 00 00     machinedramon animation
D26858 00 00 00 00 to BC 51 00 00     machinedramon animation

Jijimon has animations

Market Manager has animations

Shogun Gekkomon has animations

King Sukamon has animations