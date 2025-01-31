int * CalculateMovementDamage(int AttackerPtr,int *DefenderPtr,int MoveID)

byte[3] ResistancesArray;

{
  TechSpeciality = &TechSpecialityPtr[MoveID * 0x10];

  for (iVar2 = 0; iVar2 < 3; iVar2 = iVar2 + 1)
  {
    if ((&DigimonType)[iVar2 + &DefenderTypes] == -1) //No type
      ResistancesArray[iVar2] = 10;
    
    else
      ResistancesArray[iVar2] = (&TypeResistances)[(&DigimonType)[iVar2 + &DefenderTypes] + TechSpeciality * 7]; //Get the type matchup value
  }
  DefenderDef = &DefenderPtr + 0x3a;

  if (MoveID == 45) //Counter
    DefenderDef = (DefenderDef * 3) / 10);

    if ((MoveID < 58) || (112 < MoveID)) //It is a normal technique
    {
      StatsFactor = DigimonAttack - DefenderDef;

      if (500 < StatsFactor) 
        StatsFactor = 500;
    
      if (StatsFactor < -500) 
        StatsFactor = -500;
    
      ResistanceSum = ResistancesArray[0] + ResistancesArray[1] + ResistancesArray[2];
      variance = ReturnRandom(21);
      techDamage = TechDamages[MoveID * 10];
      finalDamage = (((((ResistanceSum * (StatsFactor + (techDamage * StatsFactor) / 500)) / 30) * (variance + 90)) / 100);
   }
   else 
   {
      ResistanceSum = ResistancesArray[0] + ResistancesArray[1] + ResistancesArray[2];
      techDamage = TechDamages[MoveID * 10];
      finalDamage = (ResistanceSum * techDamage) / 30;
   }

   if ((57 < MoveID) && (MoveID < 113)) //It is a finisher
   {
     if (AttackerPtr == PartnerPtr) // Your digimon
     {
      if (40 < FinisherCharge) //The Finisher charge goes up to 80
        finalDamage = Damage * FinisherCharge / 40); //Only applies if you filled half or more
      
     }
     else //It is a NPC
     {
       ExtraBoost = ReturnRandom(101); //Finisher varies between 100% to 200% damage
       finalDamage = (Damage * (ExtraBoost + 100)) / 100);
     }
     variance = ReturnRandom(21);
     finalDamage = (finalDamage * (variance + 90) / 100);
   }

   if (finalDamage < 1) 
     finalDamage = 1;
   
   if (9999 < finalDamage) 
     finalDamage = 9999;   

 return finalDamage;
}



Disassembly:

         Offset      Hex               Commands       

        8005beb8 d0 ff bd 27     addiu      sp,sp,-0x30
        8005bebc 20 00 bf af     sw         ra,0x20(sp)
        8005bec0 1c 00 b3 af     sw         s3,0x1c(sp)
        8005bec4 18 00 b2 af     sw         s2,0x18(sp)
        8005bec8 14 00 b1 af     sw         s1,0x14(sp)
        8005becc 10 00 b0 af     sw         s0,0x10(sp)
        8005bed0 12 80 03 3c     lui        v1,0x8012
        8005bed4 21 90 80 00     move       s2,a0
        8005bed8 21 80 c0 00     move       s0,a2
        8005bedc 00 21 10 00     sll        a0,s0,0x4
        8005bee0 45 62 63 24     addiu      v1,v1,0x6245
        8005bee4 21 98 80 00     move       s3,a0
        8005bee8 21 18 64 00     addu       v1,v1,a0
        8005beec 00 00 64 90     lbu        a0,0x0(v1)
        8005bef0 21 10 00 00     clear      v0
        8005bef4 c0 18 04 00     sll        v1,a0,0x3
        8005bef8 1c 00 00 10     b          0x8005bf6c
        8005befc 22 18 64 00     _sub       v1,v1,a0
                             LAB_8005bf00                                   
        8005bf00 00 00 a6 8c     lw         a2,0x0(a1)
        8005bf04 ff 00 01 24     li         at,0xff
        8005bf08 40 20 06 00     sll        a0,a2,0x1
        8005bf0c 20 20 86 00     add        a0,a0,a2
        8005bf10 80 20 04 00     sll        a0,a0,0x2
        8005bf14 20 20 86 00     add        a0,a0,a2
        8005bf18 80 30 04 00     sll        a2,a0,0x2
        8005bf1c 13 80 04 3c     lui        a0,0x8013
        8005bf20 d2 ce 84 24     addiu      a0,a0,-0x312e
        8005bf24 21 20 86 00     addu       a0,a0,a2
        8005bf28 21 20 44 00     addu       a0,v0,a0
        8005bf2c 00 00 84 90     lbu        a0,0x0(a0)
        8005bf30 00 00 00 00     nop
        8005bf34 09 00 81 10     beq        a0,at,0x8005bf5c
        8005bf38 21 30 80 00     _move      a2,a0
        8005bf3c 12 80 04 3c     lui        a0,0x8012
        8005bf40 70 5f 84 24     addiu      a0,a0,0x5f70
        8005bf44 21 20 83 00     addu       a0,a0,v1
        8005bf48 21 20 c4 00     addu       a0,a2,a0
        8005bf4c 00 00 86 90     lbu        a2,0x0(a0)
        8005bf50 21 20 a2 03     addu       a0,sp,v0
        8005bf54 04 00 00 10     b          0x8005bf68
        8005bf58 2c 00 86 a0     _sb        a2,0x2c(a0)
                             LAB_8005bf5c                                   
        8005bf5c 0a 00 06 24     li         a2,0xa
        8005bf60 21 20 a2 03     addu       a0,sp,v0
        8005bf64 2c 00 86 a0     sb         a2,0x2c(a0)
                             LAB_8005bf68                                  
        8005bf68 01 00 42 20     addi       v0,v0,0x1
                             LAB_8005bf6c                                      
        8005bf6c 03 00 41 28     slti       at,v0,0x3
        8005bf70 e3 ff 20 14     bne        at,zero,0x8005bf00
        8005bf74 00 00 00 00     _nop
        8005bf78 38 00 42 86     lh         v0,0x38(s2)
        8005bf7c 3a 00 a4 84     lh         a0,0x3a(a1)
        8005bf80 2d 00 01 24     li         at,0x2d
        8005bf84 0c 00 01 16     bne        s0,at,0x8005bfb8
        8005bf88 00 00 00 00     _nop
        8005bf8c 40 18 04 00     sll        v1,a0,0x1
        8005bf90 20 20 64 00     add        a0,v1,a0
        8005bf94 66 66 03 3c     lui        v1,0x6666
        8005bf98 67 66 63 34     ori        v1,v1,0x6667
        8005bf9c 18 00 64 00     mult       v1,a0
        8005bfa0 10 18 00 00     mfhi       v1
        8005bfa4 c2 27 04 00     srl        a0,a0,0x1f
        8005bfa8 83 18 03 00     sra        v1,v1,0x2
        8005bfac 21 18 64 00     addu       v1,v1,a0
        8005bfb0 00 24 03 00     sll        a0,v1,0x10
        8005bfb4 03 24 04 00     sra        a0,a0,0x10
                             LAB_8005bfb8                                   
        8005bfb8 3a 00 01 2a     slti       at,s0,0x3a
        8005bfbc 1b 00 20 14     bne        at,zero,0x8005c02c
        8005bfc0 00 00 00 00     _nop
        8005bfc4 71 00 01 2a     slti       at,s0,0x71
        8005bfc8 18 00 20 10     beq        at,zero,0x8005c02c
        8005bfcc 00 00 00 00     _nop
        8005bfd0 2d 00 a5 93     lbu        a1,0x2d(sp)
        8005bfd4 2c 00 a4 93     lbu        a0,0x2c(sp)
        8005bfd8 2e 00 a3 93     lbu        v1,0x2e(sp)
        8005bfdc 20 20 85 00     add        a0,a0,a1
        8005bfe0 20 20 64 00     add        a0,v1,a0
        8005bfe4 12 80 03 3c     lui        v1,0x8012
        8005bfe8 40 62 63 24     addiu      v1,v1,0x6240
        8005bfec 21 18 73 00     addu       v1,v1,s3
        8005bff0 00 00 63 84     lh         v1,0x0(v1)
        8005bff4 00 00 00 00     nop
        8005bff8 20 10 43 00     add        v0,v0,v1
        8005bffc 18 00 44 00     mult       v0,a0
        8005c000 88 88 02 3c     lui        v0,0x8888
        8005c004 12 20 00 00     mflo       a0
        8005c008 89 88 42 34     ori        v0,v0,0x8889
        8005c00c 00 00 00 00     nop
        8005c010 18 00 44 00     mult       v0,a0
        8005c014 c2 1f 04 00     srl        v1,a0,0x1f
        8005c018 10 10 00 00     mfhi       v0
        8005c01c 21 10 44 00     addu       v0,v0,a0
        8005c020 03 11 02 00     sra        v0,v0,0x4
        8005c024 36 00 00 10     b          0x8005c100
        8005c028 21 88 43 00     _addu      s1,v0,v1
                             LAB_8005c02c                                  
        8005c02c 22 88 44 00     sub        s1,v0,a0
        8005c030 f5 01 21 2a     slti       at,s1,0x1f5
        8005c034 02 00 20 14     bne        at,zero,0x8005c040
        8005c038 00 00 00 00     _nop
        8005c03c f4 01 11 24     li         s1,0x1f4
                             LAB_8005c040                                    
        8005c040 0c fe 21 2a     slti       at,s1,-0x1f4
        8005c044 02 00 20 10     beq        at,zero,0x8005c050
        8005c048 00 00 00 00     _nop
        8005c04c 0c fe 11 24     li         s1,-0x1f4
                             LAB_8005c050                                   
        8005c050 b5 8d 02 0c     jal        0x800a36d4  //Return random
        8005c054 15 00 04 24     _li        a0,0x15
        8005c058 12 80 03 3c     lui        v1,0x8012
        8005c05c 40 62 63 24     addiu      v1,v1,0x6240
        8005c060 21 18 73 00     addu       v1,v1,s3
        8005c064 00 00 66 84     lh         a2,0x0(v1)
        8005c068 2d 00 a5 93     lbu        a1,0x2d(sp)
        8005c06c 18 00 26 02     mult       s1,a2
        8005c070 62 10 03 3c     lui        v1,0x1062
        8005c074 12 20 00 00     mflo       a0
        8005c078 d3 4d 63 34     ori        v1,v1,0x4dd3
        8005c07c 00 00 00 00     nop
        8005c080 18 00 64 00     mult       v1,a0
        8005c084 5a 00 42 20     addi       v0,v0,0x5a
        8005c088 10 18 00 00     mfhi       v1
        8005c08c c2 27 04 00     srl        a0,a0,0x1f
        8005c090 43 19 03 00     sra        v1,v1,0x5
        8005c094 21 18 64 00     addu       v1,v1,a0
        8005c098 20 30 c3 00     add        a2,a2,v1
        8005c09c 2c 00 a4 93     lbu        a0,0x2c(sp)
        8005c0a0 2e 00 a3 93     lbu        v1,0x2e(sp)
        8005c0a4 20 20 85 00     add        a0,a0,a1
        8005c0a8 20 18 64 00     add        v1,v1,a0
        8005c0ac 18 00 66 00     mult       v1,a2
        8005c0b0 88 88 03 3c     lui        v1,0x8888
        8005c0b4 12 28 00 00     mflo       a1
        8005c0b8 89 88 63 34     ori        v1,v1,0x8889
        8005c0bc 00 00 00 00     nop
        8005c0c0 18 00 65 00     mult       v1,a1
        8005c0c4 c2 27 05 00     srl        a0,a1,0x1f
        8005c0c8 10 18 00 00     mfhi       v1
        8005c0cc 21 18 65 00     addu       v1,v1,a1
        8005c0d0 03 19 03 00     sra        v1,v1,0x4
        8005c0d4 21 18 64 00     addu       v1,v1,a0
        8005c0d8 18 00 62 00     mult       v1,v0
        8005c0dc eb 51 02 3c     lui        v0,0x51eb
        8005c0e0 12 18 00 00     mflo       v1
        8005c0e4 1f 85 42 34     ori        v0,v0,0x851f
        8005c0e8 00 00 00 00     nop
        8005c0ec 18 00 43 00     mult       v0,v1
        8005c0f0 10 10 00 00     mfhi       v0
        8005c0f4 c2 1f 03 00     srl        v1,v1,0x1f
        8005c0f8 43 11 02 00     sra        v0,v0,0x5
        8005c0fc 21 88 43 00     addu       s1,v0,v1
                             LAB_8005c100                                   
        8005c100 3a 00 01 2a     slti       at,s0,0x3a
        8005c104 35 00 20 14     bne        at,zero,0x8005c1dc
        8005c108 00 00 00 00     _nop
        8005c10c 71 00 01 2a     slti       at,s0,0x71
        8005c110 32 00 20 10     beq        at,zero,0x8005c1dc
        8005c114 00 00 00 00     _nop
        8005c118 13 80 01 3c     lui        at,0x8013
        8005c11c 48 f3 22 8c     lw         v0,-0xcb8(at) //DAT_8012f348
        8005c120 00 00 00 00     nop
        8005c124 13 00 42 16     bne        s2,v0,0x8005c174
        8005c128 00 00 00 00     _nop
        8005c12c 20 92 82 8f     lw         v0,-0x6de0(gp)
        8005c130 00 00 00 00     nop
        8005c134 68 06 42 90     lbu        v0,0x668(v0)
        8005c138 00 00 00 00     nop
        8005c13c 29 00 41 2c     sltiu      at,v0,0x29
        8005c140 19 00 20 14     bne        at,zero,0x8005c1a8
        8005c144 21 18 40 00     _move      v1,v0
        8005c148 18 00 23 02     mult       s1,v1
        8005c14c 66 66 02 3c     lui        v0,0x6666
        8005c150 12 18 00 00     mflo       v1
        8005c154 67 66 42 34     ori        v0,v0,0x6667
        8005c158 00 00 00 00     nop
        8005c15c 18 00 43 00     mult       v0,v1
        8005c160 10 10 00 00     mfhi       v0
        8005c164 c2 1f 03 00     srl        v1,v1,0x1f
        8005c168 03 11 02 00     sra        v0,v0,0x4
        8005c16c 0e 00 00 10     b          0x8005c1a8
        8005c170 21 88 43 00     _addu      s1,v0,v1
                             LAB_8005c174                                   
        8005c174 b5 8d 02 0c     jal        0x800a36d4 //Return random
        8005c178 65 00 04 24     _li        a0,0x65
        8005c17c 64 00 42 20     addi       v0,v0,0x64
        8005c180 18 00 22 02     mult       s1,v0
        8005c184 eb 51 02 3c     lui        v0,0x51eb
        8005c188 12 18 00 00     mflo       v1
        8005c18c 1f 85 42 34     ori        v0,v0,0x851f
        8005c190 00 00 00 00     nop
        8005c194 18 00 43 00     mult       v0,v1
        8005c198 10 10 00 00     mfhi       v0
        8005c19c c2 1f 03 00     srl        v1,v1,0x1f
        8005c1a0 43 11 02 00     sra        v0,v0,0x5
        8005c1a4 21 88 43 00     addu       s1,v0,v1
                             LAB_8005c1a8                                  
        8005c1a8 b5 8d 02 0c     jal        0x800a36d4 //Return random
        8005c1ac 15 00 04 24     _li        a0,0x15
        8005c1b0 5a 00 42 20     addi       v0,v0,0x5a
        8005c1b4 18 00 22 02     mult       s1,v0
        8005c1b8 eb 51 02 3c     lui        v0,0x51eb
        8005c1bc 12 18 00 00     mflo       v1
        8005c1c0 1f 85 42 34     ori        v0,v0,0x851f
        8005c1c4 00 00 00 00     nop
        8005c1c8 18 00 43 00     mult       v0,v1
        8005c1cc 10 10 00 00     mfhi       v0
        8005c1d0 c2 1f 03 00     srl        v1,v1,0x1f
        8005c1d4 43 11 02 00     sra        v0,v0,0x5
        8005c1d8 21 88 43 00     addu       s1,v0,v1
                             LAB_8005c1dc                                     
        8005c1dc 02 00 20 1e     bgtz       s1,0x8005c1e8
        8005c1e0 00 00 00 00     _nop
        8005c1e4 01 00 11 24     li         s1,0x1
                             LAB_8005c1e8                                  
        8005c1e8 10 27 21 2a     slti       at,s1,0x2710
        8005c1ec 02 00 20 14     bne        at,zero,0x8005c1f8
        8005c1f0 00 00 00 00     _nop
        8005c1f4 0f 27 11 24     li         s1,0x270f
                             LAB_8005c1f8                                    
        8005c1f8 21 10 20 02     move       v0,s1
        8005c1fc 20 00 bf 8f     lw         ra,0x20(sp)
        8005c200 1c 00 b3 8f     lw         s3,0x1c(sp)
        8005c204 18 00 b2 8f     lw         s2,0x18(sp)
        8005c208 14 00 b1 8f     lw         s1,0x14(sp)
        8005c20c 10 00 b0 8f     lw         s0,0x10(sp)
        8005c210 08 00 e0 03     jr         ra
        8005c214 30 00 bd 27     _addiu     sp,sp,0x30
