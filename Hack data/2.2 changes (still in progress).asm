//Multiple changes to the attack behaviour, mostly to change some attacks into finishers

//battle 
        8005d3d8 75 00 41 28     slti       at,v0,0x75

        80058734 75 00 a1 28     slti       at,a1,0x75

        8005a1ac 75 00 81 28     slti       at,a0,0x75

        8005bca4 75 00 e1 28     slti       at,a3,0x75

        8005d824 75 00 61 2a     slti       at,s3,0x75

        8005e050 75 00 41 28     slti       at,v0,0x75
		
//tournament
        800601cc 75 00 a1 28     slti       at,a1,0x75

        80061d74 75 00 81 28     slti       at,a0,0x75

        80063d6c 75 00 e1 28     slti       at,a3,0x75

        8006517c 75 00 41 28     slti       at,v0,0x75

        800655c8 75 00 61 2a     slti       at,s3,0x75

        80065e84 75 00 41 28     slti       at,v0,0x75

//SLUS
        800f5e34 75 00 a1 28     slti       at,a1,0x75



//Calculate damage function, it has been changed and adapted to the new effects, it is also now shared between the battle and tournament functions


int CalculateDamage(DigimonEntity *AttackerPtr, DigimonEntity *DefenderPtr, int moveID)
{
  byte attackType;
  int attack;
  int rand;
  uint DigimonType;
  int defense;
  int attDamage;
  byte typeDamageFactor[3];
  
  attackType = AllMovementData[moveID].type;
  for (int i = 0; i < 3; i++) 
  {
    DigimonType = AllDigimonData[(DefenderPtr->DigimonEntity).DigimonType].specialities[i];
    if (DigimonType == -1) 
      typeDamageFactor[i] = 10;    
    else 
      typeDamageFactor[i] = TypeResistances[DigimonType + attackType * 7];    
  }

  attack = AttackerPtr->DigimonStats.DigimonOff;
  defense = DefenderPtr->DigimonStats.DigimonDef;

  if (moveID == 45) //attack is counter
    defense = (defense * 3) / 10;
  
  if ((moveID < 57) || (116 < moveID)) //is not a finisher
  {
    attack = attack - defense;

    if (500 < attack) 
      attack = 500;
    
    if (attack < -500) 
      attack = -500;
  
    rand = ReturnRandom(21);
    attDamage = AllMovementData[moveID].damage;
    if ((AttackerPtr == PartnerPtr) && (moveID == RaiseData[AttackerPtr->DigimonEntity.DigimonType].moveBoost)) 
      attDamage = attDamage + RaiseData[AttackerPtr->DigimonEntity.DigimonType].boostValue;
    
    attDamage = ((((typeDamageFactor[2] + typeDamageFactor[0] + typeDamageFactor[1]) * 
                   (attDamage + (attack * attDamage) / 500)) / 30) * (rand + 90)) / 100;
  }
  else 
    attDamage = (attack + AllMovementData[moveID].damage) * (typeDamageFactor[2] + typeDamageFactor[0] + typeDamageFactor[1]) / 30;
  
  if ((56 < moveID) && (moveID < 117)) 
  {
    if (AttackerPtr == PartnerPtr)     
      if (40 < FinisherFilled)                        //Finisher filled is how filled the finisher bar is (the one where you have to mash L1 + R1), the max is 80
        attDamage = (attDamage * FinisherFilled) / 40;       
    else 
    {
      rand = ReturnRandom(101);
      attDamage = (attDamage * (rand + 100)) / 100;
    }

    rand = ReturnRandom(21);
    attDamage = (attDamage * (rand + 90)) / 100;
  }

  if (AttackerPtr->NewEffects == 2) //attacker is burned
    attDamage = attDamage - (attDamage / 4);
  
  if (attDamage < 1) 
    attDamage = 1;
  
  if (DefenderPtr->NewEffects == 4) //defender is frozen
    attDamage = attDamage + (attDamage / 4);
  
  if (9999 < attDamage) 
    attDamage = 9999;
  
  return attDamage;
}
                             
                             CalculateDamage
        80095934 cc ff bd 27     addiu      sp,sp,-0x34
        80095938 24 00 bf af     sw         ra,0x24(sp)
        8009593c 1c 00 b3 af     sw         s3,0x1c(sp)
        80095940 18 00 b2 af     sw         s2,0x18(sp)
        80095944 14 00 b1 af     sw         s1,0x14(sp)
        80095948 10 00 b0 af     sw         s0,0x10(sp)
        8009594c 20 00 b4 af     sw         s4,0x20(sp)
        80095950 12 80 03 3c     lui        v1,0x8012
        80095954 21 a0 05 00     move       s4,a1
        80095958 21 90 80 00     move       s2,a0
        8009595c 21 80 c0 00     move       s0,a2
        80095960 00 21 10 00     sll        a0,s0,0x4
        80095964 45 62 63 24     addiu      v1,v1,0x6245
        80095968 21 98 80 00     move       s3,a0
        8009596c 21 18 64 00     addu       v1,v1,a0
        80095970 00 00 64 90     lbu        a0,0x0(v1)  //AllMovementData[moveID].type
        80095974 21 10 00 00     clear      v0
        80095978 c0 18 04 00     sll        v1,a0,0x3
        8009597c 1c 00 00 10     b          0x800959f0
        80095980 22 18 64 00     _sub       v1,v1,a0
                             LAB_80095984                                   
        80095984 00 00 a6 8c     lw         a2,0x0(a1)  
        80095988 ff 00 01 24     li         at,0xff
        8009598c 40 20 06 00     sll        a0,a2,0x1
        80095990 20 20 86 00     add        a0,a0,a2
        80095994 80 20 04 00     sll        a0,a0,0x2
        80095998 20 20 86 00     add        a0,a0,a2
        8009599c 80 30 04 00     sll        a2,a0,0x2
        800959a0 13 80 04 3c     lui        a0,0x8013
        800959a4 d2 ce 84 24     addiu      a0,a0,-0x312e
        800959a8 21 20 86 00     addu       a0,a0,a2
        800959ac 21 20 44 00     addu       a0,v0,a0
        800959b0 00 00 84 90     lbu        a0,0x0(a0)  ////AllDigimonData[(DefenderPtr->DigimonEntity).DigimonType].specialities[i]
        800959b4 00 00 00 00     nop
        800959b8 09 00 81 10     beq        a0,at,0x800959e0
        800959bc 21 30 80 00     _move      a2,a0
        800959c0 12 80 04 3c     lui        a0,0x8012
        800959c4 70 5f 84 24     addiu      a0,a0,0x5f70
        800959c8 21 20 83 00     addu       a0,a0,v1
        800959cc 21 20 c4 00     addu       a0,a2,a0
        800959d0 00 00 86 90     lbu        a2,0x0(a0)  //TypeResistances[DigimonType + attackType * 7]
        800959d4 21 20 a2 03     addu       a0,sp,v0
        800959d8 04 00 00 10     b          0x800959ec
        800959dc 2c 00 86 a0     _sb        a2,0x2c(a0)
                             LAB_800959e0                                   
        800959e0 0a 00 06 24     li         a2,0xa
        800959e4 21 20 a2 03     addu       a0,sp,v0
        800959e8 2c 00 86 a0     sb         a2,0x2c(a0)
                             LAB_800959ec                                    
        800959ec 01 00 42 20     addi       v0,v0,0x1
                             LAB_800959f0                                  
        800959f0 03 00 41 28     slti       at,v0,0x3
        800959f4 e3 ff 20 14     bne        at,zero,0x80095984
        800959f8 00 00 00 00     _nop
        800959fc 38 00 42 86     lh         v0,0x38(s2)  //AttackerPtr->DigimonStats.DigimonOff
        80095a00 3a 00 a4 84     lh         a0,0x3a(a1)  //DefenderPtr->DigimonStats.DigimonDef
        80095a04 2d 00 01 24     li         at,0x2d
        80095a08 0c 00 01 16     bne        s0,at,0x80095a3c
        80095a0c 00 00 00 00     _nop
        80095a10 40 18 04 00     sll        v1,a0,0x1
        80095a14 20 20 64 00     add        a0,v1,a0
        80095a18 66 66 03 3c     lui        v1,0x6666
        80095a1c 67 66 63 34     ori        v1,v1,0x6667
        80095a20 18 00 64 00     mult       v1,a0
        80095a24 10 18 00 00     mfhi       v1
        80095a28 c2 27 04 00     srl        a0,a0,0x1f
        80095a2c 83 18 03 00     sra        v1,v1,0x2
        80095a30 21 18 64 00     addu       v1,v1,a0
        80095a34 00 24 03 00     sll        a0,v1,0x10
        80095a38 03 24 04 00     sra        a0,a0,0x10
                             LAB_80095a3c                                    
        80095a3c 39 00 01 2a     slti       at,s0,0x39
        80095a40 1b 00 20 14     bne        at,zero,0x80095ab0
        80095a44 00 00 00 00     _nop
        80095a48 75 00 01 2a     slti       at,s0,0x75
        80095a4c 18 00 20 10     beq        at,zero,0x80095ab0
        80095a50 00 00 00 00     _nop
        80095a54 2d 00 a5 93     lbu        a1,0x2d(sp)
        80095a58 2c 00 a4 93     lbu        a0,0x2c(sp)
        80095a5c 2e 00 a3 93     lbu        v1,0x2e(sp)
        80095a60 20 20 85 00     add        a0,a0,a1
        80095a64 20 20 64 00     add        a0,v1,a0
        80095a68 12 80 03 3c     lui        v1,0x8012
        80095a6c 40 62 63 24     addiu      v1,v1,0x6240
        80095a70 21 18 73 00     addu       v1,v1,s3
        80095a74 00 00 63 84     lh         v1,0x0(v1)  //AllMovementData[moveID].damage
        80095a78 00 00 00 00     nop
        80095a7c 20 10 43 00     add        v0,v0,v1
        80095a80 18 00 44 00     mult       v0,a0
        80095a84 88 88 02 3c     lui        v0,0x8888
        80095a88 12 20 00 00     mflo       a0
        80095a8c 89 88 42 34     ori        v0,v0,0x8889
        80095a90 00 00 00 00     nop
        80095a94 18 00 44 00     mult       v0,a0
        80095a98 c2 1f 04 00     srl        v1,a0,0x1f
        80095a9c 10 10 00 00     mfhi       v0
        80095aa0 21 10 44 00     addu       v0,v0,a0
        80095aa4 03 11 02 00     sra        v0,v0,0x4
        80095aa8 4b 00 00 10     b          0x80095bd8
        80095aac 21 88 43 00     _addu      s1,v0,v1
                             LAB_80095ab0                                   
        80095ab0 22 88 44 00     sub        s1,v0,a0
        80095ab4 f5 01 21 2a     slti       at,s1,0x1f5
        80095ab8 02 00 20 14     bne        at,zero,0x80095ac4
        80095abc 00 00 00 00     _nop
        80095ac0 f4 01 11 24     li         s1,0x1f4
                             LAB_80095ac4                                      
        80095ac4 0c fe 21 2a     slti       at,s1,-0x1f4
        80095ac8 02 00 20 10     beq        at,zero,0x80095ad4
        80095acc 00 00 00 00     _nop
        80095ad0 0c fe 11 24     li         s1,-0x1f4
                             LAB_80095ad4                                   
        80095ad4 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                    
        80095ad8 15 00 04 24     _li        a0,0x15
        80095adc 12 80 03 3c     lui        v1,0x8012
        80095ae0 40 62 63 24     addiu      v1,v1,0x6240
        80095ae4 21 18 73 00     addu       v1,v1,s3
        80095ae8 00 00 66 84     lh         a2,0x0(v1)  //AllMovementData[moveID].damage
        80095aec 00 00 00 00     nop
        80095af0 13 80 01 3c     lui        at,0x8013
        80095af4 48 f3 21 8c     lw         at,-0xcb8(at)  //PartnerPtr                       
        80095af8 00 00 00 00     nop
        80095afc 10 00 41 16     bne        s2,at,0x80095b40
        80095b00 00 00 00 00     _nop
        80095b04 00 00 43 8e     lw         v1,0x0(s2)
        80095b08 00 00 00 00     nop
        80095b0c c0 08 03 00     sll        at,v1,0x3
        80095b10 22 08 23 00     sub        at,at,v1
        80095b14 80 18 01 00     sll        v1,at,0x2
        80095b18 12 80 01 3c     lui        at,0x8012
        80095b1c c7 25 21 24     addiu      at,at,0x25c7
        80095b20 21 08 23 00     addu       at,at,v1
        80095b24 00 00 23 80     lb         v1,0x0(at)  //RaiseData[AttackerPtr->DigimonEntity.DigimonType].moveBoost
        80095b28 00 00 00 00     nop
        80095b2c 04 00 03 16     bne        s0,v1,0x80095b40
        80095b30 00 00 00 00     _nop
        80095b34 03 00 23 84     lh         v1,0x3(at)  //RaiseData[AttackerPtr->DigimonEntity.DigimonType].boostValue
        80095b38 00 00 00 00     nop
        80095b3c 20 30 c3 00     add        a2,a2,v1
                             LAB_80095b40                                   
        80095b40 2d 00 a5 93     lbu        a1,0x2d(sp)
        80095b44 18 00 26 02     mult       s1,a2
        80095b48 62 10 03 3c     lui        v1,0x1062
        80095b4c 12 20 00 00     mflo       a0
        80095b50 d3 4d 63 34     ori        v1,v1,0x4dd3
        80095b54 00 00 00 00     nop
        80095b58 18 00 64 00     mult       v1,a0
        80095b5c 5a 00 42 20     addi       v0,v0,0x5a
        80095b60 10 18 00 00     mfhi       v1
        80095b64 c2 27 04 00     srl        a0,a0,0x1f
        80095b68 43 19 03 00     sra        v1,v1,0x5
        80095b6c 21 18 64 00     addu       v1,v1,a0
        80095b70 20 30 c3 00     add        a2,a2,v1
        80095b74 2c 00 a4 93     lbu        a0,0x2c(sp)
        80095b78 2e 00 a3 93     lbu        v1,0x2e(sp)
        80095b7c 20 20 85 00     add        a0,a0,a1
        80095b80 20 18 64 00     add        v1,v1,a0
        80095b84 18 00 66 00     mult       v1,a2
        80095b88 88 88 03 3c     lui        v1,0x8888
        80095b8c 12 28 00 00     mflo       a1
        80095b90 89 88 63 34     ori        v1,v1,0x8889
        80095b94 00 00 00 00     nop
        80095b98 18 00 65 00     mult       v1,a1
        80095b9c c2 27 05 00     srl        a0,a1,0x1f
        80095ba0 10 18 00 00     mfhi       v1
        80095ba4 21 18 65 00     addu       v1,v1,a1
        80095ba8 03 19 03 00     sra        v1,v1,0x4
        80095bac 21 18 64 00     addu       v1,v1,a0
        80095bb0 18 00 62 00     mult       v1,v0
        80095bb4 eb 51 02 3c     lui        v0,0x51eb
        80095bb8 12 18 00 00     mflo       v1
        80095bbc 1f 85 42 34     ori        v0,v0,0x851f
        80095bc0 00 00 00 00     nop
        80095bc4 18 00 43 00     mult       v0,v1
        80095bc8 10 10 00 00     mfhi       v0
        80095bcc c2 1f 03 00     srl        v1,v1,0x1f
        80095bd0 43 11 02 00     sra        v0,v0,0x5
        80095bd4 21 88 43 00     addu       s1,v0,v1
                             LAB_80095bd8                                  
        80095bd8 39 00 01 2a     slti       at,s0,0x39
        80095bdc 35 00 20 14     bne        at,zero,0x80095cb4
        80095be0 00 00 00 00     _nop
        80095be4 75 00 01 2a     slti       at,s0,0x75
        80095be8 32 00 20 10     beq        at,zero,0x80095cb4
        80095bec 00 00 00 00     _nop
        80095bf0 13 80 01 3c     lui        at,0x8013
        80095bf4 48 f3 22 8c     lw         v0,-0xcb8(at)  //PartnerPtr                        
        80095bf8 00 00 00 00     nop
        80095bfc 13 00 42 16     bne        s2,v0,0x80095c4c
        80095c00 00 00 00 00     _nop
        80095c04 20 92 82 8f     lw         v0,-0x6de0(gp)  //CombatHead                      
        80095c08 00 00 00 00     nop
        80095c0c 68 06 42 90     lbu        v0,0x668(v0)  //FinisherFilled
        80095c10 00 00 00 00     nop
        80095c14 29 00 41 2c     sltiu      at,v0,0x29
        80095c18 19 00 20 14     bne        at,zero,0x80095c80
        80095c1c 21 18 40 00     _move      v1,v0
        80095c20 18 00 23 02     mult       s1,v1
        80095c24 66 66 02 3c     lui        v0,0x6666
        80095c28 12 18 00 00     mflo       v1
        80095c2c 67 66 42 34     ori        v0,v0,0x6667
        80095c30 00 00 00 00     nop
        80095c34 18 00 43 00     mult       v0,v1
        80095c38 10 10 00 00     mfhi       v0
        80095c3c c2 1f 03 00     srl        v1,v1,0x1f
        80095c40 03 11 02 00     sra        v0,v0,0x4
        80095c44 0e 00 00 10     b          0x80095c80
        80095c48 21 88 43 00     _addu      s1,v0,v1
                             LAB_80095c4c                                    
        80095c4c b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                       
        80095c50 65 00 04 24     _li        a0,0x65
        80095c54 64 00 42 20     addi       v0,v0,0x64
        80095c58 18 00 22 02     mult       s1,v0
        80095c5c eb 51 02 3c     lui        v0,0x51eb
        80095c60 12 18 00 00     mflo       v1
        80095c64 1f 85 42 34     ori        v0,v0,0x851f
        80095c68 00 00 00 00     nop
        80095c6c 18 00 43 00     mult       v0,v1
        80095c70 10 10 00 00     mfhi       v0
        80095c74 c2 1f 03 00     srl        v1,v1,0x1f
        80095c78 43 11 02 00     sra        v0,v0,0x5
        80095c7c 21 88 43 00     addu       s1,v0,v1
                             LAB_80095c80                                    
        80095c80 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                     
        80095c84 15 00 04 24     _li        a0,0x15
        80095c88 5a 00 42 20     addi       v0,v0,0x5a
        80095c8c 18 00 22 02     mult       s1,v0
        80095c90 eb 51 02 3c     lui        v0,0x51eb
        80095c94 12 18 00 00     mflo       v1
        80095c98 1f 85 42 34     ori        v0,v0,0x851f
        80095c9c 00 00 00 00     nop
        80095ca0 18 00 43 00     mult       v0,v1
        80095ca4 10 10 00 00     mfhi       v0
        80095ca8 c2 1f 03 00     srl        v1,v1,0x1f
        80095cac 43 11 02 00     sra        v0,v0,0x5
        80095cb0 21 88 43 00     addu       s1,v0,v1
                             LAB_80095cb4                                   
        80095cb4 57 00 42 82     lb         v0,0x57(s2)
        80095cb8 02 00 01 24     li         at,0x2
        80095cbc 03 00 41 14     bne        v0,at,0x80095ccc
        80095cc0 00 00 00 00     _nop
        80095cc4 83 18 11 00     sra        v1,s1,0x2
        80095cc8 22 88 23 02     sub        s1,s1,v1
                             LAB_80095ccc                                   
        80095ccc 02 00 20 1e     bgtz       s1,0x80095cd8
        80095cd0 00 00 00 00     _nop
        80095cd4 01 00 11 24     li         s1,0x1
                             LAB_80095cd8                                     
        80095cd8 57 00 82 82     lb         v0,0x57(s4)
        80095cdc 04 00 01 24     li         at,0x4
        80095ce0 03 00 41 14     bne        v0,at,0x80095cf0
        80095ce4 00 00 00 00     _nop
        80095ce8 83 18 11 00     sra        v1,s1,0x2
        80095cec 21 88 23 02     addu       s1,s1,v1
                             LAB_80095cf0                                   
        80095cf0 10 27 21 2a     slti       at,s1,0x2710
        80095cf4 02 00 20 14     bne        at,zero,0x80095d00
        80095cf8 00 00 00 00     _nop
        80095cfc 0f 27 11 24     li         s1,0x270f
                             LAB_80095d00                                   
        80095d00 21 10 20 02     move       v0,s1
        80095d04 24 00 bf 8f     lw         ra,0x24(sp)
        80095d08 1c 00 b3 8f     lw         s3,0x1c(sp)
        80095d0c 18 00 b2 8f     lw         s2,0x18(sp)
        80095d10 14 00 b1 8f     lw         s1,0x14(sp)
        80095d14 10 00 b0 8f     lw         s0,0x10(sp)
        80095d18 20 00 b4 8f     lw         s4,0x20(sp)
        80095d1c 08 00 e0 03     jr         ra
        80095d20 34 00 bd 27     _addiu     sp,sp,0x34


// Hardmode version
int CalculateDamage(DigimonEntity *AttackerPtr, DigimonEntity *DefenderPtr, int moveID)
{
//code ignored
    digimonID = AttackerPtr->DigimonEntity.DigimonType;
    if (digimonID < 66) 
    {
      if (moveID == RaiseData[digimonID].moveBoost) 
        attDamage = attDamage + RaiseData[digimonID].boostValue;
      
    }
    else 
      attDamage = attDamage + 100;

//code ignored

}


        80095af0 00 00 43 8e     lw         v1,0x0(s2)
        80095af4 00 00 00 00     nop
        80095af8 42 00 61 28     slti       at,v1,0x42
        80095afc 03 00 01 14     bne        zero,at,0x80095b0c
        80095b00 00 00 00 00     _nop
        80095b04 0e 00 00 10     b          0x80095b40
        80095b08 64 00 c6 24     _addiu     a2,a2,0x64
                             LAB_80095b0c                                     
        80095b0c c0 08 03 00     sll        at,v1,0x3
        80095b10 22 08 23 00     sub        at,at,v1
        80095b14 80 18 01 00     sll        v1,at,0x2
        80095b18 12 80 01 3c     lui        at,0x8012
        80095b1c c7 25 21 24     addiu      at,at,0x25c7
        80095b20 21 08 23 00     addu       at,at,v1
        80095b24 00 00 23 80     lb         v1,0x0(at)  //RaiseData[digimonID].moveBoost
        80095b28 00 00 00 00     nop
        80095b2c 04 00 03 16     bne        s0,v1,0x80095b40
        80095b30 00 00 00 00     _nop
        80095b34 03 00 23 84     lh         v1,0x3(at)  //RaiseData[digimonID].boostValue
        80095b38 00 00 00 00     nop
        80095b3c 20 30 c3 00     add        a2,a2,v1



// Hardcore version

int CalculateDamage(DigimonEntity *AttackerPtr, DigimonEntity *DefenderPtr, int moveID)
{
//code ignored
  //hardcore
    if (600 < attack) 
      attack = 600;
    
    if (attack < -600) 
      attack = -600;

// true hardcore
    if (700 < attack) 
      attack = 700;
    
    if (attack < -700) 
      attack = -700;

//same in both

    digimonID = AttackerPtr->DigimonEntity.DigimonType;
    if (digimonID < 66) 
    {
      if (moveID == RaiseData[digimonID].moveBoost) 
        attDamage = attDamage + RaiseData[digimonID].boostValue;
      
    }
//hardcore
    else 
      attDamage = attDamage + 200;

//true hardcore
    else 
      attDamage = attDamage + 300;

//code ignored

 //hardcore
    else 
    {
      rand = ReturnRandom(201);
      attDamage = (attDamage * (rand + 200)) / 100;
    }

//true hardcore
    else 
    {
      rand = ReturnRandom(201);
      attDamage = (attDamage * (rand + 300)) / 100;
    }

//code ignored
}

        //hardcore
        80095ab4 59 02 21 2a     slti       at,s1,0x259
        //true hardcore
        80095ab4 bc 02 21 2a     slti       at,s1,0x2bd
	
        //hardcore
        80095ac0 f4 01 11 24     li         s1,0x258
        //true hardcore
        80095ac0 bc 02 21 2a     li         s1,0x2bc

        //hardcore
        80095ac4 a7 fd 21 2a     slti       at,s1,-0x259
        //true hardcore
        80095ac4 43 fd 21 2a     slti       at,s1,-0x2bd
	
        //hardcore
        80095ad0 a8 fd 11 24     li         s1,-0x258
        //true hardcore
        80095ad0 44 fd 21 2a     li         s1,-0x2bc
                             


        80095af0 00 00 43 8e     lw         v1,0x0(s2)
        80095af4 00 00 00 00     nop
        80095af8 42 00 61 28     slti       at,v1,0x42
        80095afc 03 00 01 14     bne        zero,at,0x80095b0c
        80095b00 00 00 00 00     _nop
        80095b04 0e 00 00 10     b          0x80095b40
        //hardcore
        80095b08 c8 00 c6 24     _addiu     a2,a2,0xc8
        //true hardcore
        80095b08 2c 01 c6 24     _addiu     a2,a2,0x12c
                             LAB_80095b0c                                     
        80095b0c c0 08 03 00     sll        at,v1,0x3
        80095b10 22 08 23 00     sub        at,at,v1
        80095b14 80 18 01 00     sll        v1,at,0x2
        80095b18 12 80 01 3c     lui        at,0x8012
        80095b1c c7 25 21 24     addiu      at,at,0x25c7
        80095b20 21 08 23 00     addu       at,at,v1
        80095b24 00 00 23 80     lb         v1,0x0(at)  //RaiseData[digimonID].moveBoost
        80095b28 00 00 00 00     nop
        80095b2c 04 00 03 16     bne        s0,v1,0x80095b40
        80095b30 00 00 00 00     _nop
        80095b34 03 00 23 84     lh         v1,0x3(at)  //RaiseData[digimonID].boostValue
        80095b38 00 00 00 00     nop
        80095b3c 20 30 c3 00     add        a2,a2,v1

        //hardcore
        80095c50 c9 00 04 24     _li        a0,0xc9

        //hardcore
        80095c54 c8 00 42 20     addi       v0,v0,0xc8
        //true hardcore
        80095c54 2c 01 42 20     addi       v0,v0,0x12c

        

//ultra version

int CalculateDamage(DigimonEntity *AttackerPtr, DigimonEntity *DefenderPtr, int moveID)
{
//code ignored
    digimonID = AttackerPtr->DigimonEntity.DigimonType;
    if (digimonID != PartnerPtr) 
    {
      //ultra hardmode
      if (GameState[595] == 1) //is a boss battle
        attDamage = attDamage + 250;
      else 
        attDamage = attDamage + 150;

      //ultra hardcore
      if (GameState[595] == 1)
        attDamage = attDamage + 350;
      else 
        attDamage = attDamage + 250;

      //ultra true hardcore
      if (GameState[595] == 1) 
        attDamage = attDamage + 450;
      else 
        attDamage = attDamage + 350;
    }   

//code ignored
}

        80095afc 10 00 41 12     beq        s2,at,0x80095b40
        80095b00 00 00 00 00     _nop
        80095b04 8c 94 83 8f     lw         v1,-0x6b74(gp) //GameState
        80095b08 00 00 00 00     nop
        80095b0c fa 00 63 24     addiu      v1,v1,0xfa
        80095b10 59 01 63 90     lbu        v1,0x159(v1) //BossFlag
        80095b14 01 00 01 24     li         at,0x1
        80095b18 08 00 23 14     bne        at,v1,0x80095b3c
        80095b1c 00 00 00 00     _nop

//ultra hardmode
        80095b20 fa 00 c6 24     addiu      a2,a2,0xfa
//ultra hardcore
        80095b20 5e 01 c6 24     addiu      a2,a2,0x15e
//ultra true hardcore
        80095b20 c2 01 c6 24     addiu      a2,a2,0x1c2

        80095b24 06 00 00 10     b          0x80095b40
        80095b28 00 00 00 00     _nop
        80095b2c 00 00 00 00     nop
        80095b30 00 00 00 00     nop
        80095b34 00 00 00 00     nop
        80095b38 00 00 00 00     nop
                             LAB_80095b3c  
//ultra hardmode                                 
        80095b3c 96 00 c6 24     addiu      a2,a2,0x96
//ultra hardcore
        80095b3c fa 00 c6 24     addiu      a2,a2,0xfa
//ultra true hardcore
        80095b3c 5e 01 c6 24     addiu      a2,a2,0x15e




// This is a new funciton for the status effects, it works in the same way in the battles and tournaments but the addresses are different

void StatusEffectNew(DigimonEntity *AffectedPtr,FighterData *CombatPtr,int Tech4,int attPtr)
{
  byte value;
  BattleFlags BVar2;
  int iVar3;

    if ((CombatPtr->BattleFlags & 0x100) != 0) //Protection is active
    return;
  
  iVar3 = 0;
  if ((attPtr == PartnerPtr) && (attPtr = RaiseData[(AttackerPtr->DigimonEntity).DigimonType].moveBoost * 16, Tech4 == attPtr)) 
    iVar3 = 3;
  
  value = AllMovementData[iVar3 + Tech4];  //Either the normal effect chance or the boosted effect chance
  if (value == 0) 
    return;
  
  iVar3 = ReturnRandom(100);
  if (value <= iVar3) 
    return;
  
  iVar3 = 0;
  if (attPtr == Tech4) 
    iVar3 = 4;
  
  value = AllMovementData[iVar3 + Tech4]; //Either the normal effect or the boosted effect

  if (value == 4) //Flatten
  {
    if ((CombatPtr->BattleFlags & 8) == 0) //is flatten
    {
      CombatPtr->flattenTimer = -1;
      RemoveEffectSprites(AffectedPtr,&CombatPtr);
      ResetDumbCooldownTimers(CombatPtr);
    }   
  }
  else if (value == 3) //Stun
  {
    if (((ushort)CombatPtr->BattleFlags & 4) == 0) //is stunned
    {
      CombatPtr->BattleFlags = CombatPtr->BattleFlags | 4; //set the stun
      iVar3 = ReturnRandom(41);
      CombatPtr->stunTimer = iVar3 + 200;
      SetStunFX(AffectedPtr,CombatPtr);
      ResetDumbCooldownTimers(CombatPtr);
    }    
  }
  else if (value == 2) //Confusion
  {
    if (((ushort)CombatPtr->BattleFlags & 2) == 0) //is confused
    {
      CombatPtr->BattleFlags = CombatPtr->BattleFlags | 2; //set the confusion
      iVar3 = ReturnRandom(101);
      CombatPtr->confusedTimer = iVar3 + 200;
      SetConfusionFX(AffectedPtr,CombatPtr);
      ResetDumbCooldownTimers(CombatPtr);
    }    
  }
  else ((value < 8) && ((CombatPtr->BattleFlags & 1) != 0)) 
  {
    if (value == 1 || value == 5) 
    {
      if (value == 1)
      AffectedPtr->NewEffects = 1;
      else if (value == 5)
      AffectedPtr->NewEffects = 2;
      CombatPtr->BattleFlags = BVar2 | 1;
      CombatPtr->poisonTimer = 100;
    }
    else 
    {
      if (value == 6) 
      {
        AffectedPtr->NewEffects = 4;
        CombatPtr->BattleFlags = BVar2 | 1;
      }
      else if (value == 7)
      {        
        AffectedPtr->NewEffects = 8;
        CombatPtr->BattleFlags = BVar2 | 1;
      }
    }
    SetPoisonFX(AffectedPtr,&CombatPtr);
  }
  if (CombatPtr == *CombatHead) //fighter is your partner
      CombatHead.statusCount++;
  
  return;
}


                             
 //battle offset                                                       
                             StatusEffectNew                                
        8005beb8 d4 ff bd 27     addiu      sp,sp,-0x2c
        8005bebc 20 00 bf af     sw         ra,0x20(sp)
        8005bec0 1c 00 b3 af     sw         s3,0x1c(sp)
        8005bec4 18 00 b2 af     sw         s2,0x18(sp)
        8005bec8 14 00 b1 af     sw         s1,0x14(sp)
        8005becc 10 00 b0 af     sw         s0,0x10(sp)
        8005bed0 24 00 b4 af     sw         s4,0x24(sp)
        8005bed4 21 80 a0 00     move       s0,a1
        8005bed8 34 00 02 96     lhu        v0,0x34(s0)
        8005bedc 21 a0 07 00     move       s4,a3
        8005bee0 00 01 42 30     andi       v0,v0,0x100
        8005bee4 99 00 40 14     bne        v0,zero,0x8005c14c
        8005bee8 21 88 80 00     _move      s1,a0
        8005beec 13 80 02 3c     lui        v0,0x8013
        8005bef0 48 f3 42 8c     lw         v0,-0xcb8(v0)  //DigimonID                    
        8005bef4 21 18 00 00     clear      v1
        8005bef8 10 00 82 16     bne        s4,v0,0x8005bf3c
        8005befc 21 20 00 00     _clear     a0
        8005bf00 00 00 82 8e     lw         v0,0x0(s4)
        8005bf04 00 00 00 00     nop
        8005bf08 c0 08 02 00     sll        at,v0,0x3
        8005bf0c 22 08 22 00     sub        at,at,v0
        8005bf10 80 10 01 00     sll        v0,at,0x2
        8005bf14 12 80 01 3c     lui        at,0x8012
        8005bf18 c7 25 21 24     addiu      at,at,0x25c7
        8005bf1c 21 08 22 00     addu       at,at,v0
        8005bf20 00 00 22 80     lb         v0,0x0(at)  //RaiseData[DigimonID].moveBoost
        8005bf24 00 00 00 00     nop
        8005bf28 00 11 02 00     sll        v0,v0,0x4
        8005bf2c 03 00 c2 14     bne        a2,v0,0x8005bf3c
        8005bf30 21 a0 02 00     _move      s4,v0
        8005bf34 03 00 03 24     li         v1,0x3
        8005bf38 00 00 04 24     li         a0,0x0
                             LAB_8005bf3c                                    
        8005bf3c 12 80 02 3c     lui        v0,0x8012
        8005bf40 48 62 42 24     addiu      v0,v0,0x6248
        8005bf44 21 10 46 00     addu       v0,v0,a2
        8005bf48 21 10 43 00     addu       v0,v0,v1
        8005bf4c 00 00 42 90     lbu        v0,0x0(v0)  //AllMovementData[iVar3 + Tech4]                    
        8005bf50 21 98 06 00     move       s3,a2
        8005bf54 7d 00 40 10     beq        v0,zero,0x8005c14c
        8005bf58 21 90 40 00     _move      s2,v0
        8005bf5c 21 90 44 02     addu       s2,s2,a0
        8005bf60 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                     
        8005bf64 64 00 04 24     _li        a0,0x64
        8005bf68 2a 08 52 00     slt        at,v0,s2
        8005bf6c 77 00 20 10     beq        at,zero,0x8005c14c
        8005bf70 00 00 00 00     _nop
        8005bf74 21 18 00 00     clear      v1
        8005bf78 02 00 93 16     bne        s4,s3,0x8005bf84
        8005bf7c 00 00 00 00     _nop
        8005bf80 04 00 03 24     li         v1,0x4

                             LAB_8005bf84                                  
        8005bf84 12 80 02 3c     lui        v0,0x8012
        8005bf88 46 62 42 24     addiu      v0,v0,0x6246
        8005bf8c 21 10 53 00     addu       v0,v0,s3
        8005bf90 21 10 43 00     addu       v0,v0,v1
        8005bf94 00 00 42 90     lbu        v0,0x0(v0)  //AllMovementData[iVar3 + Tech4]                     
        8005bf98 04 00 01 24     li         at,0x4
        8005bf9c 57 00 41 10     beq        v0,at,0x8005c0fc
        8005bfa0 00 00 00 00     _nop
        8005bfa4 03 00 01 24     li         at,0x3
        8005bfa8 42 00 41 10     beq        v0,at,0x8005c0b4
        8005bfac 00 00 00 00     _nop
        8005bfb0 02 00 01 24     li         at,0x2
        8005bfb4 2d 00 41 10     beq        v0,at,0x8005c06c
        8005bfb8 00 00 00 00     _nop
        8005bfbc 08 00 41 2c     sltiu      at,v0,0x8
        8005bfc0 5a 00 01 10     beq        zero,at,0x8005c12c
        8005bfc4 00 00 00 00     _nop
        8005bfc8 34 00 03 96     lhu        v1,0x34(s0)
        8005bfcc 00 00 00 00     nop
        8005bfd0 01 00 61 30     andi       at,v1,0x1
        8005bfd4 55 00 20 14     bne        at,zero,0x8005c12c
        8005bfd8 00 00 00 00     _nop
        8005bfdc 01 00 01 24     li         at,0x1
        8005bfe0 04 00 41 14     bne        v0,at,0x8005bff4
        8005bfe4 00 00 00 00     _nop
        8005bfe8 01 00 02 24     li         v0,0x1
        8005bfec 06 00 00 10     b          0x8005c008
        8005bff0 57 00 22 a2     _sb        v0,0x57(s1)
                             LAB_8005bff4                                    
        8005bff4 05 00 01 24     li         at,0x5
        8005bff8 08 00 41 14     bne        v0,at,0x8005c01c
        8005bffc 00 00 00 00     _nop
        8005c000 02 00 02 24     li         v0,0x2
        8005c004 57 00 22 a2     sb         v0,0x57(s1)
                             LAB_8005c008                                   
        8005c008 01 00 62 34     ori        v0,v1,0x1
        8005c00c 34 00 02 a6     sh         v0,0x34(s0)
        8005c010 64 00 02 24     li         v0,0x64
        8005c014 10 00 00 10     b          0x8005c058
        8005c018 1c 00 02 a6     _sh        v0,0x1c(s0)
                             LAB_8005c01c                                    
        8005c01c 06 00 01 24     li         at,0x6
        8005c020 06 00 41 14     bne        v0,at,0x8005c03c
        8005c024 00 00 00 00     _nop
        8005c028 04 00 02 24     li         v0,0x4
        8005c02c 57 00 22 a2     sb         v0,0x57(s1)
        8005c030 01 00 62 34     ori        v0,v1,0x1
        8005c034 08 00 00 10     b          0x8005c058
        8005c038 34 00 02 a6     _sh        v0,0x34(s0)
                             LAB_8005c03c                                    
        8005c03c 07 00 01 24     li         at,0x7
        8005c040 3a 00 22 14     bne        at,v0,0x8005c12c
        8005c044 00 00 00 00     _nop
        8005c048 08 00 02 24     li         v0,0x8
        8005c04c 57 00 22 a2     sb         v0,0x57(s1)
        8005c050 01 00 62 34     ori        v0,v1,0x1
        8005c054 34 00 02 a6     sh         v0,0x34(s0)
                             LAB_8005c058                                    
        8005c058 21 20 11 00     move       a0,s1
        8005c05c ca 7a 01 0c     jal        0x8005eb28  //SetPoisonFX                                      
        8005c060 21 28 00 02     _move      a1,s0
        8005c064 31 00 00 10     b          0x8005c12c
        8005c068 00 00 00 00     _nop
                             LAB_8005c06c                                   
        8005c06c 34 00 03 96     lhu        v1,0x34(s0)
        8005c070 00 00 00 00     nop
        8005c074 02 00 62 30     andi       v0,v1,0x2
        8005c078 2c 00 40 14     bne        v0,zero,0x8005c12c
        8005c07c 00 00 00 00     _nop
        8005c080 02 00 62 34     ori        v0,v1,0x2
        8005c084 34 00 02 a6     sh         v0,0x34(s0)
        8005c088 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                     
        8005c08c 65 00 04 24     _li        a0,0x65
        8005c090 c8 00 42 20     addi       v0,v0,0xc8
        8005c094 1e 00 02 a6     sh         v0,0x1e(s0)
        8005c098 21 20 20 02     move       a0,s1
        8005c09c dc 7a 01 0c     jal        0x8005eb70  //SetConfusionFX                                  
        8005c0a0 21 28 00 02     _move      a1,s0
        8005c0a4 c3 7a 01 0c     jal        0x8005eb0c //ResetDumbCooldownTimers                         
        8005c0a8 21 20 00 02     _move      a0,s0
        8005c0ac 1f 00 00 10     b          0x8005c12c
        8005c0b0 00 00 00 00     _nop
                             LAB_8005c0b4                                   
        8005c0b4 34 00 03 96     lhu        v1,0x34(s0)
        8005c0b8 00 00 00 00     nop
        8005c0bc 04 00 62 30     andi       v0,v1,0x4
        8005c0c0 1a 00 40 14     bne        v0,zero,0x8005c12c
        8005c0c4 00 00 00 00     _nop
        8005c0c8 04 00 62 34     ori        v0,v1,0x4
        8005c0cc 34 00 02 a6     sh         v0,0x34(s0)
        8005c0d0 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                   
        8005c0d4 29 00 04 24     _li        a0,0x29
        8005c0d8 c8 00 42 20     addi       v0,v0,0xc8
        8005c0dc 20 00 02 a6     sh         v0,0x20(s0)
        8005c0e0 21 20 20 02     move       a0,s1
        8005c0e4 f9 7a 01 0c     jal        0x8005ebe4  //SetStunFX                                     
        8005c0e8 21 28 00 02     _move      a1,s0
        8005c0ec c3 7a 01 0c     jal        0x8005eb0c //ResetDumbCooldownTimers                         
        8005c0f0 21 20 00 02     _move      a0,s0
        8005c0f4 0d 00 00 10     b          0x8005c12c
        8005c0f8 00 00 00 00     _nop
                             LAB_8005c0fc                                  
        8005c0fc 34 00 02 96     lhu        v0,0x34(s0)
        8005c100 00 00 00 00     nop
        8005c104 08 00 42 30     andi       v0,v0,0x8
        8005c108 08 00 40 14     bne        v0,zero,0x8005c12c
        8005c10c 00 00 00 00     _nop
        8005c110 ff ff 02 24     li         v0,-0x1
        8005c114 22 00 02 a6     sh         v0,0x22(s0)
        8005c118 21 20 20 02     move       a0,s1
        8005c11c 1f 7b 01 0c     jal        0x8005ec7c  //RemoveEffectSprites                              
        8005c120 21 28 00 02     _move      a1,s0
        8005c124 c3 7a 01 0c     jal        0x8005eb0c //ResetDumbCooldownTimers                          
        8005c128 21 20 00 02     _move      a0,s0
                             LAB_8005c12c                                   
        8005c12c 20 92 82 8f     lw         v0=>CombatHeadAddr,-0x6de0(gp)  //CombatHead
        8005c130 00 00 00 00     nop
        8005c134 05 00 02 16     bne        s0,v0,0x8005c14c
        8005c138 21 18 40 00     _move      v1,v0
        8005c13c 44 06 62 94     lhu        v0,0x644(v1)  //CombatHead.statusCount
        8005c140 00 00 00 00     nop
        8005c144 01 00 42 24     addiu      v0,v0,0x1
        8005c148 44 06 62 a4     sh         v0,0x644(v1)
                             LAB_8005c14c                                                                                                                              
        8005c14c 20 00 bf 8f     lw         ra,0x20(sp)
        8005c150 1c 00 b3 8f     lw         s3,0x1c(sp)
        8005c154 18 00 b2 8f     lw         s2,0x18(sp)
        8005c158 14 00 b1 8f     lw         s1,0x14(sp)
        8005c15c 10 00 b0 8f     lw         s0,0x10(sp)
        8005c160 24 00 b4 8f     lw         s4,0x24(sp)
        8005c164 08 00 e0 03     jr         ra
        8005c168 2c 00 bd 27     _addiu     sp,sp,0x2c



//Remove status effect boost

void StatusEffectNew(DigimonEntity *AffectedPtr,FighterData *CombatPtr,int Tech4,int attPtr)
{
  if ((CombatPtr->BattleFlags & 0x100) != 0) 
    return;
  
  value = AllMovementData[Tech4].effectChance;
  if (value == 0) {
    return;
  }
  iVar3 = ReturnRandom(100);
  if (value < iVar3) {
    return;
  }
  value = AllMovementData[Tech4].effect;

//code ignored
}
                             
                             StatusEffectNew                                  
        8005beb8 d4 ff bd 27     addiu      sp,sp,-0x2c
        8005bebc 20 00 bf af     sw         ra,0x20(sp)
        8005bec0 1c 00 b3 af     sw         s3,0x1c(sp)
        8005bec4 18 00 b2 af     sw         s2,0x18(sp)
        8005bec8 14 00 b1 af     sw         s1,0x14(sp)
        8005becc 10 00 b0 af     sw         s0,0x10(sp)
        8005bed0 24 00 b4 af     sw         s4,0x24(sp)
        8005bed4 21 80 a0 00     move       s0,a1
        8005bed8 34 00 02 96     lhu        v0,0x34(s0)
        8005bedc 21 a0 07 00     move       s4,a3
        8005bee0 00 01 42 30     andi       v0,v0,0x100
        8005bee4 99 00 40 14     bne        v0,zero,0x8005c14c
        8005bee8 21 88 80 00     _move      s1,a0
        8005beec 21 20 00 00     clear      a0
        8005bef0 12 00 00 10     b          0x8005bf3c     //jumps to ignored code
        8005bef4 21 18 00 00     _clear     v1
        8005bef8 10 00 82 16     bne        s4,v0,0x8005bf3c
        8005befc 21 20 00 00     _clear     a0
        8005bf00 00 00 82 8e     lw         v0,0x0(s4)
        8005bf04 00 00 00 00     nop
        8005bf08 c0 08 02 00     sll        at,v0,0x3
        8005bf0c 22 08 22 00     sub        at,at,v0
        8005bf10 80 10 01 00     sll        v0,at,0x2
        8005bf14 12 80 01 3c     lui        at,0x8012
        8005bf18 c7 25 21 24     addiu      at,at,0x25c7
        8005bf1c 21 08 22 00     addu       at,at,v0
        8005bf20 00 00 22 80     lb         v0,0x0(at)     
        8005bf24 00 00 00 00     nop
        8005bf28 00 11 02 00     sll        v0,v0,0x4
        8005bf2c 03 00 c2 14     bne        a2,v0,0x8005bf3c
        8005bf30 21 a0 02 00     _move      s4,v0
        8005bf34 03 00 03 24     li         v1,0x3
        8005bf38 00 00 04 24     li         a0,0x0
                             LAB_8005bf3c                                   
        8005bf3c 12 80 02 3c     lui        v0,0x8012
        8005bf40 48 62 42 24     addiu      v0,v0,0x6248
        8005bf44 21 10 46 00     addu       v0,v0,a2
        8005bf48 21 10 43 00     addu       v0,v0,v1
        8005bf4c 00 00 42 90     lbu        v0,0x0(v0)  //AllMovementData[Tech4]                          
        8005bf50 21 98 06 00     move       s3,a2
        8005bf54 7d 00 40 10     beq        v0,zero,0x8005c14c
        8005bf58 21 90 40 00     _move      s2,v0
        8005bf5c 21 90 44 02     addu       s2,s2,a0
        8005bf60 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                    
        8005bf64 64 00 04 24     _li        a0,0x64
        8005bf68 2a 08 42 02     slt        at,s2,v0
        8005bf6c 77 00 20 14     bne        at,zero,0x8005c14c
        8005bf70 00 00 00 00     _nop
        8005bf74 21 18 00 00     clear      v1
        8005bf78 02 00 93 16     bne        s4,s3,0x8005bf84
        8005bf7c 00 00 00 00     _nop
        8005bf80 21 18 00 00     clear      v1


//Hardmode/Hardcore

 void NewStatusEffect(DigimonEntity *AffectedPtr,FighterData *CombatPtr,int Tech4,int attPtr)
{
  if (((ushort)combatPtr->BattleFlags & 0x100) != 0) 
    return;
  
  iVar4 = 0;
  iVar3 = (attPtr->Entity).DigimonType;
  
  //hardmode
  iVar5 = 10;
  
  //hardcore
  iVar5 = 20;
  
  //true hardcore
  iVar5 = 30;
  
  if (iVar3 < 66)
  {
    iVar5 = 0;
    attPtr = RaiseData[iVar3].moveBoost << 4);
	
    if (Tech4 == attPtr) 
      iVar4 = 3;    
  }
  
  value = AllMovementData[iVar4 + Tech4];
  if (value == 0) 
    return;
  
  iVar3 = ReturnRandom(100);
  if ((value + iVar5) <= iVar3) 
    return;
  
  iVar3 = 0;
  if (attPtr == Tech4) 
    iVar3 = 4;
} 

//tournament offset
                             NewStatusEffect                                
        80063ea0 d4 ff bd 27     addiu      sp,sp,-0x2c
        80063ea4 20 00 bf af     sw         ra,0x20(sp)
        80063ea8 1c 00 b3 af     sw         s3,0x1c(sp)
        80063eac 18 00 b2 af     sw         s2,0x18(sp)
        80063eb0 14 00 b1 af     sw         s1,0x14(sp)
        80063eb4 10 00 b0 af     sw         s0,0x10(sp)
        80063eb8 24 00 b4 af     sw         s4,0x24(sp)
        80063ebc 21 80 a0 00     move       s0,a1
        80063ec0 34 00 02 96     lhu        v0,0x34(s0)
        80063ec4 21 a0 07 00     move       s4,a3
        80063ec8 00 01 42 30     andi       v0,v0,0x100
        80063ecc 99 00 40 14     bne        v0,zero,0x80064134
        80063ed0 21 88 80 00     _move      s1,a0
        80063ed4 00 00 00 00     nop
        80063ed8 00 00 e2 8c     lw         v0,0x0(a3) attPtr->Entity.DigimonType
        80063edc 21 18 00 00     clear      v1
        80063ee0 00 00 00 00     nop
		//hardmode
		80063ee4 0a 00 04 24     li         a0,0xa
  
        //hardcore
		80063ee4 14 00 04 24     li         a0,0x14
  
        //true hardcore
		80063ee4 1e 00 04 24     li         a0,0x1e

        80063ee8 42 00 41 28     slti       at,v0,0x42
        80063eec 0d 00 20 10     beq        at,zero,0x80063f24
        80063ef0 00 00 00 00     _nop
        80063ef4 c0 08 02 00     sll        at,v0,0x3
        80063ef8 22 08 22 00     sub        at,at,v0
        80063efc 80 10 01 00     sll        v0,at,0x2
        80063f00 12 80 01 3c     lui        at,0x8012
        80063f04 c7 25 21 24     addiu      at,at,0x25c7
        80063f08 21 08 22 00     addu       at,at,v0
        80063f0c 00 00 22 80     lb         v0,0x0(at)  //RaiseData[(AttackerPtr->DigimonEntity).DigimonType]
        80063f10 21 20 00 00     clear      a0
        80063f14 00 11 02 00     sll        v0,v0,0x4
        80063f18 02 00 c2 14     bne        a2,v0,0x80063f24
        80063f1c 21 a0 02 00     _move      s4,v0
        80063f20 03 00 03 24     li         v1,0x3
                             LAB_80063f24                                   
        80063f24 12 80 02 3c     lui        v0,0x8012
        80063f28 48 62 42 24     addiu      v0,v0,0x6248
        80063f2c 21 10 46 00     addu       v0,v0,a2
        80063f30 21 10 43 00     addu       v0,v0,v1
        80063f34 00 00 42 90     lbu        v0,0x0(v0)  //AllMovementData[iVar4 + Tech4]                       
        80063f38 21 98 06 00     move       s3,a2
        80063f3c 7d 00 40 10     beq        v0,zero,0x80064134
        80063f40 21 90 40 00     _move      s2,v0
        80063f44 21 90 44 02     addu       s2,s2,a0
        80063f48 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                   
        80063f4c 64 00 04 24     _li        a0,0x64
        80063f50 2a 08 52 00     slt        at,v0,s2
        80063f54 77 00 20 10     beq        at,zero,0x80064134
        80063f58 00 00 00 00     _nop
        80063f5c 21 18 00 00     clear      v1
        80063f60 02 00 93 16     bne        s4,s3,0x80063f6c
        80063f64 00 00 00 00     _nop
        80063f68 04 00 03 24     li         v1,0x4




//Ultra effect

void NewStatusEffect(DigimonEntity *AffectedPtr,FighterData *CombatPtr,int Tech4,int attPtr)
{
  if (((ushort)combatPtr->BattleFlags & 0x100) != 0) 
    return;
  
  iVar3 = 0;
  iVar4 = 0;
  if (attPtr != PartnerPtr)
  {
    iVar3 = 3;
	
    //hardmode
    iVar4 = 10;
  
    //hardcore
    iVar4 = 20;
  
    //true hardcore
    iVar4 = 30;
	
    attPtr = Tech4;
    if (GameState[595] == 1) //is a boss battle
      //hardmode
      iVar4 = 20;
  
      //hardcore
      iVar4 = 30;
  
      //true hardcore
      iVar4 = 40;
    
  }
  value = AllMovementData[iVar3 + Tech4];
  if (value == 0) 
    return;
  
  iVar3 = ReturnRandom(100);
  if ((value + iVar4) <= iVar3) 
    return;
}



                             NewStatusEffect                               
        80063ea0 d4 ff bd 27     addiu      sp,sp,-0x2c
        80063ea4 20 00 bf af     sw         ra,0x20(sp)
        80063ea8 1c 00 b3 af     sw         s3,0x1c(sp)
        80063eac 18 00 b2 af     sw         s2,0x18(sp)
        80063eb0 14 00 b1 af     sw         s1,0x14(sp)
        80063eb4 10 00 b0 af     sw         s0,0x10(sp)
        80063eb8 24 00 b4 af     sw         s4,0x24(sp)
        80063ebc 21 80 a0 00     move       s0,a1
        80063ec0 34 00 02 96     lhu        v0,0x34(s0)
        80063ec4 21 a0 07 00     move       s4,a3
        80063ec8 00 01 42 30     andi       v0,v0,0x100
        80063ecc 99 00 40 14     bne        v0,zero,0x80064134
        80063ed0 21 88 80 00     _move      s1,a0
        80063ed4 13 80 02 3c     lui        v0,0x8013
        80063ed8 48 f3 42 8c     lw         v0,-0xcb8(v0)  //PartnerID                
        80063edc 21 18 00 00     clear      v1
        80063ee0 10 00 82 12     beq        s4,v0,0x80063f24
        80063ee4 21 20 00 00     _clear     a0
        80063ee8 00 00 00 00     nop
        80063eec 8c 94 82 8f     lw         v0,-0x6b74(gp) //GameState
        80063ef0 00 00 00 00     nop
        80063ef4 fa 00 42 24     addiu      v0,v0,0xfa
        80063ef8 59 01 42 90     lbu        v0,0x159(v0)
        80063efc 01 00 01 24     li         at,0x1
        80063f00 03 00 03 24     li         v1,0x3
		//hardmode
		80063f04 0a 00 04 24     li         a0,0xa
  
        //hardcore
		80063f04 14 00 04 24     li         a0,0x14
  
        //true hardcore
		80063f04 1e 00 04 24     li         a0,0x1e

        80063f08 06 00 41 14     bne        v0,at,0x80063f24
        80063f0c 21 a0 06 00     _move      s4,a2
		//hardmode
        80063f10 14 00 04 24     li         a0,0x14
  
        //hardcore
        80063f10 1c 00 04 24     li         a0,0x1e
  
        //true hardcore
		80063f10 28 00 04 24     li         a0,0x28

        80063f14 03 00 00 10     b          0x80063f24
        80063f18 00 00 00 00     _nop
        80063f1c 00 00 00 00     nop
        80063f20 00 00 00 00     nop
		
		
//Render the new effects

//Sorry, not the full function, only the changes

RenderBattleBubble(param_1)
{
  if ((0x200 < depth) && (depth < 0x10000)) 
  {
    PoisonBubble.UvX = BubblePosX[(FramesAlive / 2) % 6] + 0x20;
                   
    sVar3 = 487;
    cVar1 = DigimonEntity->NewEffects;
    if (cVar1 != '\0') 
	{
      if (cVar1 == 2) 
        sVar3 = 482;
      
      if (cVar1 == 4) 
        sVar3 = 493;
      
      if (cVar1 == 11) 
        sVar3 = 494;      
                    
      PoisonBubble.ClutY = sVar3;
      RenderSprite(&PoisonBubble, posX, posY, depth, 20132, 20132);	  
   }
}

                          RenderBattleBubble									
	    //battle
	    8006eed4 21 90 00 02     move       s2,s0		

        8006ef2c 10 00 b1 af     sw         s1,local_28(sp)
		
		8006efb8 43 18 11 00     sra        v1,s1,0x1

        8006eff8 5c b4 00 10     b          0x8005c16c
		
		                    LAB_8005c16c                                    
        8005c16c 08 00 42 8e     lw         v0,0x8(s2)
        8005c170 e7 01 03 24     li         v1,0x1e7
        8005c174 57 00 42 80     lb         v0,0x57(v0)
        8005c178 02 00 01 24     li         at,0x2
        8005c17c 0d 00 02 10     beq        zero,v0,0x8005c1b4
        8005c180 00 00 00 00     _nop
        8005c184 02 00 22 14     bne        at,v0,0x8005c190
        8005c188 04 00 01 24     _li        at,0x4
        8005c18c e2 01 03 24     li         v1,0x1e2
                             LAB_8005c190                                   
        8005c190 02 00 22 14     bne        at,v0,0x8005c19c
        8005c194 08 00 01 24     _li        at,0x8
        8005c198 ed 01 03 24     li         v1,0x1ed
                             LAB_8005c19c                                   
        8005c19c 02 00 22 14     bne        at,v0,0x8005c1a8
        8005c1a0 00 00 00 00     _nop
        8005c1a4 ee 01 03 24     li         v1,0x1ee
                             LAB_8005c1a8                                   
        8005c1a8 12 00 83 a4     sh         v1,0x12(a0)
        8005c1ac db 68 03 0c     jal        0x800da36c   //RenderSprite                                   
        8005c1b0 00 00 00 00     _nop
                             LAB_8005c1b4                                  
        8005c1b4 92 4b 00 10     b          0x8006f000
        8005c1b8 00 00 00 00     _nop



        //tournament

		80077474 21 90 00 02     move       s2,s0
		
		800774cc 10 00 b1 af     sw         s1,0x10(sp)

        80077558 43 18 11 00     sra        v1,s1,0x1

        80077598 ee b2 00 10     b          0x80064154
		
		                             LAB_80064154                                  
        80064154 08 00 42 8e     lw         v0,0x8(s2)
        80064158 e7 01 03 24     li         v1,0x1e7
        8006415c 57 00 42 80     lb         v0,0x57(v0)
        80064160 02 00 01 24     li         at,0x2
        80064164 0d 00 02 10     beq        zero,v0,0x8006419c
        80064168 00 00 00 00     _nop
        8006416c 02 00 22 14     bne        at,v0,0x80064178
        80064170 04 00 01 24     _li        at,0x4
        80064174 e2 01 03 24     li         v1,0x1e2
                             LAB_80064178                                  
        80064178 02 00 22 14     bne        at,v0,0x80064184
        8006417c 08 00 01 24     _li        at,0x8
        80064180 ed 01 03 24     li         v1,0x1ed
                             LAB_80064184                                    
        80064184 02 00 22 14     bne        at,v0,0x80064190
        80064188 00 00 00 00     _nop
        8006418c ee 01 03 24     li         v1,0x1ee
                             LAB_80064190                                   
        80064190 12 00 83 a4     sh         v1,0x12(a0)
        80064194 db 68 03 0c     jal        0x800da36c   //RenderSprite                                   
        80064198 00 00 00 00     _nop
                             LAB_8006419c                                   
        8006419c 00 4d 00 10     b          0x800775a0
        800641a0 00 00 00 00     _nop


//New function

int CalculateDamageRandom3(stats *stats)

{
  int iVar1;
  
  iVar1 = ReturnRandom(3);
  return ((int)stats->MaxDigimonHP * (iVar1 + 1)) / 100;
}

                            
//battle version
                             CalculateDamageRandom3                          
        8005c1bc e8 ff bd 27     addiu      sp,sp,-0x18
        8005c1c0 10 00 bf af     sw         ra,0x10(sp)
        8005c1c4 08 00 b0 af     sw         s0,0x8(sp)
        8005c1c8 00 00 00 00     nop
        8005c1cc 21 80 04 00     move       s0,stats
        8005c1d0 b5 8d 02 0c     jal        0x800a36d4 //ReturnRandom                                    
        8005c1d4 03 00 04 24     _li        stats,0x3
        8005c1d8 10 00 03 86     lh         v1,0x10(s0)
        8005c1dc 01 00 42 20     addi       v0,v0,0x1
        8005c1e0 18 00 62 00     mult       v1,v0
        8005c1e4 eb 51 02 3c     lui        v0,0x51eb
        8005c1e8 12 18 00 00     mflo       v1
        8005c1ec 1f 85 42 34     ori        v0,v0,0x851f
        8005c1f0 00 00 00 00     nop
        8005c1f4 18 00 43 00     mult       v0,v1
        8005c1f8 10 10 00 00     mfhi       v0
        8005c1fc c2 1f 03 00     srl        v1,v1,0x1f
        8005c200 43 11 02 00     sra        v0,v0,0x5
        8005c204 21 10 43 00     addu       v0,v0,v1
        8005c208 10 00 bf 8f     lw         ra,0x10(sp)
        8005c20c 08 00 b0 8f     lw         s0,0x8(sp)
        8005c210 08 00 e0 03     jr         ra
        8005c214 18 00 bd 27     _addiu     sp,sp,0x18

// the tournament version is the same, but located at 0x800641a4


//Another new function

void ResetspeedBuffer(short Damage, FighterData *fighter, DigimonEntity *digi)
{
  short sVar1;
  short sVar2;
  
  sVar1 = fighter->speedBuffer - Damage;
  
  if (digi->NewEffects == 8) //is shocked
  {
    sVar1 = sVar1 - Damage;
	//battle 
    sVar2 = -620;
	//tournament
	sVar2 = -400;
  }
  else 
    sVar2 = -155;
  
  fighter->speedBuffer = sVar1;
  
  if (fighter->speedBuffer < sVar2) 
    fighter->speedBuffer = sVar2;
  
  return;
}

//battle version
                             ResetspeedBuffer                               
        8005c46c e8 ff bd 27     addiu      sp,sp,-0x18
        8005c470 10 00 bf af     sw         ra,0x10(sp)
        8005c474 32 00 a2 84     lh         v0,0x32(a1)
        8005c478 00 00 00 00     nop
        8005c47c 22 10 44 00     sub        v0,v0,a0
        8005c480 57 00 c3 80     lb         v1,0x57(a2)
        8005c484 08 00 01 24     li         at,0x8
        8005c488 04 00 23 14     bne        at,v1,0x8005c49c
        8005c48c 00 00 00 00     _nop
        8005c490 22 10 44 00     sub        v0,v0,a0
        8005c494 02 00 00 10     b          0x8005c4a0
        8005c498 94 fd 04 24     _li        a0,-0x26c
                             LAB_8005c49c                                   
        8005c49c 65 ff 04 24     li         a0,-0x9b
                             LAB_8005c4a0                                  
        8005c4a0 32 00 a2 a4     sh         v0,0x32(a1)
        8005c4a4 32 00 a2 84     lh         v0,0x32(a1)
        8005c4a8 00 00 00 00     nop
        8005c4ac 2a 08 44 00     slt        at,v0,a0
        8005c4b0 02 00 01 10     beq        zero,at,0x8005c4bc
        8005c4b4 00 00 00 00     _nop
        8005c4b8 32 00 a4 a4     sh         a0,0x32(a1)
                             LAB_8005c4bc                                    
        8005c4bc 10 00 bf 8f     lw         ra,0x10(sp)
        8005c4c0 00 00 00 00     nop
        8005c4c4 08 00 e0 03     jr         ra
        8005c4c8 18 00 bd 27     _addiu     sp,sp,0x18
		

//tournament version
                             ResetSpeedBuffer                                
        80064454 e8 ff bd 27     addiu      sp,sp,-0x18
        80064458 10 00 bf af     sw         ra,0x10(sp)
        8006445c 32 00 a2 84     lh         v0,0x32(a1)
        80064460 00 00 00 00     nop
        80064464 22 10 44 00     sub        v0,v0,a0
        80064468 57 00 c3 80     lb         v1,0x57(a2)
        8006446c 08 00 01 24     li         at,0x8
        80064470 04 00 23 14     bne        at,v1,0x80064484
        80064474 00 00 00 00     _nop
        80064478 22 10 44 00     sub        v0,v0,a0
        8006447c 02 00 00 10     b          0x80064488
        80064480 70 fe 04 24     _li        a0,-0x190
                             LAB_80064484                                     
        80064484 65 ff 04 24     li         a0,-0x9b
                             LAB_80064488                                   
        80064488 32 00 a2 a4     sh         v0,0x32(a1)
        8006448c 32 00 a2 84     lh         v0,0x32(a1)
        80064490 00 00 00 00     nop
        80064494 2a 08 44 00     slt        at,v0,a0
        80064498 02 00 01 10     beq        zero,at,0x800644a4
        8006449c 00 00 00 00     _nop
        800644a0 32 00 a4 a4     sh         a0,0x32(a1)
                             LAB_800644a4                                    
        800644a4 10 00 bf 8f     lw         ra,0x10(sp)
        800644a8 00 00 00 00     nop
        800644ac 08 00 e0 03     jr         ra
        800644b0 18 00 bd 27     _addiu     sp,sp,0x18

	
	
//Change the chance to hit function to add the shock effect


ChanceToHit(DigimonEntity *AttackerPtr,DigimonEntity *DefenderPtr,FighterData *combatPtr, int MoveID)
{
//code ignored
    blockChance = (movementAccuracy / 2) * ((DefenderPtr->DigimonStats).DigimonSpeed - (AttackerPtr->DigimonStats).DigimonSpeed / 10)) / 999;
	
    if ((currentAnimation == IDLE) || (currentAnimation == IDLE_TIRED)) 
        blockChance = (blockChance * 6) / 5;
                
    if (DefenderPtr->NewEffects == 8) //is shocked
        blockChance = blockChance / 2;
                
    chanceToHit = movementAccuracy - blockChance;
//code ignored
}
//battle version 
                              ChanceToHit
        8005bd08 22 30 64 00     sub        a2,v1,a0
                                 
        8005bd38 18 00 26 03     mult       t9,a2		
                             
        8005bda0 ca 01 00 10     b          0x8005c4cc
        8005bda4 00 00 00 00     _nop
                                  
        8005c4cc 57 00 a6 80     lb         digi,0x57(a1)
        8005c4d0 08 00 01 24     li         at,0x8
        8005c4d4 02 00 26 14     bne        at,digi,0x8005c4e0
        8005c4d8 00 00 00 00     _nop
        8005c4dc 43 20 04 00     sra        a0,a0,0x1
                             LAB_8005c4e0                                    
        8005c4e0 22 10 64 00     sub        v0,v1,a0
        8005c4e4 00 14 02 00     sll        v0,v0,0x10
        8005c4e8 2f fe 00 10     b          0x8005bda8
        8005c4ec 00 00 00 00     _nop
		
//tournament version

        80063dd0 22 30 64 00     sub        a2,v1,a0

        80063e00 18 00 26 03     mult       t9,a2
                            
        80063e68 92 01 00 10     b          0x800644b4
        80063e6c 00 00 00 00     _nop
		                             
        800644b4 57 00 a6 80     lb         a2,0x57(a1)
        800644b8 08 00 01 24     li         at,0x8
        800644bc 02 00 26 14     bne        at,a2,0x800644c8
        800644c0 00 00 00 00     _nop
        800644c4 43 20 04 00     sra        a0,a0,0x1
                             LAB_800644c8                                 
        800644c8 22 10 64 00     sub        v0,v1,a0
        800644cc 00 14 02 00     sll        v0,v0,0x10
        800644d0 67 fe 00 10     b          0x80063e70
        800644d4 00 00 00 00     _nop



//tick battle effect function Change

TickStatusEffect(void)
{
//code ignored
      if (((currentFighter->BattleFlags & 1) != 0) && ((digimonEntity->newEffects & 3) != 0)) 
	  {
        if (NO_AI_FLAG == 0) {
          currentFighter->poisonTimer = currentFighter->poisonTimer + -1;
        }
        if (currentFighter->poisonTimer == 0) 
		{
          currentFighter->poisonTimer = 100;
          damage = CalculateDamageRandom3(&digimonEntity->DigimonStats);
		  
          currentFighter->HPDamage = currentFighter->HPDamage + damage;
		  
          if (9999 < currentFighter->HPDamage) 
            currentFighter->HPDamage = 9999;
          
          if (currentTarget != 0)  //is not your partner digimon
            CombatHead[iVar6 + 0x5c4] = 0;
          
          color = 12;
          if (digimonEntity->newEffects == 2) 
            color = 3;
          
          AddEntityText(digimonEntity, currentTarget, color, damage, 0);
        }
      }
//code ignored
}

//battle version
        8005e700 57 00 22 82     lb         v0,0x57(s1)
        8005e704 00 00 00 00     nop
        8005e708 03 00 42 30     andi       v0,v0,0x3
        8005e70c 2f 00 02 10     beq        zero,v0,0x8005e7cc
        8005e710 00 00 00 00     _nop
        8005e714 48 92 82 8f     lw         v0,-0x6db8(gp)  //NO_AI_FLAG
        8005e718 00 00 00 00     nop
        8005e71c 05 00 40 14     bne        v0,zero,0x8005e734
        8005e720 00 00 00 00     _nop
        8005e724 1c 00 02 86     lh         v0,0x1c(s0) //currentFighter->poisonTimer
        8005e728 00 00 00 00     nop
        8005e72c ff ff 42 20     addi       v0,v0,-0x1
        8005e730 1c 00 02 a6     sh         v0,0x1c(s0)
                             LAB_8005e734                                   
        8005e734 1c 00 02 86     lh         v0,0x1c(s0)
        8005e738 00 00 00 00     nop
        8005e73c 23 00 40 14     bne        v0,zero,0x8005e7cc
        8005e740 00 00 00 00     _nop
        8005e744 64 00 02 24     li         v0,0x64
        8005e748 1c 00 02 a6     sh         v0,0x1c(s0)
        8005e74c 6f 70 01 0c     jal        0x8005c1bc  //CalculateDamageRandom3                           
        8005e750 21 20 12 00     _move      a0,s2
        8005e754 21 38 02 00     move       a3,v0
        8005e758 00 1c 02 00     sll        v1,v0,0x10
        8005e75c 2e 00 02 86     lh         v0,0x2e(s0)  //currentFighter->HPDamage
        8005e760 03 1c 03 00     sra        v1,v1,0x10
        8005e764 20 10 43 00     add        v0,v0,v1
        8005e768 2e 00 02 a6     sh         v0,0x2e(s0)
        8005e76c 2e 00 02 86     lh         v0,0x2e(s0)
        8005e770 00 00 00 00     nop
        8005e774 10 27 41 28     slti       at,v0,0x2710
        8005e778 03 00 20 14     bne        at,zero,0x8005e788
        8005e77c 00 00 00 00     _nop
        8005e780 0f 27 02 24     li         v0,0x270f
        8005e784 2e 00 02 a6     sh         v0,0x2e(s0)
                             LAB_8005e788                                    
        8005e788 05 00 60 12     beq        s3,zero,0x8005e7a0
        8005e78c 00 00 00 00     _nop
        8005e790 20 92 82 8f     lw         v0,-0x6de0(gp)        
        8005e794 00 00 00 00     nop
        8005e798 21 10 82 02     addu       v0,s4,v0
        8005e79c c4 05 40 a0     sb         zero,0x5c4(v0) //CombatHead[iVar6 + 0x5c4]
                             LAB_8005e7a0                                     
        8005e7a0 10 00 a0 af     sw         zero,0x10(sp)
        8005e7a4 21 20 20 02     move       a0,s1
        8005e7a8 21 28 60 02     move       a1,s3
        8005e7ac 57 00 22 82     lb         v0,0x57(s1)
        8005e7b0 02 00 01 24     li         at,0x2
        8005e7b4 02 00 41 14     bne        v0,at,0x8005e7c0
        8005e7b8 0c 00 06 24     _li        a2,0xc
        8005e7bc 03 00 06 24     li         a2,0x3
                             LAB_8005e7c0                                   
        8005e7c0 21 28 60 02     move       a1,s3
        8005e7c4 1a 7e 03 0c     jal        0x800df868  //AddEntityText                             
        8005e7c8 00 00 00 00     _nop
		
//the tournament version is mostly the same, except for the address of the CalculateDamageRandom3
//the tournament changes are located at 0x80066478



//various changes to make the digimon walking adapt to the new effects

        800e7d7c 57 00 a6 80     _lb        a2,0x57(a1)
		
        800e8970 0d 00 c2 30     andi       v0,SlowEffect,0xd

		
		//battle
		8005a5bc 57 00 86 90     lbu        a2,0x57(a0)

        8005b154 0d 00 c2 30     andi       v0,a2,0xd

		
		//tournament
		80062270 57 00 86 80     _lb        a2,0x57(a0)
 
        80062cbc 0b 00 c2 30     andi       v0,a2,0xd


		
//Cure all the new effects when using various/sup restore or death

        8005e5d8 57 00 80 a0     _sb        zero,0x57(a0)
		
		//battle death
        8005b02c 57 00 20 a2     sb         zero,0x57(s1)	
		
        //no need to heal on tournament deaths since the battle ends

//Restart the new effect flag

//battle
        80057244 1f 00 80 a0     sb         zero,0x1f(a0)

        80057254 02 00 a4 a4     sh         a0,0x2(a1)                 

//tournament
        8005ee44 1f 00 80 a0     sb         zero,0x1f(a0)
        8005ee48 02 00 87 84     lh         a3,0x2(a0)
        8005ee4c 02 00 08 25     addiu      t0,t0,0x2
        8005ee50 02 00 c7 a4     sh         a3,0x2(a2)
        8005ee54 21 30 00 01     move       a2,t0
        8005ee58 04 00 87 84     lh         a3,0x4(a0)
        8005ee5c 02 00 c8 24     addiu      t0,a2,0x2
        8005ee60 00 00 c7 a4     sh         a3,0x0(a2)
        8005ee64 06 00 84 84     lh         a0,0x6(a0)
        8005ee68 02 00 63 20     addi       v1,v1,0x2
        8005ee6c 00 00 04 a5     sh         a0,0x0(t0)



//new stats menu, there was also changes to the spritesheet but those are not included

void RenderStatsMenu(void)

{
  bool looping;
  byte tempByte;
  short tempValue2;
  short tempValue;
  short tempS;
  char offsetX;
  char spec;
  stats * digiStats;
  int tempInt;
  int count;
  byte clut_copy [24];
  
  clut_copy = CLUT_MENU;

  if (STRING_RENDERED == 1) 
  {
    CreatePrimitiveLines(LINEDATA_StatsMenu,6,5);
	
    for (count = 0; count < 6; count++) 
      renderSpriteFromVRAM(3,StringsStatMenuUI[count].PosX, StringsStatMenuUI[count].PosY + 1, StringsStatMenuUI[count].uvWidht,
				12, StringsStatMenuUI[count].uvX, StringsStatMenuUI[count].uvY, 5, 1);
    
    for (count = 0; count < 24; count++)
	{
      tempS = GetClutIndex(96, clut_copy[count] + 488);
      CreateRectPolyFT4(Poly_FT4_StatMenu[count].PosX, Poly_FT4_StatMenu[count].PosY, Poly_FT4_StatMenu[count].width, 
	                    Poly_FT4_StatMenu[count].height, Poly_FT4_StatMenu[count].uvX, Poly_FT4_StatMenu[count].uvY + 128, 
						5, tempS, 5, 0);
    }
    renderSpriteFromVRAM(0, -108, -66, 144, 12, 0, 60, 5, 0); //Digimon name
    renderSpriteFromVRAM(0, -108, -50, 144, 12, 0, 72, 5, 0); //Digimon species
    renderNumber(0, -104, -34, 2, age, 5);
    renderNumber(0, -32, -34, 2, Weight, 5);
	
    RenderItemBagNumber(0, 4, 59, 4, Partner.EntityData.DigimonStats.CurrentDigimonHP, 5);
    RenderItemBagNumber(0, 4, 104, 4, Partner.EntityData.DigimonStats.MaxDigimonHP, 5);
    RenderItemBagNumber(0, 4, 59, 19, Partner.EntityData.DigimonStats.CurrentDigimonMP, 5);
    RenderItemBagNumber(0, 4, 104, 19, Partner.EntityData.DigimonStats.MaxDigimonMP, 5);
    RenderSegmentedStatsBar(Partner.EntityData.DigimonStats.MaxDigimonHP, 9999, 85, 54, 15);
    RenderSegmentedStatsBar(Partner.EntityData.DigimonStats.MaxDigimonMP, 9999, 85, 54, 30);
	
    digiStats = &Partner.EntityData.DigimonStats;
    tempValue2 = 34;
    count = 0;
    do {
      RenderItemBagNumber(0, 4, 59, tempValue2, digiStats[count], 5);
      RenderSegmentedStatsBar(digiStats->DigimonOff, 999, 41, 54, tempValue2 + 11);
      tempValue2 = tempValue2 + 15;
      looping = count < 3;
      count++;
    } while (looping);
	
    renderNumber(0, 40, -34, 3, CareMistake, 5);
    renderNumber(0, 112, -34, 3, Battles, 5);
    RenderSquare(195, 84, 30, 14, 5);
    RenderSquare(267, 84, 30, 14, 5);
    RenderSmallNumbers(0, 3, -36, 39, Tiredness, 0);
	
    tempValue2 = 47;
    for (count = 0; count < 3; count++) 
	{
      spec = AllDigimonData[Partner.EntityData.DigimonID].specialities[count];
      if (spec != -1) 
	  {
        tempValue = 31238;
        if (((spec == 2) || (spec == 3)) || (spec == 4)) 
          tempValue = 31302;
        
        else if (spec == 5) 
          tempValue = 31366;
        
        offsetX = 15;
        if ((uint)playtimeFrames % 10 < 5) 
          offsetX = 0;
        
        CreateRectPolyFT4(tempValue2, -67, 12, 12, offsetX + spec * 24 + 36, 128, 5, tempValue, 5, 0);
      }
      tempValue2 = tempValue2 + 12;
    }
	
    count = (EntityPtr->EntityData).DigimonEntity.DigimonID;
    if (AllDigimonData[count].type != 0) 
      CreateRectPolyFT4(107, -67, 12, 12, (AllDigimonData[count].type - 1) * 15, 128, 5, 31238, 5, 0);
    
    spec = 24;
    tempByte = RaiseData[Partner.EntityData.DigimonEntity.DigimonID].sleepCycle;
    if (4 < tempByte) 
	{
      tempByte = tempByte - 4;
      spec = 48;
    }
    renderSpriteFromVRAM(0, 76, -50, 48, 12, tempByte * 48, spec, 5, 0);
	
    tempValue2 = -42;
    for (count = 0; count < 3; count++) 
	{
      if (count < (char)Partner.Lives) 
        CreateRectPolyFT4(tempValue2, -14, 12, 11, 232, 140, 5, 31622, 5, 0);
      
      CreateRectPolyFT4(tempValue2, -14, 12, 11, 244, 140, 5, 31622, 5, 0);
      tempValue2 = tempValue2 + 14;
    }
	
    RenderCurrentBubblesStats(ConditionFlag);
	
    tempS = GetClutIndex(96, 498);
    tempByte = 11;
    if (-1 < Happiness) 
      tempByte = 0;
    
    CreateRectPolyFT4(-145, 52, 11, 11, tempByte, 244, 5, tempS, 5, 0);
    count = 2;
    tempValue2 = Happiness;
    if (Happiness < 1)
	{
      tempValue2 = 100 - (Happiness + 100);
      count = 6;
    }
    RenderSmallNumbers(count, 3, -36, 54, tempValue2, 0);
    if (Happiness < 0) 
	{
      tempS = GetClutIndex(112, 501);
      tempValue = (short)tempS;
      count = Happiness + 100;
      if (count < 0) {
        count = Happiness + 101;
      }
      tempS = 50 - (50 - count / 2);
    }
    else 
	{
      tempS = GetClutIndex(112, 502);
      CreateRectPolyFT4(-91, 54, (50 - (50 - Happiness / 2)) & 0xff, 8, 24, 240, 24,
                        tempS, 5, 0);
      tempS = GetClutIndex(112, 501);
      tempValue = (short)tempS;
      tempS = 50;
    }
    CreateRectPolyFT4(-91, 54, tempS & 0xff, 8, 74, 240, 24, tempValue, 5, 0);
	
    tempS = GetClutIndex(96, 498);	
    tempByte = 33;
    if (49 < Discipline) 
      tempByte = 22;
    
    CreateRectPolyFT4(-145, 67, 11, 11, tempByte, 244, 5, tempS, 5, 0);
    RenderSmallNumbers(0, 3, -36, 69, Discipline, 0);
    if (Discipline < 50)
	{
      tempS = GetClutIndex(112, 501);
      tempValue = (short)tempS;
      tempS = Discipline & 0xff;
    }
    else 
	{
      tempS = GetClutIndex(112, 502);
      CreateRectPolyFT4(-91, 69, (Discipline - 50) & 0xff,8, 24, 240, 24, tempS, 5, 0);
      tempS = GetClutIndex(112, 501);
      tempValue = (short)tempS;
      tempS = 50;
    }
    CreateRectPolyFT4(-91, 69, tempS, 8, 74, 240, 24,tempValue,5,'\0');
	
    tempInt = VirusBar * 6;
    for (count = tempInt; 23 < count; count = count + -24) 
      tempInt = tempInt + 1;
    
    RenderSmallNumbers(0, 3, -36, 84, tempInt, 0);
    RenderSegmentedBar(-91, 84, VirusBar * 3, 8, 200, 200, 60, 0, 5);
    RenderSegmentedBar(-91, 39, Tiredness / 2, 8, 144, 213, 255, 0, 5);
    RenderItemBagNumber(0, 4, 108, 53, Lifetime, 5);
    RenderItemBagNumber(0, 4, 108, 82, EvoTimer, 5);
	
    for (count = 0; count < 19; count++) 
      RenderSquare(SquareUIStatsMenu[count].PosX, SquareUIStatsMenu[count].PosY, SquareUIStatsMenu[count].Width,
                   SquareUIStatsMenu[count].Height, 5);
    
	
    count = 0;
    tempValue2 = 38;
    do {
      tempS = GetClutIndex(96, 500);
      CreateRectPolyFT4(-38, tempValue2, 35, 11, 128, 210, 5, tempS, 5, 0);
      count = count + 1;
      tempValue2 = tempValue2 + 15;
    } while (count < 4);
	
    RenderEntity(&EntityPtr->EntityData,1);
  }
  else if ((STRING_RENDERED == 0) && (RenderStringsStats() == 1)) 
    STRING_RENDERED = 1';
  
  return;
}
                             
                             RenderStatsMenu                                
        800bb610 b0 ff bd 27     addiu      sp,sp,-0x50
        800bb614 34 00 bf af     sw         ra,0x34(sp)
        800bb618 30 00 b2 af     sw         s2,0x30(sp)
        800bb61c 2c 00 b1 af     sw         s1,0x2c(sp)
        800bb620 12 80 18 3c     lui        t8,0x8012
        800bb624 28 00 b0 af     sw         s0,0x28(sp)
        800bb628 b8 3d 18 27     addiu      t8,t8,0x3db8
        800bb62c 38 00 af 27     addiu      t7,sp,0x38
        800bb630 18 00 19 24     li         t9,0x18
                             LAB_800bb634                                     
        800bb634 00 00 0e 83     lb         t6,0x0(t8)  //CLUT_MENU
        800bb638 ff ff 39 27     addiu      t9,t9,-0x1
        800bb63c 00 00 ee a1     sb         t6,0x0(t7)
        800bb640 01 00 18 27     addiu      t8,t8,0x1
        800bb644 fb ff 20 1f     bgtz       t9,0x800bb634
        800bb648 01 00 ef 25     _addiu     t7,t7,0x1
        800bb64c 08 92 82 83     lb         v0,-0x6df8(gp)  //STRING_RENDERED                
        800bb650 01 00 01 24     li         at,0x1
        800bb654 0b 00 41 10     beq        v0,at,0x800bb684
        800bb658 00 00 00 00     _nop
        800bb65c 09 03 40 14     bne        v0,zero,0x800bc284
        800bb660 00 00 00 00     _nop
        800bb664 b4 fa 02 0c     jal        0x800bead0  //RenderStringsStats                              
        800bb668 00 00 00 00     _nop
        800bb66c 01 00 01 24     li         at,0x1
        800bb670 04 03 41 14     bne        v0,at,0x800bc284
        800bb674 00 00 00 00     _nop
        800bb678 01 00 02 24     li         v0,0x1
        800bb67c 01 03 00 10     b          0x800bc284
        800bb680 08 92 82 a3     _sb        v0,-0x6df8(gp) //STRING_RENDERED                    
                             LAB_800bb684                                   
        800bb684 12 80 04 3c     lui        a0,0x8012
        800bb688 54 3f 84 24     addiu      a0,a0,0x3f54  //LINEDATA_StatsMenu
        800bb68c 06 00 05 24     li         a1,0x6
        800bb690 d8 e0 02 0c     jal        0x800b8360  //CreatePrimitiveLines                             
        800bb694 05 00 06 24     _li        a2,0x5
        800bb698 21 88 00 00     clear      s1
        800bb69c 17 00 00 10     b          0x800bb6fc
        800bb6a0 21 80 00 00     _clear     s0
                             LAB_800bb6a4                                   
        800bb6a4 0c 80 02 3c     lui        v0,0x800c
        800bb6a8 9c c2 42 24     addiu      v0,v0,-0x3d64
        800bb6ac 21 18 50 00     addu       v1,v0,s0
        800bb6b0 0c 00 02 24     li         v0,0xc
        800bb6b4 10 00 a2 af     sw         v0,0x10(sp)
        800bb6b8 06 00 62 90     lbu        v0,0x6(v1)  //StringsStatMenuUI[count].uvX
        800bb6bc 03 00 04 24     li         a0,0x3
        800bb6c0 14 00 a2 af     sw         v0,0x14(sp)
        800bb6c4 07 00 62 90     lbu        v0,0x7(v1)  //StringsStatMenuUI[count].uvY
        800bb6c8 00 00 00 00     nop
        800bb6cc 18 00 a2 af     sw         v0,0x18(sp)
        800bb6d0 05 00 02 24     li         v0,0x5
        800bb6d4 1c 00 a2 af     sw         v0,0x1c(sp)
        800bb6d8 01 00 02 24     li         v0,0x1
        800bb6dc 20 00 a2 af     sw         v0,0x20(sp)
        800bb6e0 02 00 62 84     lh         v0,0x2(v1)  //StringsStatMenuUI[count].PosY
        800bb6e4 00 00 65 84     lh         a1,0x0(v1)  //StringsStatMenuUI[count].PosX
        800bb6e8 04 00 67 84     lh         a3,0x4(v1)  //StringsStatMenuUI[count].uvWidht
        800bb6ec d4 96 03 0c     jal        0x800e5b50  //renderSpriteFromVRAM                             
        800bb6f0 01 00 46 20     _addi      a2,v0,0x1
        800bb6f4 01 00 31 22     addi       s1,s1,0x1
        800bb6f8 08 00 10 22     addi       s0,s0,0x8
                             LAB_800bb6fc                                   
        800bb6fc 06 00 21 2a     slti       at,s1,0x6
        800bb700 e8 ff 20 14     bne        at,zero,0x800bb6a4
        800bb704 00 00 00 00     _nop
        800bb708 21 88 00 00     clear      s1
        800bb70c 1e 00 00 10     b          0x800bb788
        800bb710 21 90 00 00     _clear     s2
                             LAB_800bb714                                   
        800bb714 12 80 02 3c     lui        v0,0x8012
        800bb718 64 43 42 24     addiu      v0,v0,0x4364
        800bb71c 21 80 52 00     addu       s0,v0,s2
        800bb720 21 10 b1 03     addu       v0,sp,s1
        800bb724 38 00 42 90     lbu        v0,0x38(v0)
        800bb728 60 00 04 24     li         a0,0x60
        800bb72c af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                   
        800bb730 e8 01 45 20     _addi      a1,v0,0x1e8
        800bb734 06 00 03 92     lbu        v1,0x6(s0)  //PolyFT4StatMenu[count].texX
        800bb738 00 24 02 00     sll        a0,v0,0x10
        800bb73c 10 00 a3 af     sw         v1,local_40(sp)
        800bb740 07 00 02 92     lbu        v0,0x7(s0)  //PolyFT4StatMenu[count].texY
        800bb744 03 24 04 00     sra        a0,a0,0x10
        800bb748 80 00 42 20     addi       v0,v0,0x80
        800bb74c ff 00 42 30     andi       v0,v0,0xff
        800bb750 14 00 a2 af     sw         v0,0x14(sp)
        800bb754 05 00 02 24     li         v0,0x5
        800bb758 18 00 a2 af     sw         v0,0x18(sp)
        800bb75c 1c 00 a4 af     sw         a0,0x1c(sp)
        800bb760 20 00 a2 af     sw         v0,0x20(sp)
        800bb764 24 00 a0 af     sw         zero,0x24(sp)
        800bb768 00 00 04 86     lh         a0,0x0(s0)  //PolyFT4StatMenu[count].PosX
        800bb76c 02 00 05 86     lh         a1,0x2(s0)  //PolyFT4StatMenu[count].PosY
        800bb770 04 00 06 92     lbu        a2,0x4(s0)  //PolyFT4StatMenu[count].width
        800bb774 05 00 07 92     lbu        a3,0x5(s0)  //PolyFT4StatMenu[count].height
        800bb778 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                
        800bb77c 00 00 00 00     _nop
        800bb780 01 00 31 22     addi       s1,s1,0x1
        800bb784 08 00 52 22     addi       s2,s2,0x8
                             LAB_800bb788                                    
        800bb788 18 00 21 2a     slti       at,s1,0x18
        800bb78c e1 ff 20 14     bne        at,zero,0x800bb714
        800bb790 00 00 00 00     _nop
        800bb794 0c 00 02 24     li         v0,0xc
        800bb798 10 00 a2 af     sw         v0,0x10(sp)
        800bb79c 14 00 a0 af     sw         zero,0x14(sp)
        800bb7a0 3c 00 02 24     li         v0,0x3c
        800bb7a4 18 00 a2 af     sw         v0,0x18(sp)
        800bb7a8 05 00 02 24     li         v0,0x5
        800bb7ac 1c 00 a2 af     sw         v0,0x1c(sp)
        800bb7b0 20 00 a0 af     sw         zero,0x20(sp)
        800bb7b4 21 20 00 00     clear      a0
        800bb7b8 94 ff 05 24     li         a1,-0x6c
        800bb7bc be ff 06 24     li         a2,-0x42
        800bb7c0 d4 96 03 0c     jal        0x800e5b50  //renderSpriteFromVRAM                                    
        800bb7c4 90 00 07 24     _li        a3,0x90
        800bb7c8 90 00 07 24     li         a3,0x90
        800bb7cc 0c 00 02 24     li         v0,0xc
        800bb7d0 10 00 a2 af     sw         v0,0x10(sp)
        800bb7d4 14 00 a0 af     sw         zero,0x14(sp)
        800bb7d8 48 00 02 24     li         v0,0x48
        800bb7dc 18 00 a2 af     sw         v0,0x18(sp)
        800bb7e0 05 00 02 24     li         v0,0x5
        800bb7e4 1c 00 a2 af     sw         v0,0x1c(sp)
        800bb7e8 20 00 a0 af     sw         zero,0x20(sp)
        800bb7ec 21 20 00 00     clear      a0
        800bb7f0 94 ff 05 24     li         a1,-0x6c
        800bb7f4 d4 96 03 0c     jal        0x800e5b50  //renderSpriteFromVRAM                                    
        800bb7f8 ce ff 06 24     _li        a2,-0x32
        800bb7fc 14 80 01 3c     lui        at,0x8014
        800bb800 aa 84 22 84     lh         v0,-0x7b56(at) //age                            
        800bb804 21 20 00 00     clear      a0
        800bb808 10 00 a2 af     sw         v0,0x10(sp)
        800bb80c 05 00 02 24     li         v0,0x5
        800bb810 14 00 a2 af     sw         v0,0x14(sp)
        800bb814 98 ff 05 24     li         a1,-0x68
        800bb818 de ff 06 24     li         a2,-0x22
        800bb81c 11 95 03 0c     jal        0x800e5444  //renderNumber                                     
        800bb820 02 00 07 24     _li        a3,0x2
        800bb824 14 80 01 3c     lui        at,0x8014
        800bb828 a2 84 22 84     lh         v0,-0x7b5e(at)  //Weight                           
        800bb82c 21 20 00 00     clear      a0
        800bb830 10 00 a2 af     sw         v0,0x10(sp)
        800bb834 05 00 02 24     li         v0,0x5
        800bb838 14 00 a2 af     sw         v0,0x14(sp)
        800bb83c e0 ff 05 24     li         a1,-0x20
        800bb840 de ff 06 24     li         a2,-0x22
        800bb844 11 95 03 0c     jal        0x800e5444  //renderNumber                                          
        800bb848 02 00 07 24     _li        a3,0x2
        800bb84c 15 80 01 3c     lui        at,0x8015
        800bb850 f4 57 22 84     lh         v0,0x57f4(at)  //CurrentDigimonHP
        800bb854 21 20 00 00     clear      a0
        800bb858 10 00 a2 af     sw         v0,0x10(sp)
        800bb85c 05 00 02 24     li         v0,0x5
        800bb860 14 00 a2 af     sw         v0,0x14(sp)
        800bb864 3b 00 06 24     li         a2,0x3b
        800bb868 04 00 07 24     li         a3,0x4
        800bb86c 29 96 03 0c     jal        0x800e58a4  //RenderItemBagNumber                              
        800bb870 04 00 05 24     _li        a1,0x4
        800bb874 15 80 01 3c     lui        at,0x8015
        800bb878 f0 57 22 84     lh         v0,0x57f0(at)  //MaxDigimonHP
        800bb87c 21 20 00 00     clear      a0
        800bb880 10 00 a2 af     sw         v0,0x10(sp)
        800bb884 05 00 02 24     li         v0,0x5
        800bb888 14 00 a2 af     sw         v0,0x14(sp)
        800bb88c 68 00 06 24     li         a2,0x68
        800bb890 04 00 07 24     li         a3,0x4
        800bb894 29 96 03 0c     jal        0x800e58a4  //RenderItemBagNumber                              
        800bb898 04 00 05 24     _li        a1,0x4
        800bb89c 15 80 01 3c     lui        at,0x8015
        800bb8a0 f6 57 22 84     lh         v0,0x57f6(at) //CurrentDigimonMP
        800bb8a4 21 20 00 00     clear      a0
        800bb8a8 10 00 a2 af     sw         v0,0x10(sp)
        800bb8ac 05 00 02 24     li         v0,0x5
        800bb8b0 14 00 a2 af     sw         v0,0x14(sp)
        800bb8b4 3b 00 06 24     li         a2,0x3b
        800bb8b8 13 00 07 24     li         a3,0x13
        800bb8bc 29 96 03 0c     jal        0x800e58a4  //RenderItemBagNumber                             
        800bb8c0 04 00 05 24     _li        a1,0x4
        800bb8c4 15 80 01 3c     lui        at,0x8015
        800bb8c8 f2 57 22 84     lh         v0,0x57f2(at)  //MaxDigimonMP
        800bb8cc 21 20 00 00     clear      a0
        800bb8d0 10 00 a2 af     sw         v0,0x10(sp)
        800bb8d4 05 00 02 24     li         v0,0x5
        800bb8d8 14 00 a2 af     sw         v0,0x14(sp)
        800bb8dc 68 00 06 24     li         a2,0x68
        800bb8e0 13 00 07 24     li         a3,0x13
        800bb8e4 29 96 03 0c     jal        0x800e58a4  //RenderItemBagNumber                              
        800bb8e8 04 00 05 24     _li        a1,0x4
        800bb8ec 0f 00 02 24     li         v0,0xf
        800bb8f0 10 00 a2 af     sw         v0,0x10(sp)
        800bb8f4 15 80 01 3c     lui        at,0x8015
        800bb8f8 f0 57 24 84     lh         a0,0x57f0(at)
        800bb8fc 0f 27 05 24     li         a1,0x270f
        800bb900 55 00 06 24     li         a2,0x55
        800bb904 0a fb 02 0c     jal        0x800bec28  //RenderSegmentedStatsBar                          
        800bb908 36 00 07 24     _li        a3,0x36
        800bb90c 1e 00 02 24     li         v0,0x1e
        800bb910 10 00 a2 af     sw         v0,0x10(sp)
        800bb914 15 80 01 3c     lui        at,0x8015
        800bb918 f2 57 24 84     lh         a0,0x57f2(at)
        800bb91c 0f 27 05 24     li         a1,0x270f
        800bb920 55 00 06 24     li         a2,0x55
        800bb924 0a fb 02 0c     jal        0x800bec28  //RenderSegmentedStatsBar                         
        800bb928 36 00 07 24     _li        a3,0x36
        800bb92c 15 80 10 3c     lui        s0,0x8015
        800bb930 e0 57 10 26     addiu      s0,s0,0x57e0
        800bb934 22 00 11 24     li         s1,0x22
        800bb938 10 00 a2 af     sw         v0,0x10(sp)
        800bb93c 21 90 00 00     clear      s2
                             LAB_800bb940                                   
        800bb940 00 00 02 86     lh         v0,0x0(s0)  //digiStats[count]
        800bb944 21 20 00 00     clear      a0
        800bb948 10 00 a2 af     sw         v0,0x10(sp)
        800bb94c 05 00 02 24     li         v0,0x5
        800bb950 14 00 a2 af     sw         v0,0x14(sp)
        800bb954 3b 00 06 24     li         a2,0x3b
        800bb958 21 38 11 00     move       a3,s1
        800bb95c 29 96 03 0c     jal        0x800e58a4  //RenderItemBagNumber                              
        800bb960 04 00 05 24     _li        a1,0x4
        800bb964 0b 00 22 26     addiu      v0,s1,0xb
        800bb968 00 00 04 86     lh         a0,0x0(s0)  //digiStats[count]
        800bb96c e7 03 05 24     li         a1,0x3e7
        800bb970 29 00 06 24     li         a2,0x29
        800bb974 10 00 a2 af     sw         v0,0x10(sp)
        800bb978 0a fb 02 0c     jal        0x800bec28  //RenderSegmentedStatsBar                          
        800bb97c 36 00 07 24     _li        a3,0x36
        800bb980 02 00 10 26     addiu      s0,s0,0x2
        800bb984 0f 00 31 26     addiu      s1,s1,0xf
        800bb988 03 00 41 2a     slti       at,s2,0x3
        800bb98c ec ff 20 14     bne        at,zero,0x800bb940
        800bb990 01 00 52 26     _addiu     s2,s2,0x1
        800bb994 14 80 01 3c     lui        at,0x8014
        800bb998 b2 84 22 84     lh         v0,-0x7b4e(at) //CareMistake                     
        800bb99c 21 20 00 00     clear      a0
        800bb9a0 10 00 a2 af     sw         v0,0x10(sp)
        800bb9a4 05 00 02 24     li         v0,0x5
        800bb9a8 14 00 a2 af     sw         v0,0x14(sp)
        800bb9ac 28 00 05 24     li         a1,0x28
        800bb9b0 de ff 06 24     li         a2,-0x22
        800bb9b4 11 95 03 0c     jal        0x800e5444  //renderNumber                                         
        800bb9b8 03 00 07 24     _li        a3,0x3
        800bb9bc 14 80 01 3c     lui        at,0x8014
        800bb9c0 b4 84 22 84     lh         v0,-0x7b4c(at) //Battles                        
        800bb9c4 21 20 00 00     clear      a0
        800bb9c8 10 00 a2 af     sw         v0,0x10(sp)
        800bb9cc 05 00 02 24     li         v0,0x5
        800bb9d0 14 00 a2 af     sw         v0,0x14(sp)
        800bb9d4 70 00 05 24     li         a1,0x70
        800bb9d8 de ff 06 24     li         a2,-0x22
        800bb9dc 11 95 03 0c     jal        0x800e5444  //renderNumber                                         
        800bb9e0 03 00 07 24     _li        a3,0x3
        800bb9e4 05 00 02 24     li         v0,0x5
        800bb9e8 10 00 a2 af     sw         v0,0x10(sp)
        800bb9ec c3 00 04 24     li         a0,0xc3
        800bb9f0 54 00 05 24     li         a1,0x54
        800bb9f4 1e 00 06 24     li         a2,0x1e
        800bb9f8 cd f9 02 0c     jal        0x800be734  //RenderSquare                                    
        800bb9fc 0e 00 07 24     _li        a3,0xe
        800bba00 05 00 02 24     li         v0,0x5
        800bba04 10 00 a2 af     sw         v0,0x10(sp)
        800bba08 0b 01 04 24     li         a0,0x10b
        800bba0c 54 00 05 24     li         a1,0x54
        800bba10 1e 00 06 24     li         a2,0x1e
        800bba14 cd f9 02 0c     jal        0x800be734  //RenderSquare                                     
        800bba18 0e 00 07 24     _li        a3,0xe
        800bba1c 14 80 01 3c     lui        at,0x8014
        800bba20 82 84 22 84     lh         v0,-0x7b7e(at)  //Tiredness                       
        800bba24 21 20 00 00     clear      a0
        800bba28 10 00 a2 af     sw         v0,0x10(sp)
        800bba2c 00 00 02 24     li         v0,0x0
        800bba30 14 00 a2 af     sw         v0,0x14(sp)
        800bba34 dc ff 06 24     li         a2,-0x24
        800bba38 27 00 07 24     li         a3,0x27
        800bba3c c9 95 03 0c     jal        0x800e5724   //RenderSmallNumbers                                 
        800bba40 03 00 05 24     _li        a1,0x3
        800bba44 21 80 00 00     clear      s0
        800bba48 21 88 00 00     clear      s1
        800bba4c 4c 00 00 10     b          0x800bbb80
        800bba50 2f 00 12 24     _li        s2,0x2f
                             LAB_800bba54                                   
        800bba54 13 80 01 3c     lui        at,0x8013
        800bba58 48 f3 22 8c     lw         v0,-0xcb8(at) //PartnerPtr                       
        800bba5c 00 00 00 00     nop
        800bba60 00 00 43 8c     lw         v1,0x0(v0)  //Partner.EntityData.DigimonType
        800bba64 ff 00 01 24     li         at,0xff
        800bba68 40 10 03 00     sll        v0,v1,0x1
        800bba6c 20 10 43 00     add        v0,v0,v1
        800bba70 80 10 02 00     sll        v0,v0,0x2
        800bba74 20 10 43 00     add        v0,v0,v1
        800bba78 80 18 02 00     sll        v1,v0,0x2
        800bba7c 13 80 02 3c     lui        v0,0x8013
        800bba80 d2 ce 42 24     addiu      v0,v0,-0x312e
        800bba84 21 10 43 00     addu       v0,v0,v1
        800bba88 21 10 02 02     addu       v0,s0,v0
        800bba8c 00 00 42 90     lbu        v0,0x0(v0) //AllDigimonData[Partner.EntityData.DigimonID].specialities[count]
        800bba90 00 00 00 00     nop
        800bba94 00 24 02 00     sll        a0,v0,0x10
        800bba98 03 24 04 00     sra        a0,a0,0x10
        800bba9c 36 00 81 10     beq        a0,at,0x800bbb78
        800bbaa0 00 00 00 00     _nop
        800bbaa4 06 7a 02 24     li         v0,0x7a06
        800bbaa8 00 2c 02 00     sll        a1,v0,0x10
        800bbaac 02 00 01 24     li         at,0x2
        800bbab0 07 00 81 10     beq        a0,at,0x800bbad0
        800bbab4 03 2c 05 00     _sra       a1,a1,0x10
        800bbab8 03 00 01 24     li         at,0x3
        800bbabc 04 00 81 10     beq        a0,at,0x800bbad0
        800bbac0 00 00 00 00     _nop
        800bbac4 04 00 01 24     li         at,0x4
        800bbac8 05 00 81 14     bne        a0,at,0x800bbae0
        800bbacc 00 00 00 00     _nop
                             LAB_800bbad0                                     
        800bbad0 46 7a 02 24     li         v0,0x7a46
        800bbad4 00 2c 02 00     sll        a1,v0,0x10
        800bbad8 07 00 00 10     b          0x800bbaf8
        800bbadc 03 2c 05 00     _sra       a1,a1,0x10
                             LAB_800bbae0                                   
        800bbae0 05 00 01 24     li         at,0x5
        800bbae4 04 00 81 14     bne        a0,at,0x800bbaf8
        800bbae8 00 00 00 00     _nop
        800bbaec 86 7a 02 24     li         v0,0x7a86
        800bbaf0 00 2c 02 00     sll        a1,v0,0x10
        800bbaf4 03 2c 05 00     sra        a1,a1,0x10
                             LAB_800bbaf8                                     
        800bbaf8 d4 93 83 97     lhu        v1,-0x6c2c(gp)  //playtimeFrames                 
        800bbafc 0a 00 02 24     li         v0,0xa
        800bbb00 1a 00 62 00     div        v1,v0
        800bbb04 10 10 00 00     mfhi       v0
        800bbb08 05 00 41 28     slti       at,v0,0x5
        800bbb0c 03 00 20 10     beq        at,zero,0x800bbb1c
        800bbb10 0c 00 03 24     _li        v1,0xc
        800bbb14 01 00 00 10     b          0x800bbb1c
        800bbb18 21 18 00 00     _clear     v1
                             LAB_800bbb1c                                   
        800bbb1c 40 10 04 00     sll        v0,a0,0x1
        800bbb20 20 10 44 00     add        v0,v0,a0
        800bbb24 c0 10 02 00     sll        v0,v0,0x3
        800bbb28 24 00 42 20     addi       v0,v0,0x24
        800bbb2c 20 10 62 00     add        v0,v1,v0
        800bbb30 ff 00 42 30     andi       v0,v0,0xff
        800bbb34 10 00 a2 af     sw         v0,0x10(sp)
        800bbb38 80 00 02 24     li         v0,0x80
        800bbb3c 14 00 a2 af     sw         v0,0x14(sp)
        800bbb40 05 00 02 24     li         v0,0x5
        800bbb44 18 00 a2 af     sw         v0,0x18(sp)
        800bbb48 1c 00 a5 af     sw         a1,0x1c(sp)
        800bbb4c 20 00 a2 af     sw         v0,0x20(sp)
        800bbb50 00 24 12 00     sll        a0,s2,0x10
        800bbb54 0c 00 07 24     li         a3,0xc
        800bbb58 24 00 a0 af     sw         zero,0x24(sp)
        800bbb5c 03 24 04 00     sra        a0,a0,0x10
        800bbb60 bd ff 05 24     li         a1,-0x43
        800bbb64 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                 
        800bbb68 21 30 e0 00     _move      a2,a3
        800bbb6c 0c 00 22 22     addi       v0,s1,0xc
        800bbb70 00 8c 02 00     sll        s1,v0,0x10
        800bbb74 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800bbb78                                   
        800bbb78 0c 00 52 22     addi       s2,s2,0xc
        800bbb7c 01 00 10 22     addi       s0,s0,0x1
                             LAB_800bbb80                                     
        800bbb80 03 00 01 2a     slti       at,s0,0x3
        800bbb84 b3 ff 20 14     bne        at,zero,0x800bba54
        800bbb88 00 00 00 00     _nop
        800bbb8c 13 80 01 3c     lui        at,0x8013
        800bbb90 48 f3 22 8c     lw         v0,-0xcb8(at)  //Partner                        
        800bbb94 00 00 00 00     nop
        800bbb98 00 00 43 8c     lw         v1,0x0(v0)  //PartnerID
        800bbb9c 00 00 00 00     nop
        800bbba0 40 10 03 00     sll        v0,v1,0x1
        800bbba4 20 10 43 00     add        v0,v0,v1
        800bbba8 80 10 02 00     sll        v0,v0,0x2
        800bbbac 20 10 43 00     add        v0,v0,v1
        800bbbb0 80 18 02 00     sll        v1,v0,0x2
        800bbbb4 13 80 02 3c     lui        v0,0x8013
        800bbbb8 d0 ce 42 24     addiu      v0,v0,-0x3130
        800bbbbc 21 10 43 00     addu       v0,v0,v1
        800bbbc0 00 00 42 90     lbu        v0,0x0(v0)
        800bbbc4 00 00 00 00     nop
        800bbbc8 18 00 40 10     beq        v0,zero,0x800bbc2c
        800bbbcc 21 20 60 00     _move      a0,v1
        800bbbd0 13 80 02 3c     lui        v0,0x8013
        800bbbd4 b4 ce 42 24     addiu      v0,v0,-0x314c
        800bbbd8 21 10 44 00     addu       v0,v0,a0
        800bbbdc 1c 00 42 90     lbu        v0,0x1c(v0)  //AllDigimonData[Partner.EntityData.DigimonID].type
        800bbbe0 0c 00 07 24     li         a3,0xc
        800bbbe4 ff ff 43 20     addi       v1,v0,-0x1
        800bbbe8 40 10 03 00     sll        v0,v1,0x1
        800bbbec 20 10 43 00     add        v0,v0,v1
        800bbbf0 80 10 02 00     sll        v0,v0,0x2
        800bbbf4 ff 00 42 30     andi       v0,v0,0xff
        800bbbf8 10 00 a2 af     sw         v0,0x10(sp)
        800bbbfc 80 00 02 24     li         v0,0x80
        800bbc00 14 00 a2 af     sw         v0,0x14(sp)
        800bbc04 05 00 03 24     li         v1,0x5
        800bbc08 18 00 a3 af     sw         v1,0x18(sp)
        800bbc0c 06 7a 02 24     li         v0,0x7a06
        800bbc10 1c 00 a2 af     sw         v0,0x1c(sp)
        800bbc14 20 00 a3 af     sw         v1,0x20(sp)
        800bbc18 24 00 a0 af     sw         zero,0x24(sp)
        800bbc1c 6b 00 04 24     li         a0,0x6b
        800bbc20 bd ff 05 24     li         a1,-0x43
        800bbc24 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                 
        800bbc28 21 30 e0 00     _move      a2,a3
                             LAB_800bbc2c                                     
        800bbc2c 15 80 01 3c     lui        at,0x8015
        800bbc30 a8 57 23 8c     lw         v1,0x57a8(at) //PartnerID
        800bbc34 18 00 07 24     li         a3,0x18
        800bbc38 c0 10 03 00     sll        v0,v1,0x3
        800bbc3c 22 10 43 00     sub        v0,v0,v1
        800bbc40 80 18 02 00     sll        v1,v0,0x2
        800bbc44 12 80 02 3c     lui        v0,0x8012
        800bbc48 ce 25 42 24     addiu      v0,v0,0x25ce
        800bbc4c 21 10 43 00     addu       v0,v0,v1
        800bbc50 00 00 42 90     lbu        v0,0x0(v0)  //SleepCycle
        800bbc54 00 00 00 00     nop
        800bbc58 05 00 41 28     slti       at,v0,0x5
        800bbc5c 03 00 01 14     bne        zero,at,0x800bbc6c
        800bbc60 00 00 00 00     _nop
        800bbc64 fc ff 42 24     addiu      v0,v0,-0x4
        800bbc68 30 00 07 24     li         a3,0x30
                             LAB_800bbc6c                                  
        800bbc6c 18 00 a7 af     sw         a3,0x18(sp)
        800bbc70 30 00 07 24     li         a3,0x30
        800bbc74 21 20 00 00     clear      a0
        800bbc78 40 18 02 00     sll        v1,v0,0x1
        800bbc7c 20 10 43 00     add        v0,v0,v1
        800bbc80 00 11 02 00     sll        v0,v0,0x4
        800bbc84 14 00 a2 af     sw         v0,0x14(sp)
        800bbc88 4c 00 05 24     li         a1,0x4c
        800bbc8c 0c 00 02 24     li         v0,0xc
        800bbc90 10 00 a2 af     sw         v0,0x10(sp)
        800bbc94 05 00 02 24     li         v0,0x5
        800bbc98 1c 00 a2 af     sw         v0,0x1c(sp)
        800bbc9c 20 00 a0 af     sw         zero,0x20(sp)
        800bbca0 d4 96 03 0c     jal        0x800e5b50  //renderSpriteFromVRAM                                    
        800bbca4 ce ff 06 24     _li        a2,-0x32
        800bbca8 21 80 00 00     clear      s0
        800bbcac 29 00 00 10     b          0x800bbd54
        800bbcb0 d6 ff 11 24     _li        s1,-0x2a
                             LAB_800bbcb4                                    
        800bbcb4 15 80 01 3c     lui        at,0x8015
        800bbcb8 24 58 22 80     lb         v0,0x5824(at) //Lives
        800bbcbc 00 00 00 00     nop
        800bbcc0 2a 08 02 02     slt        at,s0,v0
        800bbcc4 11 00 20 10     beq        at,zero,0x800bbd0c
        800bbcc8 00 00 00 00     _nop
        800bbccc e8 00 02 24     li         v0,0xe8
        800bbcd0 10 00 a2 af     sw         v0,0x10(sp)
        800bbcd4 8c 00 02 24     li         v0,0x8c
        800bbcd8 14 00 a2 af     sw         v0,0x14(sp)
        800bbcdc 05 00 03 24     li         v1,0x5
        800bbce0 00 24 11 00     sll        a0,s1,0x10
        800bbce4 18 00 a3 af     sw         v1,0x18(sp)
        800bbce8 86 7b 02 24     li         v0,0x7b86
        800bbcec 1c 00 a2 af     sw         v0,0x1c(sp)
        800bbcf0 20 00 a3 af     sw         v1,0x20(sp)
        800bbcf4 24 00 a0 af     sw         zero,0x24(sp)
        800bbcf8 03 24 04 00     sra        a0,a0,0x10
        800bbcfc f2 ff 05 24     li         a1,-0xe
        800bbd00 0c 00 06 24     li         a2,0xc
        800bbd04 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                
        800bbd08 0b 00 07 24     _li        a3,0xb
                             LAB_800bbd0c                                   
        800bbd0c f4 00 02 24     li         v0,0xf4
        800bbd10 10 00 a2 af     sw         v0,0x10(sp)
        800bbd14 8c 00 02 24     li         v0,0x8c
        800bbd18 14 00 a2 af     sw         v0,0x14(sp)
        800bbd1c 05 00 03 24     li         v1,0x5
        800bbd20 00 24 11 00     sll        a0,s1,0x10
        800bbd24 18 00 a3 af     sw         v1,0x18(sp)
        800bbd28 86 7b 02 24     li         v0,0x7b86
        800bbd2c 1c 00 a2 af     sw         v0,0x1c(sp)
        800bbd30 20 00 a3 af     sw         v1,0x20(sp)
        800bbd34 24 00 a0 af     sw         zero,0x24(sp)
        800bbd38 03 24 04 00     sra        a0,a0,0x10
        800bbd3c f2 ff 05 24     li         a1,-0xe
        800bbd40 0c 00 06 24     li         a2,0xc
        800bbd44 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                 
        800bbd48 0b 00 07 24     _li        a3,0xb
        800bbd4c 01 00 10 22     addi       s0,s0,0x1
        800bbd50 0e 00 31 22     addi       s1,s1,0xe
                             LAB_800bbd54                                   
        800bbd54 03 00 01 2a     slti       at,s0,0x3
        800bbd58 d6 ff 20 14     bne        at,zero,0x800bbcb4
        800bbd5c 00 00 00 00     _nop
        800bbd60 14 80 01 3c     lui        at,0x8014
        800bbd64 60 84 24 8c     lw         a0,-0x7ba0(at) //ConditionFlag                   
        800bbd68 24 fb 02 0c     jal        0x800bec90  //RenderCurrentBubblesStats                       
        800bbd6c 00 00 00 00     _nop
        800bbd70 60 00 04 24     li         a0,0x60
        800bbd74 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                   
        800bbd78 f2 01 05 24     _li        a1,0x1f2
        800bbd7c 21 18 02 00     move       v1,v0
        800bbd80 14 80 01 3c     lui        at,0x8014
        800bbd84 8a 84 22 84     lh         v0,-0x7b76(at) //Happiness                       
        800bbd88 0b 00 04 24     li         a0,0xb
        800bbd8c 02 00 40 04     bltz       v0,0x800bbd98
        800bbd90 00 1c 03 00     _sll       v1,v1,0x10
        800bbd94 21 20 00 00     clear      a0
                             LAB_800bbd98                                      
        800bbd98 0b 00 07 24     li         a3,0xb
        800bbd9c 10 00 a4 af     sw         a0,0x10(sp)
        800bbda0 f4 00 02 24     li         v0,0xf4
        800bbda4 14 00 a2 af     sw         v0,0x14(sp)
        800bbda8 05 00 02 24     li         v0,0x5
        800bbdac 03 1c 03 00     sra        v1,v1,0x10
        800bbdb0 18 00 a2 af     sw         v0,0x18(sp)
        800bbdb4 1c 00 a3 af     sw         v1,0x1c(sp)
        800bbdb8 20 00 a2 af     sw         v0,0x20(sp)
        800bbdbc 24 00 a0 af     sw         zero,0x24(sp)
        800bbdc0 6f ff 04 24     li         a0,-0x91
        800bbdc4 34 00 05 24     li         a1,0x34
        800bbdc8 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                 
        800bbdcc 21 30 e0 00     _move      a2,a3
        800bbdd0 14 80 01 3c     lui        at,0x8014
        800bbdd4 8a 84 22 84     lh         v0,-0x7b76(at) //Happiness                       
        800bbdd8 02 00 04 24     li         a0,0x2
        800bbddc 05 00 40 1c     bgtz       v0,0x800bbdf4
        800bbde0 00 00 00 00     _nop
        800bbde4 64 00 42 24     addiu      v0,v0,0x64
        800bbde8 64 00 03 24     li         v1,0x64
        800bbdec 22 10 62 00     sub        v0,v1,v0
        800bbdf0 06 00 04 24     li         a0,0x6
                             LAB_800bbdf4                                     
        800bbdf4 10 00 a2 af     sw         v0,0x10(sp)
        800bbdf8 00 00 02 24     li         v0,0x0
        800bbdfc 14 00 a2 af     sw         v0,0x14(sp)
        800bbe00 dc ff 06 24     li         a2,-0x24
        800bbe04 36 00 07 24     li         a3,0x36
        800bbe08 c9 95 03 0c     jal        0x800e5724   //RenderSmallNumbers                                 
        800bbe0c 03 00 05 24     _li        a1,0x3
        800bbe10 14 80 01 3c     lui        at,0x8014
        800bbe14 8a 84 22 84     lh         v0,-0x7b76(at)  //Happiness                       
        800bbe18 70 00 04 24     li         a0,0x70
        800bbe1c 1f 00 40 04     bltz       v0,0x800bbe9c
        800bbe20 00 00 00 00     _nop
        800bbe24 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                     
        800bbe28 f6 01 05 24     _li        a1,0x1f6
        800bbe2c 00 24 02 00     sll        a0,v0,0x10
        800bbe30 18 00 03 24     li         v1,0x18
        800bbe34 10 00 a3 af     sw         v1,0x10(sp)
        800bbe38 f0 00 02 24     li         v0,0xf0
        800bbe3c 14 00 a2 af     sw         v0,0x14(sp)
        800bbe40 03 24 04 00     sra        a0,a0,0x10
        800bbe44 18 00 a3 af     sw         v1,0x18(sp)
        800bbe48 1c 00 a4 af     sw         a0,0x1c(sp)
        800bbe4c 05 00 02 24     li         v0,0x5
        800bbe50 20 00 a2 af     sw         v0,0x20(sp)
        800bbe54 24 00 a0 af     sw         zero,0x24(sp)
        800bbe58 14 80 01 3c     lui        at,0x8014
        800bbe5c 8a 84 22 84     lh         v0,-0x7b76(at)  //Happiness                      
        800bbe60 32 00 03 24     li         v1,0x32
        800bbe64 43 10 02 00     sra        v0,v0,0x1
        800bbe68 22 10 62 00     sub        v0,v1,v0
        800bbe6c 22 10 62 00     sub        v0,v1,v0
        800bbe70 ff 00 46 30     andi       a2,v0,0xff
        800bbe74 a5 ff 04 24     li         a0,-0x5b
        800bbe78 36 00 05 24     li         a1,0x36
        800bbe7c 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                 
        800bbe80 08 00 07 24     _li        a3,0x8
        800bbe84 70 00 04 24     li         a0,0x70
        800bbe88 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                   
        800bbe8c f5 01 05 24     _li        a1,0x1f5
        800bbe90 00 1c 02 00     sll        v1,v0,0x10
        800bbe94 0f 00 00 10     b          0x800bbed4
        800bbe98 32 00 02 24     _li        v0,0x32
                             LAB_800bbe9c                                    
        800bbe9c af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                    
        800bbea0 f5 01 05 24     _li        a1,0x1f5
        800bbea4 00 1c 02 00     sll        v1,v0,0x10
        800bbea8 14 80 01 3c     lui        at,0x8014
        800bbeac 8a 84 22 84     lh         v0,-0x7b76(at)  //Happiness                       
        800bbeb0 00 00 00 00     nop
        800bbeb4 64 00 42 20     addi       v0,v0,0x64
        800bbeb8 03 00 41 04     bgez       v0,0x800bbec8
        800bbebc 43 c8 02 00     _sra       t9,v0,0x1
        800bbec0 01 00 42 24     addiu      v0,v0,0x1
        800bbec4 43 c8 02 00     sra        t9,v0,0x1
                             LAB_800bbec8                                    
        800bbec8 32 00 01 24     li         at,0x32
        800bbecc 22 10 39 00     sub        v0,at,t9
        800bbed0 22 10 22 00     sub        v0,at,v0
                             LAB_800bbed4                                   
        800bbed4 ff 00 46 30     andi       a2,v0,0xff
        800bbed8 4a 00 02 24     li         v0,0x4a
        800bbedc 10 00 a2 af     sw         v0,0x10(sp)
        800bbee0 f0 00 02 24     li         v0,0xf0
        800bbee4 14 00 a2 af     sw         v0,0x14(sp)
        800bbee8 18 00 02 24     li         v0,0x18
        800bbeec 18 00 a2 af     sw         v0,0x18(sp)
        800bbef0 03 1c 03 00     sra        v1,v1,0x10
        800bbef4 1c 00 a3 af     sw         v1,0x1c(sp)
        800bbef8 05 00 02 24     li         v0,0x5
        800bbefc 20 00 a2 af     sw         v0,0x20(sp)
        800bbf00 24 00 a0 af     sw         zero,0x24(sp)
        800bbf04 a5 ff 04 24     li         a0,-0x5b
        800bbf08 36 00 05 24     li         a1,0x36
        800bbf0c 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                 
        800bbf10 08 00 07 24     _li        a3,0x8
        800bbf14 60 00 04 24     li         a0,0x60
        800bbf18 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                    
        800bbf1c f2 01 05 24     _li        a1,0x1f2
        800bbf20 00 1c 02 00     sll        v1,v0,0x10
        800bbf24 14 80 01 3c     lui        at,0x8014
        800bbf28 88 84 22 84     lh         v0,-0x7b78(at)  //Discipline                      
        800bbf2c 21 00 04 24     li         a0,0x21
        800bbf30 32 00 41 28     slti       at,v0,0x32
        800bbf34 02 00 20 14     bne        at,zero,0x800bbf40
        800bbf38 00 00 00 00     _nop
        800bbf3c 16 00 04 24     li         a0,0x16
                             LAB_800bbf40                                   
        800bbf40 10 00 a4 af     sw         a0,0x10(sp)
        800bbf44 f4 00 02 24     li         v0,0xf4
        800bbf48 14 00 a2 af     sw         v0,0x14(sp)
        800bbf4c 05 00 02 24     li         v0,0x5
        800bbf50 0b 00 07 24     li         a3,0xb
        800bbf54 03 1c 03 00     sra        v1,v1,0x10
        800bbf58 18 00 a2 af     sw         v0,0x18(sp)
        800bbf5c 1c 00 a3 af     sw         v1,0x1c(sp)
        800bbf60 20 00 a2 af     sw         v0,0x20(sp)
        800bbf64 24 00 a0 af     sw         zero,0x24(sp)
        800bbf68 6f ff 04 24     li         a0,-0x91
        800bbf6c 43 00 05 24     li         a1,0x43
        800bbf70 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                
        800bbf74 21 30 e0 00     _move      a2,a3
        800bbf78 14 80 01 3c     lui        at,0x8014
        800bbf7c 88 84 22 84     lh         v0,-0x7b78(at)  //Discipline                       
        800bbf80 00 00 04 24     li         a0,0x0
        800bbf84 32 00 41 28     slti       at,v0,0x32
        800bbf88 02 00 20 14     bne        at,zero,0x800bbf94
        800bbf8c 00 00 00 00     _nop
        800bbf90 00 00 04 24     li         a0,0x0
                             LAB_800bbf94                                    
        800bbf94 10 00 a2 af     sw         v0,0x10(sp)
        800bbf98 00 00 02 24     li         v0,0x0
        800bbf9c 14 00 a2 af     sw         v0,0x14(sp)
        800bbfa0 dc ff 06 24     li         a2,-0x24
        800bbfa4 45 00 07 24     li         a3,0x45
        800bbfa8 c9 95 03 0c     jal        0x800e5724   //RenderSmallNumbers                                 
        800bbfac 03 00 05 24     _li        a1,0x3
        800bbfb0 14 80 01 3c     lui        at,0x8014
        800bbfb4 88 84 22 84     lh         v0,-0x7b78(at)  //Discipline                       
        800bbfb8 70 00 04 24     li         a0,0x70
        800bbfbc 32 00 41 28     slti       at,v0,0x32
        800bbfc0 1c 00 20 14     bne        at,zero,0x800bc034
        800bbfc4 00 00 00 00     _nop
        800bbfc8 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                     
        800bbfcc f6 01 05 24     _li        a1,0x1f6
        800bbfd0 00 24 02 00     sll        a0,v0,0x10
        800bbfd4 18 00 03 24     li         v1,0x18
        800bbfd8 10 00 a3 af     sw         v1,0x10(sp)
        800bbfdc f0 00 02 24     li         v0,0xf0
        800bbfe0 14 00 a2 af     sw         v0,0x14(sp)
        800bbfe4 03 24 04 00     sra        a0,a0,0x10
        800bbfe8 18 00 a3 af     sw         v1,0x18(sp)
        800bbfec 1c 00 a4 af     sw         a0,0x1c(sp)
        800bbff0 05 00 02 24     li         v0,0x5
        800bbff4 20 00 a2 af     sw         v0,0x20(sp)
        800bbff8 24 00 a0 af     sw         zero,0x24(sp)
        800bbffc 14 80 01 3c     lui        at,0x8014
        800bc000 88 84 22 84     lh         v0,-0x7b78(at)  //Discipline                       
        800bc004 a5 ff 04 24     li         a0,-0x5b
        800bc008 ce ff 42 20     addi       v0,v0,-0x32
        800bc00c ff 00 46 30     andi       a2,v0,0xff
        800bc010 45 00 05 24     li         a1,0x45
        800bc014 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                
        800bc018 08 00 07 24     _li        a3,0x8
        800bc01c 70 00 04 24     li         a0,0x70
        800bc020 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                    
        800bc024 f5 01 05 24     _li        a1,0x1f5
        800bc028 00 1c 02 00     sll        v1,v0,0x10
        800bc02c 08 00 00 10     b          0x800bc050
        800bc030 32 00 06 24     _li        a2,0x32
                             LAB_800bc034                                  
        800bc034 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                     
        800bc038 f5 01 05 24     _li        a1,0x1f5
        800bc03c 00 1c 02 00     sll        v1,v0,0x10
        800bc040 14 80 01 3c     lui        at,0x8014
        800bc044 88 84 22 84     lh         v0,-0x7b78(at) //Discipline                      
        800bc048 00 00 00 00     nop
        800bc04c ff 00 46 30     andi       a2,v0,0xff
                             LAB_800bc050                                   
        800bc050 4a 00 02 24     li         v0,0x4a
        800bc054 10 00 a2 af     sw         v0,0x10(sp)
        800bc058 f0 00 02 24     li         v0,0xf0
        800bc05c 14 00 a2 af     sw         v0,0x14(sp)
        800bc060 18 00 02 24     li         v0,0x18
        800bc064 18 00 a2 af     sw         v0,0x18(sp)
        800bc068 03 1c 03 00     sra        v1,v1,0x10
        800bc06c 1c 00 a3 af     sw         v1,0x1c(sp)
        800bc070 05 00 02 24     li         v0,0x5
        800bc074 20 00 a2 af     sw         v0,0x20(sp)
        800bc078 24 00 a0 af     sw         zero,0x24(sp)
        800bc07c 14 80 01 3c     lui        at,0x8014
        800bc080 45 00 05 24     li         a1,0x45
        800bc084 a5 ff 04 24     li         a0,-0x5b
        800bc088 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                
        800bc08c 08 00 07 24     _li        a3,0x8
        800bc090 14 80 01 3c     lui        at,0x8014
        800bc094 7e 84 22 84     lh         v0,-0x7b82(at)  //VirusBar                       
        800bc098 21 20 00 00     clear      a0
        800bc09c 40 18 02 00     sll        v1,v0,0x1
        800bc0a0 20 18 62 00     add        v1,v1,v0
        800bc0a4 40 10 03 00     sll        v0,v1,0x1
        800bc0a8 21 20 02 00     move       a0,v0
                             LAB_800bc0ac                                   
        800bc0ac 18 00 81 28     slti       at,a0,0x18
        800bc0b0 04 00 20 14     bne        at,zero,0x800bc0c4
        800bc0b4 00 00 00 00     _nop
        800bc0b8 01 00 42 24     addiu      v0,v0,0x1
        800bc0bc fb ff 00 10     b          0x800bc0ac
        800bc0c0 e8 ff 84 24     _addiu     a0,a0,-0x18
                             LAB_800bc0c4                                   
        800bc0c4 21 20 00 00     clear      a0
        800bc0c8 10 00 a2 af     sw         v0,0x10(sp)
        800bc0cc 00 00 02 24     li         v0,0x0
        800bc0d0 14 00 a2 af     sw         v0,0x14(sp)
        800bc0d4 dc ff 06 24     li         a2,-0x24
        800bc0d8 54 00 07 24     li         a3,0x54
        800bc0dc c9 95 03 0c     jal        0x800e5724   //RenderSmallNumbers                                
        800bc0e0 03 00 05 24     _li        a1,0x3
        800bc0e4 c8 00 02 24     li         v0,0xc8
        800bc0e8 10 00 a2 af     sw         v0,0x10(sp)
        800bc0ec 14 00 a2 af     sw         v0,0x14(sp)
        800bc0f0 3c 00 02 24     li         v0,0x3c
        800bc0f4 18 00 a2 af     sw         v0,0x18(sp)
        800bc0f8 1c 00 a0 af     sw         zero,0x1c(sp)
        800bc0fc 05 00 02 24     li         v0,0x5
        800bc100 20 00 a2 af     sw         v0,0x20(sp)
        800bc104 14 80 01 3c     lui        at,0x8014
        800bc108 7e 84 23 84     lh         v1,-0x7b82(at)  //VirusBar                         
        800bc10c a5 ff 04 24     li         a0,-0x5b
        800bc110 40 10 03 00     sll        v0,v1,0x1
        800bc114 20 10 43 00     add        v0,v0,v1
        800bc118 00 34 02 00     sll        a2,v0,0x10
        800bc11c 03 34 06 00     sra        a2,a2,0x10
        800bc120 54 00 05 24     li         a1,0x54
        800bc124 17 fa 02 0c     jal        0x800be85c   //RenderSegmentedBar                                
        800bc128 08 00 07 24     _li        a3,0x8
        800bc12c 90 00 02 24     li         v0,0x90
        800bc130 10 00 a2 af     sw         v0,0x10(sp)
        800bc134 d5 00 02 24     li         v0,0xd5
        800bc138 14 00 a2 af     sw         v0,0x14(sp)
        800bc13c ff 00 02 24     li         v0,0xff
        800bc140 18 00 a2 af     sw         v0,0x18(sp)
        800bc144 1c 00 a0 af     sw         zero,0x1c(sp)
        800bc148 05 00 02 24     li         v0,0x5
        800bc14c 20 00 a2 af     sw         v0,0x20(sp)
        800bc150 14 80 01 3c     lui        at,0x8014
        800bc154 82 84 23 84     lh         v1,-0x7b7e(at)  //Tiredness                        
        800bc158 a5 ff 04 24     li         a0,-0x5b
        800bc15c 43 10 03 00     sra        v0,v1,0x1
        800bc160 00 34 02 00     sll        a2,v0,0x10
        800bc164 03 34 06 00     sra        a2,a2,0x10
        800bc168 27 00 05 24     li         a1,0x27
        800bc16c 17 fa 02 0c     jal        0x800be85c   //RenderSegmentedBar                              
        800bc170 08 00 07 24     _li        a3,0x8
        800bc174 14 80 01 3c     lui        at,0x8014
        800bc178 a8 84 22 84     lh         v0,-0x7b58(at)  //Lifetime                         
        800bc17c 21 20 00 00     clear      a0
        800bc180 10 00 a2 af     sw         v0,0x10(sp)
        800bc184 05 00 02 24     li         v0,0x5
        800bc188 14 00 a2 af     sw         v0,0x14(sp)
        800bc18c 6c 00 06 24     li         a2,0x6c
        800bc190 35 00 07 24     li         a3,0x35
        800bc194 29 96 03 0c     jal        0x800e58a4  //RenderItemBagNumber                             
        800bc198 04 00 05 24     _li        a1,0x4
        800bc19c 14 80 01 3c     lui        at,0x8014
        800bc1a0 b6 84 22 84     lh         v0,-0x7b4a(at)=>EvoTimer                       
        800bc1a4 21 20 00 00     clear      a0
        800bc1a8 10 00 a2 af     sw         v0,0x10(sp)
        800bc1ac 05 00 02 24     li         v0,0x5
        800bc1b0 14 00 a2 af     sw         v0,0x14(sp)
        800bc1b4 6c 00 06 24     li         a2,0x6c
        800bc1b8 52 00 07 24     li         a3,0x52
        800bc1bc 29 96 03 0c     jal        0x800e58a4  //RenderItemBagNumber                              
        800bc1c0 04 00 05 24     _li        a1,0x4
        800bc1c4 21 88 00 00     clear      s1
        800bc1c8 0e 00 00 10     b          0x800bc204
        800bc1cc 21 80 00 00     _clear     s0
                             LAB_800bc1d0                                  
        800bc1d0 12 80 02 3c     lui        v0,0x8012
        800bc1d4 cc 42 42 24     addiu      v0,v0,0x42cc
        800bc1d8 21 18 50 00     addu       v1,v0,s0
        800bc1dc 05 00 02 24     li         v0,0x5
        800bc1e0 10 00 a2 af     sw         v0,0x10(sp)
        800bc1e4 00 00 64 84     lh         a0,0x0(v1)=>SquareUIStatsMenu[count].PosX
        800bc1e8 02 00 65 84     lh         a1,0x2(v1)=>SquareUIStatsMenu[count].PosY
        800bc1ec 04 00 66 84     lh         a2,0x4(v1)=>SquareUIStatsMenu[count].Widht
        800bc1f0 06 00 67 84     lh         a3,0x6(v1)=>SquareUIStatsMenu[count].Height
        800bc1f4 cd f9 02 0c     jal        RenderSquare                                    
        800bc1f8 00 00 00 00     _nop
        800bc1fc 01 00 31 22     addi       s1,s1,0x1
        800bc200 08 00 10 22     addi       s0,s0,0x8
                             LAB_800bc204                                  
        800bc204 13 00 21 2a     slti       at,s1,0x13
        800bc208 f1 ff 20 14     bne        at,zero,0x800bc1d0
        800bc20c 00 00 00 00     _nop
        800bc210 21 80 00 00     clear      s0
        800bc214 26 00 11 24     li         s1,0x26
                             LAB_800bc218                                   
        800bc218 60 00 04 24     li         a0,0x60
        800bc21c 0c 00 05 24     li         a1,0xc
        800bc220 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                    
        800bc224 e8 01 a5 20     _addi      a1,a1,0x1e8
        800bc228 1c 00 a2 af     sw         v0,0x1c(sp)
        800bc22c 80 00 02 24     li         v0,0x80
        800bc230 10 00 a2 af     sw         v0,0x10(sp)
        800bc234 d2 00 02 24     li         v0,0xd2
        800bc238 14 00 a2 af     sw         v0,0x14(sp)
        800bc23c 05 00 03 24     li         v1,0x5
        800bc240 00 2c 11 00     sll        a1,s1,0x10
        800bc244 18 00 a3 af     sw         v1,0x18(sp)
        800bc248 20 00 a3 af     sw         v1,0x20(sp)
        800bc24c 24 00 a0 af     sw         zero,0x24(sp)
        800bc250 03 2c 05 00     sra        a1,a1,0x10
        800bc254 da ff 04 24     li         a0,-0x26
        800bc258 24 00 06 24     li         a2,0x24
        800bc25c 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                                
        800bc260 0b 00 07 24     _li        a3,0xb
        800bc264 01 00 10 26     addiu      s0,s0,0x1
        800bc268 04 00 01 2a     slti       at,s0,0x4
        800bc26c ea ff 20 14     bne        at,zero,0x800bc218
        800bc270 0f 00 31 26     _addiu     s1,s1,0xf
        800bc274 13 80 01 3c     lui        at,0x8013
        800bc278 48 f3 24 8c     lw         a0=>Partner,-0xcb8(at)  //PartnerPtr               
        800bc27c af fb 02 0c     jal        0x800beebc  //RenderDigimon                                 
        800bc280 01 00 05 24     _li        a1,0x1
                             LAB_800bc284                                    
        800bc284 34 00 bf 8f     lw         ra,0x34(sp)
        800bc288 30 00 b2 8f     lw         s2,0x30(sp)
        800bc28c 2c 00 b1 8f     lw         s1,0x2c(sp)
        800bc290 28 00 b0 8f     lw         s0,0x28(sp)
        800bc294 08 00 e0 03     jr         ra
        800bc298 50 00 bd 27     _addiu     sp,sp,0x50

New UI stats data:

Square UI 
Format: Pos X (short), Pos Y (short), Width (short) and height (short)

32 00 34 00 90 00 0E 00 
32 00 44 00 90 00 0E 00 
32 00 54 00 1A 00 0E 00 
7A 00 54 00 1A 00 0E 00 
44 00 AD 00 33 00 09 00 
44 00 BC 00 33 00 09 00 
44 00 CB 00 33 00 09 00 
D4 00 85 00 58 00 05 00 
D4 00 94 00 58 00 05 00 
D4 00 A3 00 2C 00 05 00 
D4 00 B2 00 2C 00 05 00 
D4 00 C1 00 2C 00 05 00 
D4 00 D0 00 2C 00 05 00 
D8 00 43 00 10 00 0F 00 
F2 00 28 00 28 00 0F 00 
08 01 AA 00 24 00 0E 00 
08 01 C7 00 24 00 0E 00 
44 00 9E 00 33 00 09 00
E9 00 44 00 2F 00 0E 00


Poly FT4 UI

Format: Pos X (short), Pos Y (short), Width (byte), height (byte), UV X (byte) and UV Y (byte)

6A 00 28 00 28 09 D6 69 
68 00 46 00 28 09 D6 75 
71 FF 25 00 07 0B 7A 74 
74 FF AF FF 35 0B 00 4B 
74 FF F4 FF 35 0B 00 56 
06 00 F4 FF 35 0B 00 61 
76 FF BE FF 1A 0A 80 30 
7A FF CE FF 1A 0A 9A 30 
BA FF DE FF 1C 0A B4 30 
7D FF DE FF 13 0A 80 3A 
65 00 B4 FF 19 08 00 38 
36 00 B4 FF 19 08 19 38 
2B 00 D0 FF 19 08 32 38 
00 00 00 00 00 00 80 52 
07 00 07 00 0B 0B 93 3A 
07 00 16 00 0B 0B 9E 3A 
07 00 25 00 0B 0B A9 3A 
07 00 34 00 0B 0B B4 3A 
07 00 43 00 0B 0B BF 3A 
07 00 52 00 0A 0B CA 3A 
7B FF 26 00 28 36 D6 30 
FC FF DE FF 22 0A 80 46 
4A 00 DE FF 1C 0A A3 46 
70 FF 52 00 09 0B 2C 74


Clut:

04 
04 
04 
08 
08 
08 
09 
09 
09 
09 
09 
09 
09 
0C 
0B 
0B 
0B 
0B 
0B 
0B 
04 
09 
09 
0A


Text UI

Format: Pos X (short), Pos Y (short), Width (short), UV X (byte) and UV Y (byte) (this one always assumes the height is 12)

14 00 07 00 18 00 00 30 
14 00 16 00 18 00 18 30 
14 00 25 00 24 00 00 24 
14 00 34 00 24 00 24 24 
14 00 43 00 24 00 70 24 
14 00 52 00 24 00 48 24


void SetSmallNumbersUI(int color,int digits,int posX,short posY,short value,int param_6)
{
  //code ignored

   SetUVDataPolyFT4(poly, values[currentValue] * 8, 183, 8, 9);
   SetPosDataPolyFT4(poly,(posX + ((digits - 1) - renderAmount) * 7)), posY, 8, 9);

  //code ignored

  return;
}

        800e57f0 09 00 06 24     li         a2,0x9
        800e57f4 06 00 02 a2     sb         v0,0x6(s0)
        800e57f8 10 00 a6 af     sw         a2,0x10(sp)
        800e57fc 21 10 b3 03     addu       v0,sp,s3
        800e5800 48 00 42 8c     lw         v0,0x48(v0)
        800e5804 b7 00 06 24     li         a2,0xb7

        800e5820 09 00 06 24     li         a2,0x9
        800e5824 20 10 c2 02     add        v0,s6,v0
        800e5828 08 00 07 24     li         a3,0x8
        800e582c 00 2c 02 00     sll        a1,v0,0x10
        800e5830 10 00 a6 af     sw         a2,0x10(sp)
		
		

//Remove lifetime/evotimer info

        800bc174 21 88 00 00     clear      s1
        800bc178 22 00 00 10     b          0x800bc204
        800bc17c 21 80 00 00     _clear     s0
		
Square UI 
//squares ignored
F2 00 28 00 28 00 0F 00  
D8 00 43 00 00 00 00 00  //changed
D8 00 43 00 00 00 00 00  //changed
45 00 9E 00 33 00 09 00


Poly FT4 UI
6A 00 28 00 28 09 D6 69 //first
68 00 46 00 28 09 D6 75 //second



//Change the tech data displayed

void DigimonTechMenu(void)
{

//code ignored

    uvY = 24;
    posY = -66;
    for (iVar4 = 0; iVar4 < 3; iVar4 = iVar4 + 1) {
      uVar2 = (uint)TechsEquipped[iVar4];
      if (uVar2 != 0xff) 
	  {
        renderSpriteFromVRAM(0,-0x8c,posY,0x78,0xc,'\0',uvY,5,1);
        sVar3 = AllMovementData[uVar2].damage;
        iVar7 = (EntityPtr->EntityData).DigimonEntity.DigimonType;
        bVar1 = (int)(char)RaiseData[iVar7].moveBoost == (uint)TechsEquipped[iVar4];
        colour = 0;
        if (bVar1) 
		{
          sVar3 = sVar3 + RaiseData[iVar7].boostValue;
          colour = 7;
        }
        RenderItemBagNumber(colour,4,-8,posY,sVar3,5);
		
        if (AllMovementData[uVar2].range != 0) 		
          renderSpriteFromVRAM(0,0x50,posY,0xc,0xc,(AllMovementData[uVar2].range - 1) * 24,'x',5,1);
        
		
        RenderItemBagNumber(0,3,0x1a,posY,(ushort)AllMovementData[uVar2].MPcost * 3,5);
        RenderItemBagNumber(0,3,0x34,posY,(ushort)AllMovementData[uVar2].accuracy,5);
		
        if ((AllMovementData[uVar2].effect != 0) || (AllMovementData[uVar2].effectBoosted != 0)) 
		{
          bVar5 = AllMovementData[uVar2].effect;
          if (bVar1) {
            bVar5 = AllMovementData[uVar2].effectBoosted;
          }
          CreateRectPolyFT4(0x65,posY,0xc,0xc,(bVar5 - 1) * '\f' + 0x78,0xe8,5,0x7a06,5,'\0');
          bVar5 = AllMovementData[uVar2].effectChance;
          if (bVar1) {
            bVar5 = AllMovementData[uVar2].chanceBoosted;
          }
          if (bVar5 != 0) {
            RenderItemBagNumber(0,3,0x7c,posY,(ushort)bVar5,5);
          }
        }
      }
      posY = posY + 15;
      uvY = uvY + 12;    
    }
//code ignored

    if (TechsEquipped[3] != -1) //has a finisher
	{
      renderSpriteFromVRAM(3,-0x8e,-0xf,0x24,0xc,'<','H',5,1);
      renderSpriteFromVRAM(0,-0x8c,1,0x84,0xc,'\0','<',5,1);
      RenderItemBagNumber(0,4,0x14,1,AllMovementData[TechsEquipped[3]].damage,5);
      renderSpriteFromVRAM(0,0x6e,1,0xc,0xc,(AllMovementData[TechsEquipped[3]].range - 1) * 24,'x',5,1);
      if (AllMovementData[TechsEquipped[3]].effect != 0) 
        CreateRectPolyFT4(0x83,0,0xc,0xc,
                          (AllMovementData[TechsEquipped[3]].effect - 1) * '\f' + 0x78,0xe8,5,0x7a06
                          ,5,'\0');
      
    }
	
//code ignored
}

Disassembly:

                       DigimonTechMenu

//attacks data
        800bc3c4 21 98 00 00     clear      s3
        800bc3c8 18 00 14 24     li         s4,0x18
        800bc3cc be ff 12 24     li         s2,-0x42
        800bc3d0 5d 00 00 10     b          0x800bc548
        800bc3d4 bd ff 11 24     _li        s1,-0x43
                             LAB_800bc3d8                                   
        800bc3d8 08 87 82 27     addiu      v0,gp,-0x78f8
        800bc3dc 21 10 53 00     addu       v0,v0,s3
        800bc3e0 00 00 42 90     lbu        v0,0x0(v0)  //TechsEquipped
        800bc3e4 ff 00 01 24     li         at,0xff
        800bc3e8 54 00 41 10     beq        v0,at,0x800bc53c
        800bc3ec 21 18 40 00     _move      v1,v0
        800bc3f0 12 80 02 3c     lui        v0,0x8012
        800bc3f4 00 19 03 00     sll        v1,v1,0x4
        800bc3f8 3c 62 42 24     addiu      v0,v0,0x623c
        800bc3fc 21 80 43 00     addu       s0,v0,v1
        800bc400 0c 00 02 24     li         v0,0xc
        800bc404 10 00 a2 af     sw         v0,0x10(sp)
        800bc408 14 00 a0 af     sw         zero,0x14(sp)
        800bc40c 18 00 b4 af     sw         s4,0x18(sp)
        800bc410 05 00 02 24     li         v0,0x5
        800bc414 1c 00 a2 af     sw         v0,0x1c(sp)
        800bc418 01 00 02 24     li         v0,0x1
        800bc41c 20 00 a2 af     sw         v0,0x20(sp)
        800bc420 21 20 00 00     clear      a0
        800bc424 74 ff 05 24     li         a1,-0x8c
        800bc428 21 30 40 02     move       a2,s2
        800bc42c d4 96 03 0c     jal        renderSpriteFromVRAM                             
        800bc430 78 00 07 24     _li        a3,0x78
        800bc434 a3 8a 02 08     j          0x800a2a8c
        800bc438 00 00 00 00     _nop
		
		                             LAB_800a2a8c                                    
        800a2a8c 04 00 02 86     lh         v0,0x4(s0)=>TechDamage
        800a2a90 00 00 00 00     nop
        800a2a94 13 80 01 3c     lui        at,0x8013
        800a2a98 48 f3 21 8c     lw         at,-0xcb8(at)=>->Partner                        
        800a2a9c 00 00 00 00     nop
        800a2aa0 00 00 24 8c     lw         a0,0x0(at)=>Partner
        800a2aa4 21 88 00 00     clear      s1
        800a2aa8 c0 08 04 00     sll        at,a0,0x3
        800a2aac 22 08 24 00     sub        at,at,a0
        800a2ab0 80 20 01 00     sll        a0,at,0x2
        800a2ab4 12 80 01 3c     lui        at,0x8012
        800a2ab8 c7 25 21 24     addiu      at,at,0x25c7
        800a2abc 21 08 24 00     addu       at,at,a0
        800a2ac0 00 00 24 80     lb         a0,0x0(at)=>MoveBoost
        800a2ac4 00 00 00 00     nop
        800a2ac8 08 87 85 27     addiu      a1,gp,-0x78f8
        800a2acc 21 28 b3 00     addu       a1,a1,s3
        800a2ad0 00 00 a5 90     lbu        a1,0x0(a1)=>TechsEquipped
        800a2ad4 00 00 00 00     nop
        800a2ad8 05 00 85 14     bne        a0,a1,0x800a2af0
        800a2adc 21 20 00 00     _clear     a0
        800a2ae0 03 00 25 84     lh         a1,0x3(at)=>BoostValue
        800a2ae4 01 00 11 24     li         s1,0x1
        800a2ae8 21 10 45 00     addu       v0,v0,a1
        800a2aec 07 00 04 24     li         a0,0x7
                             LAB_800a2af0                                   
        800a2af0 10 00 a2 af     sw         v0,0x10(sp)
        800a2af4 05 00 02 24     li         v0,0x5
        800a2af8 14 00 a2 af     sw         v0,0x14(sp)
        800a2afc f8 ff 06 24     li         a2,-0x8
        800a2b00 21 38 12 00     move       a3,s2
        800a2b04 29 96 03 0c     jal        RenderItemBagNumber                              
        800a2b08 04 00 05 24     _li        a1,0x4
        800a2b0c 08 00 02 92     lbu        v0,0x8(s0)
        800a2b10 00 00 00 00     nop
        800a2b14 12 00 40 10     beq        v0,zero,0x800a2b60
        800a2b18 21 18 40 00     _move      v1,v0
        800a2b1c ff ff 63 20     addi       v1,v1,-0x1
        800a2b20 40 10 03 00     sll        v0,v1,0x1
        800a2b24 0c 00 07 24     li         a3,0xc
        800a2b28 20 10 43 00     add        v0,v0,v1
        800a2b2c 10 00 a7 af     sw         a3,0x10(sp)
        800a2b30 c0 10 02 00     sll        v0,v0,0x3
        800a2b34 14 00 a2 af     sw         v0,0x14(sp)
        800a2b38 78 00 02 24     li         v0,0x78
        800a2b3c 18 00 a2 af     sw         v0,0x18(sp)
        800a2b40 05 00 02 24     li         v0,0x5
        800a2b44 1c 00 a2 af     sw         v0,0x1c(sp)
        800a2b48 01 00 02 24     li         v0,0x1
        800a2b4c 20 00 a2 af     sw         v0,0x20(sp)
        800a2b50 21 20 00 00     clear      a0
        800a2b54 50 00 05 24     li         a1,0x50
        800a2b58 d4 96 03 0c     jal        renderSpriteFromVRAM                             
        800a2b5c 21 30 40 02     _move      a2,s2
                             LAB_800a2b60                                    
        800a2b60 0f f1 02 08     j          0x800bc43c
        800a2b64 00 00 00 00     _nop


                             LAB_800bc43c                                   
        800bc43c 06 00 03 92     lbu        v1,0x6(s0)
        800bc440 21 20 00 00     clear      a0
        800bc444 40 10 03 00     sll        v0,v1,0x1
        800bc448 20 10 43 00     add        v0,v0,v1
        800bc44c 10 00 a2 af     sw         v0,0x10(sp)
        800bc450 05 00 02 24     li         v0,0x5
        800bc454 14 00 a2 af     sw         v0,0x14(sp)
        800bc458 1a 00 06 24     li         a2,0x1a
        800bc45c 21 38 12 00     move       a3,s2
        800bc460 29 96 03 0c     jal        RenderItemBagNumber                             
        800bc464 03 00 05 24     _li        a1,0x3
        800bc468 0b 00 02 92     lbu        v0,0xb(s0)
        800bc46c 21 20 00 00     clear      a0
        800bc470 10 00 a2 af     sw         v0,0x10(sp)
        800bc474 05 00 02 24     li         v0,0x5
        800bc478 14 00 a2 af     sw         v0,0x14(sp)
        800bc47c 34 00 06 24     li         a2,0x34
        800bc480 21 38 12 00     move       a3,s2
        800bc484 29 96 03 0c     jal        RenderItemBagNumber                             
        800bc488 03 00 05 24     _li        a1,0x3
        800bc48c 0a 00 03 92     lbu        v1,0xa(s0)
        800bc490 0e 00 02 92     lbu        v0,0xe(s0)
        800bc494 03 00 60 14     bne        v1,zero,0x800bc4a4
        800bc498 65 00 04 24     _li        a0,0x65
        800bc49c 27 00 02 10     beq        zero,v0,0x800bc53c
        800bc4a0 00 00 00 00     _nop
                             LAB_800bc4a4                                     
        800bc4a4 02 00 11 10     beq        zero,s1,0x800bc4b0
        800bc4a8 00 00 00 00     _nop
        800bc4ac 21 18 02 00     move       v1,v0
                             LAB_800bc4b0                                    
        800bc4b0 ff ff 63 20     addi       v1,v1,-0x1
        800bc4b4 40 10 03 00     sll        v0,v1,0x1
        800bc4b8 20 10 43 00     add        v0,v0,v1
        800bc4bc 80 10 02 00     sll        v0,v0,0x2
        800bc4c0 78 00 42 24     addiu      v0,v0,0x78
        800bc4c4 ff 00 42 30     andi       v0,v0,0xff
        800bc4c8 10 00 a2 af     sw         v0,0x10(sp)
        800bc4cc e8 00 02 24     li         v0,0xe8
        800bc4d0 14 00 a2 af     sw         v0,0x14(sp)
        800bc4d4 05 00 03 24     li         v1,0x5
        800bc4d8 00 2c 12 00     sll        a1,s2,0x10
        800bc4dc 0c 00 07 24     li         a3,0xc
        800bc4e0 18 00 a3 af     sw         v1,0x18(sp)
        800bc4e4 06 7a 02 24     li         v0,0x7a06
        800bc4e8 1c 00 a2 af     sw         v0,0x1c(sp)
        800bc4ec 20 00 a3 af     sw         v1,0x20(sp)
        800bc4f0 24 00 a0 af     sw         zero,0x24(sp)
        800bc4f4 03 2c 05 00     sra        a1,a1,0x10
        800bc4f8 4a e2 02 0c     jal        CreateRectPolyFT4                               
        800bc4fc 21 30 e0 00     _move      a2,a3
        800bc500 0c 00 02 92     lbu        v0,0xc(s0)
        800bc504 00 00 00 00     nop
        800bc508 03 00 20 12     beq        s1,zero,0x800bc518
        800bc50c 00 00 00 00     _nop
        800bc510 0f 00 02 92     lbu        v0,0xf(s0)
        800bc514 00 00 00 00     nop
                             LAB_800bc518                                   
        800bc518 08 00 40 18     blez       v0,0x800bc53c
        800bc51c 21 20 00 00     _clear     a0
        800bc520 10 00 a2 af     sw         v0,0x10(sp)
        800bc524 05 00 02 24     li         v0,0x5
        800bc528 14 00 a2 af     sw         v0,0x14(sp)
        800bc52c 7c 00 06 24     li         a2,0x7c
        800bc530 21 38 12 00     move       a3,s2
        800bc534 29 96 03 0c     jal        RenderItemBagNumber                            
        800bc538 03 00 05 24     _li        a1,0x3
                             LAB_800bc53c                                    
        800bc53c 01 00 73 22     addi       s3,s3,0x1
        800bc540 0f 00 52 22     addi       s2,s2,0xf
        800bc544 0c 00 94 22     addi       s4,s4,0xc
                             LAB_800bc548                                    
        800bc548 03 00 61 2a     slti       at,s3,0x3
        800bc54c a2 ff 20 14     bne        at,zero,0x800bc3d8
        800bc550 00 00 00 00     _nop
		
		
//Finisher data		
		800bc6bc 0b 87 82 93     lbu        v0,-0x78f5(gp)=>TechsEquipped[3]
        800bc6c0 ff 00 01 24     li         at,0xff
        800bc6c4 63 00 41 10     beq        v0,at,LAB_800bc854
        800bc6c8 00 00 00 00     _nop
        800bc6cc 0c 00 02 24     li         v0,0xc
        800bc6d0 10 00 a2 af     sw         v0,0x10(sp)
        800bc6d4 3c 00 02 24     li         v0,0x3c
        800bc6d8 14 00 a2 af     sw         v0,0x14(sp)
        800bc6dc 48 00 02 24     li         v0,0x48
        800bc6e0 18 00 a2 af     sw         v0,0x18(sp)
        800bc6e4 05 00 02 24     li         v0,0x5
        800bc6e8 1c 00 a2 af     sw         v0,0x1c(sp)
        800bc6ec 01 00 02 24     li         v0,0x1
        800bc6f0 20 00 a2 af     sw         v0,0x20(sp)
        800bc6f4 03 00 04 24     li         a0,0x3
        800bc6f8 72 ff 05 24     li         a1,-0x8e
        800bc6fc f1 ff 06 24     li         a2,-0xf
        800bc700 d4 96 03 0c     jal        renderSpriteFromVRAM                             undefined renderSpriteFromVRAM(i
        800bc704 24 00 07 24     _li        a3,0x24
        800bc708 0c 00 02 24     li         v0,0xc
        800bc70c 10 00 a2 af     sw         v0,0x10(sp)
        800bc710 14 00 a0 af     sw         zero,0x14(sp)
        800bc714 3c 00 02 24     li         v0,0x3c
        800bc718 18 00 a2 af     sw         v0,0x18(sp)
        800bc71c 05 00 02 24     li         v0,0x5
        800bc720 1c 00 a2 af     sw         v0,0x1c(sp)
        800bc724 01 00 06 24     li         a2,0x1
        800bc728 20 00 a6 af     sw         a2,0x20(sp)
        800bc72c 21 20 00 00     clear      a0
        800bc730 74 ff 05 24     li         a1,-0x8c
        800bc734 d4 96 03 0c     jal        renderSpriteFromVRAM                             undefined renderSpriteFromVRAM(i
        800bc738 84 00 07 24     _li        a3,0x84
        800bc73c 0b 87 82 93     lbu        v0,-0x78f5(gp)=>TechsEquipped[3]
        800bc740 21 20 00 00     clear      a0
        800bc744 00 19 02 00     sll        v1,v0,0x4
        800bc748 12 80 02 3c     lui        v0,0x8012
        800bc74c 3c 62 42 24     addiu      v0,v0,0x623c
        800bc750 21 10 43 00     addu       v0,v0,v1
        800bc754 04 00 42 84     lh         v0,0x4(v0)=>DAT_80127230                         = 07D0h
        800bc758 14 00 06 24     li         a2,0x14
        800bc75c 10 00 a2 af     sw         v0,0x10(sp)
        800bc760 05 00 02 24     li         v0,0x5
        800bc764 14 00 a2 af     sw         v0,0x14(sp)
        800bc768 01 00 07 24     li         a3,0x1
        800bc76c 29 96 03 0c     jal        RenderItemBagNumber                              undefined RenderItemBagNumber(in
        800bc770 04 00 05 24     _li        a1,0x4
        800bc774 0c 00 07 24     li         a3,0xc
        800bc778 10 00 a7 af     sw         a3,0x10(sp)
        800bc77c 0b 87 82 93     lbu        v0,-0x78f5(gp)=>TechsEquipped[3]
        800bc780 01 00 06 24     li         a2,0x1
        800bc784 00 19 02 00     sll        v1,v0,0x4
        800bc788 12 80 02 3c     lui        v0,0x8012
        800bc78c 3c 62 42 24     addiu      v0,v0,0x623c
        800bc790 21 10 43 00     addu       v0,v0,v1
        800bc794 08 00 42 90     lbu        v0,0x8(v0)=>DAT_80127234
        800bc798 21 20 00 00     clear      a0
        800bc79c ff ff 43 20     addi       v1,v0,-0x1
        800bc7a0 40 10 03 00     sll        v0,v1,0x1
        800bc7a4 20 10 43 00     add        v0,v0,v1
        800bc7a8 c0 10 02 00     sll        v0,v0,0x3
        800bc7ac 20 00 a6 af     sw         a2,0x20(sp)
        800bc7b0 14 00 a2 af     sw         v0,0x14(sp)
        800bc7b4 78 00 02 24     li         v0,0x78
        800bc7b8 18 00 a2 af     sw         v0,0x18(sp)
        800bc7bc 05 00 02 24     li         v0,0x5
        800bc7c0 1c 00 a2 af     sw         v0,0x1c(sp)
        800bc7c4 01 00 06 24     li         a2,0x1
        800bc7c8 d4 96 03 0c     jal        renderSpriteFromVRAM                             undefined renderSpriteFromVRAM(i
        800bc7cc 6e 00 05 24     _li        a1,0x6e
        800bc7d0 0b 87 82 93     lbu        v0,-0x78f5(gp)=>TechsEquipped[3]
        800bc7d4 00 00 00 00     nop
        800bc7d8 00 19 02 00     sll        v1,v0,0x4
        800bc7dc 12 80 02 3c     lui        v0,0x8012
        800bc7e0 46 62 42 24     addiu      v0,v0,0x6246
        800bc7e4 21 10 43 00     addu       v0,v0,v1
        800bc7e8 00 00 42 90     lbu        v0,0x0(v0)=>DAT_80127236                         = 02h
        800bc7ec 21 28 00 00     clear      a1
        800bc7f0 18 00 40 10     beq        v0,zero,LAB_800bc854
        800bc7f4 21 20 60 00     _move      a0,v1
        800bc7f8 12 80 02 3c     lui        v0,0x8012
        800bc7fc 3c 62 42 24     addiu      v0,v0,0x623c
        800bc800 21 10 44 00     addu       v0,v0,a0
        800bc804 0a 00 42 90     lbu        v0,0xa(v0)=>DAT_80127236                         = 02h
        800bc808 0c 00 07 24     li         a3,0xc
        800bc80c ff ff 43 20     addi       v1,v0,-0x1
        800bc810 40 10 03 00     sll        v0,v1,0x1
        800bc814 20 10 43 00     add        v0,v0,v1
        800bc818 80 10 02 00     sll        v0,v0,0x2
        800bc81c 78 00 42 24     addiu      v0,v0,0x78
        800bc820 ff 00 42 30     andi       v0,v0,0xff
        800bc824 10 00 a2 af     sw         v0,0x10(sp)
        800bc828 e8 00 02 24     li         v0,0xe8
        800bc82c 14 00 a2 af     sw         v0,0x14(sp)
        800bc830 05 00 03 24     li         v1,0x5
        800bc834 18 00 a3 af     sw         v1,0x18(sp)
        800bc838 06 7a 02 24     li         v0,0x7a06
        800bc83c 1c 00 a2 af     sw         v0,0x1c(sp)
        800bc840 20 00 a3 af     sw         v1,0x20(sp)
        800bc844 24 00 a0 af     sw         zero,0x24(sp)
        800bc848 83 00 04 24     li         a0,0x83
        800bc84c 4a e2 02 0c     jal        CreateRectPolyFT4                                undefined CreateRectPolyFT4(shor
        800bc850 21 30 e0 00     _move      a2,a3




//Render String pointers and strings changed

58 39 12 80 // "(triangle symbol) cancel"
61 39 12 80 // "S  L  W  A"

Located at:  0x14D64ECC / 0x801247d4


PolyFT4_TechMenu

Format: Pos X (short), Pos Y (short), Width (byte), height (byte), UV X (byte) and UV Y (byte)

F8 FF B4 FF 17 07 5C 22 
4B 00 B4 FF 13 07 4C 29 
62 00 B4 FF 11 07 00 30 
1E 00 B4 FF 0B 07 74 22 
14 00 C1 FF 04 22 CE 48 
2E 00 C1 FF 04 22 CE 48 
48 00 C1 FF 04 22 CE 48 
5E 00 C1 FF 04 22 CE 48 
35 00 B4 FF 10 07 84 77 
7C 00 B4 FF 12 07 99 77 
75 00 C1 FF 04 22 CE 48 
F2 FF C1 FF 04 22 CE 48 
75 00 E0 FF 00 00 78 0C



//Change the small box with tech data
void RenderMoveSelected(int currentPosX)

{
  byte bVar1;
  bool bVar2;
  undefined3 extraout_var;
  uint uVar3;
  short sVar4;
  LINE *linePrt;
  short uvWidht;
  int iVar5;
  uint uVar6;
  int iVar7;
  int iVar8;
  char uvX;
  RECT local_8;
  
  linePrt = LINE_ARRAY_80124064;
  if (currentPosX != 0) 
    linePrt = LINE_ARRAY_80124118;
  
  CreatePrimitiveLines(linePrt,0x12,4);
  iVar7 = currentPosX * 0x9d;
  CreateRectPolyFT4((short)((uint)((iVar7 + -0x91) * 0x10000) >> 0x10),-0x10,4,4,0x78,0x90,5,0x7b06,
                    4,'\0');
  CreateRectPolyFT4((short)((uint)((iVar7 + -0x10) * 0x10000) >> 0x10),-0x10,4,4,0x7c,0x90,5,0x7b06,
                    4,'\0');
  iVar8 = currentPosX * 0xcb;
  CreateRectPolyFT4((short)((uint)((iVar8 + -0x91) * 0x10000) >> 0x10),0x5c,4,4,0x78,0x94,5,0x7b06,4
                    ,'\0');
  CreateRectPolyFT4((short)((uint)((iVar8 + -0x3e) * 0x10000) >> 0x10),0x5c,4,4,0x7c,0x94,5,0x7b06,4
                    ,'\0');
  sVar4 = -0x10;
  bVar1 = 0x7c;
  if (currentPosX != 0) {
    sVar4 = 0xc;
    bVar1 = 0x78;
  }
  CreateRectPolyFT4(sVar4,4,4,4,bVar1,0x94,5,0x7b06,4,'\0');
  CreateRectPolyFT4((short)((uint)((iVar8 + -0x8c) * 0x10000) >> 0x10),0x4f,0x11,7,0x84,0xf7,5,
                    0x7b06,3,'\0');
  RenderSegmentedBar((short)((uint)((iVar7 + -0x8f) * 0x10000) >> 0x10),-0xe,0x80,0x13,0x32,0x32,
                     0x80,0,4);
  RenderSegmentedBar((short)((uint)((iVar8 + -0x8f) * 0x10000) >> 0x10),7,0x55,0x56,0x32,0x32,0x80,0
                     ,4);
  CreateRectPolyFT4((short)((uint)((iVar7 + -0x8c) * 0x10000) >> 0x10),-0x13,0x25,7,0x11,0xb0,5,
                    0x7b06,3,'\0');
  CreateRectPolyFT4((short)((uint)((iVar8 + -0x8c) * 0x10000) >> 0x10),0xb,0x17,7,0x5c,0xa2,5,0x7b06
                    ,3,'\0');
  sVar4 = (short)(iVar8 + -0x8c);
  CreateRectPolyFT4(sVar4,0x1c,0xb,7,0x74,0xa2,5,0x7b06,3,'\0');
  CreateRectPolyFT4(sVar4,0x2d,0x13,7,0x4c,0xa9,5,0x7b06,3,'\0');
  CreateRectPolyFT4(sVar4,0x3e,0x11,7,0,0xb0,5,0x7b06,3,'\0');
  sVar4 = 10;
  for (iVar5 = 0; iVar5 < 5; iVar5 = iVar5 + 1)
  {
    CreateRectPolyFT4((short)((uint)((iVar8 + -0x71) * 0x10000) >> 0x10),sVar4,10,10,0x8a,0x8c,5,
                      0x7b06,3,'\0');
    sVar4 = sVar4 + 0x11;
  }
  uVar3 = (DAT_80134d38 + -0x6f) / 0xf & 0xff;
  if (uVar3 == 1) 
    uVar3 = 5;
  
  else if (uVar3 == 2) 
    uVar3 = 1;
  
  else if (uVar3 == 3) 
    uVar3 = 4;
  
  else if (uVar3 == 4) 
    uVar3 = 2;
  
  else if (uVar3 == 5) 
    uVar3 = 3;
  
  uVar6 = ((DAT_80134d3a + -0x73) / 0x12 & 0xffU) + uVar3 * 8 & 0xff;
  if (uVar3 == 6) 
    uVar6 = uVar6 + 1 & 0xff;
  
  bVar2 = HasMove(uVar6);
  if (CONCAT31(extraout_var,bVar2) == 0) 
    return;
  
  local_8.x = 0;
  local_8.y = 0x90;
  local_8.w = 0x78;
  local_8.h = 0xc;
  ClearTextSubArea(&local_8);
  RenderString((&PTR_s_Fire_Tower_80126054)[uVar6],0,0x90);
  renderSpriteFromVRAM(0,(short)iVar7 + -0x8a,-8,0x78,0xc,'\0',-0x70,3,1);
  sVar4 = AllMovementData[uVar6].damage;
  iVar7 = 0;
  iVar5 = (EntityPtr->EntityData).DigimonEntity.DigimonType;
  if (uVar6 == (int)(char)RaiseData[iVar5].moveBoost) {
    iVar7 = 2;
    sVar4 = sVar4 + RaiseData[iVar5].boostValue;
  }
  RenderItemBagNumber(iVar7,4,iVar8 + -100,10,sVar4,3);
  RenderItemBagNumber(0,3,iVar8 + -0x5b,0x1b,(ushort)AllMovementData[uVar6].MPcost * 3,3);
  sVar4 = (short)(iVar8 + -100);
  renderSpriteFromVRAM
            (0,sVar4 + 8,0x2b,0x14,0xc,(AllMovementData[uVar6].range - 1) * 24,'x',3,1);
  bVar1 = AllMovementData[uVar6].effect;
  if (bVar1 == 7) 
  {
    uvWidht = 0x20;
    uvX = -0x50;
  }
  else if (bVar1 == 6) 
  {
    uvWidht = 0x1c;
    uvX = -0x6c;
  }
  else if (bVar1 == 5) 
  {
    uvWidht = 0x20;
    uvX = 't';
  }
  else if (bVar1 == 4)
  {
    uvWidht = 0x1d;
    uvX = 'W';
  }
  else if (bVar1 == 3)
  {
    uvWidht = 0x20;
    uvX = '8';
  }
  else if (bVar1 == 2) 
  {
    uvWidht = 0x1c;
    uvX = '\x1c';
  }
  else 
  {
    if (bVar1 != 1) goto LAB_800bfc50;
    uvWidht = 0x1a;
    uvX = '\0';
  }
  renderSpriteFromVRAM(0,sVar4,0x3c,uvWidht,0xc,uvX,-0x7c,3,1);
LAB_800bfc50:
  RenderItemBagNumber(0,3,iVar8 + -0x5b,0x4e,(ushort)AllMovementData[uVar6].accuracy,3);
  return;
  
Disassembly:  

                             RenderMoveSelected                            
        800bf4fc b8 ff bd 27     addiu      sp,sp,-0x48
        800bf500 3c 00 bf af     sw         ra,0x3c(sp)
        800bf504 38 00 b4 af     sw         s4,0x38(sp)
        800bf508 34 00 b3 af     sw         s3,0x34(sp)
        800bf50c 30 00 b2 af     sw         s2,0x30(sp)
        800bf510 2c 00 b1 af     sw         s1,0x2c(sp)
        800bf514 28 00 b0 af     sw         s0,0x28(sp)
        800bf518 21 80 80 00     move       s0,currentPosX
        800bf51c 12 80 04 3c     lui        currentPosX,0x8012
        800bf520 64 40 84 24     addiu      currentPosX=>LINE_ARRAY_80124064,currentPosX,0
        800bf524 02 00 00 12     beq        s0,zero,0x800bf530
        800bf528 00 00 00 00     _nop
        800bf52c b4 00 84 24     addiu      currentPosX=>LINE_ARRAY_80124118,currentPosX,0
                             LAB_800bf530                                   
        800bf530 12 00 05 24     li         a1,0x12
        800bf534 d8 e0 02 0c     jal        CreatePrimitiveLines                             
        800bf538 04 00 06 24     _li        a2,0x4
        800bf53c 78 00 02 24     li         v0,0x78
        800bf540 10 00 a2 af     sw         v0,0x10(sp)
        800bf544 90 00 02 24     li         v0,0x90
        800bf548 14 00 a2 af     sw         v0,0x14(sp)
        800bf54c 05 00 02 24     li         v0,0x5
        800bf550 18 00 a2 af     sw         v0,0x18(sp)
        800bf554 06 7b 02 24     li         v0,0x7b06
        800bf558 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf55c 04 00 07 24     li         a3,0x4
        800bf560 9d 00 02 24     li         v0,0x9d
        800bf564 18 00 02 02     mult       s0,v0
        800bf568 20 00 a7 af     sw         a3,0x20(sp)
        800bf56c 12 10 00 00     mflo       v0
        800bf570 21 a0 40 00     move       s4,v0
        800bf574 6f ff 42 20     addi       v0,v0,-0x91
        800bf578 00 24 02 00     sll        currentPosX,v0,0x10
        800bf57c 24 00 a0 af     sw         zero,0x24(sp)
        800bf580 21 88 00 02     move       s1,s0
        800bf584 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf588 f0 ff 05 24     li         a1,-0x10
        800bf58c 4a e2 02 0c     jal        CreateRectPolyFT4                               
        800bf590 21 30 e0 00     _move      a2,a3
        800bf594 7c 00 02 24     li         v0,0x7c
        800bf598 10 00 a2 af     sw         v0,0x10(sp)
        800bf59c 90 00 02 24     li         v0,0x90
        800bf5a0 14 00 a2 af     sw         v0,0x14(sp)
        800bf5a4 05 00 02 24     li         v0,0x5
        800bf5a8 18 00 a2 af     sw         v0,0x18(sp)
        800bf5ac 06 7b 02 24     li         v0,0x7b06
        800bf5b0 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf5b4 04 00 07 24     li         a3,0x4
        800bf5b8 f0 ff 82 22     addi       v0,s4,-0x10
        800bf5bc 20 00 a7 af     sw         a3,0x20(sp)
        800bf5c0 00 24 02 00     sll        currentPosX,v0,0x10
        800bf5c4 24 00 a0 af     sw         zero,0x24(sp)
        800bf5c8 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf5cc f0 ff 05 24     li         a1,-0x10
        800bf5d0 4a e2 02 0c     jal        CreateRectPolyFT4                               
        800bf5d4 21 30 e0 00     _move      a2,a3
        800bf5d8 78 00 02 24     li         v0,0x78
        800bf5dc 10 00 a2 af     sw         v0,0x10(sp)
        800bf5e0 94 00 02 24     li         v0,0x94
        800bf5e4 14 00 a2 af     sw         v0,0x14(sp)
        800bf5e8 05 00 02 24     li         v0,0x5
        800bf5ec 18 00 a2 af     sw         v0,0x18(sp)
        800bf5f0 06 7b 02 24     li         v0,0x7b06
        800bf5f4 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf5f8 04 00 07 24     li         a3,0x4
        800bf5fc cb 00 02 24     li         v0,0xcb
        800bf600 18 00 22 02     mult       s1,v0
        800bf604 20 00 a7 af     sw         a3,0x20(sp)
        800bf608 12 10 00 00     mflo       v0
        800bf60c 21 98 40 00     move       s3,v0
        800bf610 6f ff 42 20     addi       v0,v0,-0x91
        800bf614 00 24 02 00     sll        currentPosX,v0,0x10
        800bf618 24 00 a0 af     sw         zero,0x24(sp)
        800bf61c 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf620 5c 00 05 24     li         a1,0x5c
        800bf624 4a e2 02 0c     jal        CreateRectPolyFT4                              
        800bf628 21 30 e0 00     _move      a2,a3
        800bf62c 7c 00 02 24     li         v0,0x7c
        800bf630 10 00 a2 af     sw         v0,0x10(sp)
        800bf634 94 00 02 24     li         v0,0x94
        800bf638 14 00 a2 af     sw         v0,0x14(sp)
        800bf63c 05 00 02 24     li         v0,0x5
        800bf640 18 00 a2 af     sw         v0,0x18(sp)
        800bf644 06 7b 02 24     li         v0,0x7b06
        800bf648 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf64c 04 00 07 24     li         a3,0x4
        800bf650 c2 ff 62 22     addi       v0,s3,-0x3e
        800bf654 20 00 a7 af     sw         a3,0x20(sp)
        800bf658 00 24 02 00     sll        currentPosX,v0,0x10
        800bf65c 24 00 a0 af     sw         zero,0x24(sp)
        800bf660 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf664 5c 00 05 24     li         a1,0x5c
        800bf668 4a e2 02 0c     jal        CreateRectPolyFT4                               
        800bf66c 21 30 e0 00     _move      a2,a3
        800bf670 f0 ff 04 24     li         currentPosX,-0x10
        800bf674 7c 00 02 24     li         v0,0x7c
        800bf678 03 00 00 12     beq        s0,zero,0x800bf688
        800bf67c 00 00 00 00     _nop
        800bf680 0c 00 04 24     li         currentPosX,0xc
        800bf684 78 00 02 24     li         v0,0x78
                             LAB_800bf688                                    
        800bf688 10 00 a2 af     sw         v0,0x10(sp)
        800bf68c 94 00 02 24     li         v0,0x94
        800bf690 14 00 a2 af     sw         v0,0x14(sp)
        800bf694 05 00 02 24     li         v0,0x5
        800bf698 18 00 a2 af     sw         v0,0x18(sp)
        800bf69c 06 7b 02 24     li         v0,0x7b06
        800bf6a0 04 00 07 24     li         a3,0x4
        800bf6a4 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf6a8 20 00 a7 af     sw         a3,0x20(sp)
        800bf6ac 24 00 a0 af     sw         zero,0x24(sp)
        800bf6b0 04 00 05 24     li         a1,0x4
        800bf6b4 4a e2 02 0c     jal        CreateRectPolyFT4                               
        800bf6b8 21 30 e0 00     _move      a2,a3
        800bf6bc 84 00 02 24     li         v0,0x84
        800bf6c0 10 00 a2 af     sw         v0,0x10(sp)
        800bf6c4 f7 00 02 24     li         v0,0xf7
        800bf6c8 14 00 a2 af     sw         v0,0x14(sp)
        800bf6cc 05 00 02 24     li         v0,0x5
        800bf6d0 18 00 a2 af     sw         v0,0x18(sp)
        800bf6d4 06 7b 02 24     li         v0,0x7b06
        800bf6d8 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf6dc 03 00 02 24     li         v0,0x3
        800bf6e0 20 00 a2 af     sw         v0,0x20(sp)
        800bf6e4 74 ff 62 22     addi       v0,s3,-0x8c
        800bf6e8 00 24 02 00     sll        currentPosX,v0,0x10
        800bf6ec 24 00 a0 af     sw         zero,0x24(sp)
        800bf6f0 21 80 40 00     move       s0,v0
        800bf6f4 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf6f8 4f 00 05 24     li         a1,0x4f
        800bf6fc 11 00 06 24     li         a2,0x11
        800bf700 4a e2 02 0c     jal        CreateRectPolyFT4                               
        800bf704 07 00 07 24     _li        a3,0x7
        800bf708 00 00 00 00     nop
        800bf70c 00 00 00 00     nop
        800bf710 32 00 02 24     li         v0,0x32
        800bf714 10 00 a2 af     sw         v0,0x10(sp)
        800bf718 14 00 a2 af     sw         v0,0x14(sp)
        800bf71c 80 00 06 24     li         a2,0x80
        800bf720 18 00 a6 af     sw         a2,0x18(sp)
        800bf724 1c 00 a0 af     sw         zero,0x1c(sp)
        800bf728 04 00 02 24     li         v0,0x4
        800bf72c 20 00 a2 af     sw         v0,0x20(sp)
        800bf730 71 ff 82 22     addi       v0,s4,-0x8f
        800bf734 00 24 02 00     sll        currentPosX,v0,0x10
        800bf738 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf73c f2 ff 05 24     li         a1,-0xe
        800bf740 17 fa 02 0c     jal        RenderSegmentedBar                              
        800bf744 13 00 07 24     _li        a3,0x13
        800bf748 32 00 02 24     li         v0,0x32
        800bf74c 10 00 a2 af     sw         v0,0x10(sp)
        800bf750 14 00 a2 af     sw         v0,0x14(sp)
        800bf754 80 00 02 24     li         v0,0x80
        800bf758 18 00 a2 af     sw         v0,0x18(sp)
        800bf75c 1c 00 a0 af     sw         zero,0x1c(sp)
        800bf760 04 00 02 24     li         v0,0x4
        800bf764 20 00 a2 af     sw         v0,0x20(sp)
        800bf768 71 ff 62 22     addi       v0,s3,-0x8f
        800bf76c 00 24 02 00     sll        currentPosX,v0,0x10
        800bf770 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf774 07 00 05 24     li         a1,0x7
        800bf778 55 00 06 24     li         a2,0x55
        800bf77c 17 fa 02 0c     jal        RenderSegmentedBar                              
        800bf780 56 00 07 24     _li        a3,0x56
        800bf784 11 00 02 24     li         v0,0x11
        800bf788 10 00 a2 af     sw         v0,0x10(sp)
        800bf78c b0 00 02 24     li         v0,0xb0
        800bf790 14 00 a2 af     sw         v0,0x14(sp)
        800bf794 05 00 02 24     li         v0,0x5
        800bf798 18 00 a2 af     sw         v0,0x18(sp)
        800bf79c 06 7b 02 24     li         v0,0x7b06
        800bf7a0 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf7a4 03 00 02 24     li         v0,0x3
        800bf7a8 20 00 a2 af     sw         v0,0x20(sp)
        800bf7ac 74 ff 82 22     addi       v0,s4,-0x8c
        800bf7b0 00 24 02 00     sll        currentPosX,v0,0x10
        800bf7b4 24 00 a0 af     sw         zero,0x24(sp)
        800bf7b8 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf7bc ed ff 05 24     li         a1,-0x13
        800bf7c0 25 00 06 24     li         a2,0x25
        800bf7c4 4a e2 02 0c     jal        CreateRectPolyFT4                              
        800bf7c8 07 00 07 24     _li        a3,0x7
        800bf7cc 5c 00 02 24     li         v0,0x5c
        800bf7d0 10 00 a2 af     sw         v0,0x10(sp)
        800bf7d4 a2 00 02 24     li         v0,0xa2
        800bf7d8 14 00 a2 af     sw         v0,0x14(sp)
        800bf7dc 05 00 02 24     li         v0,0x5
        800bf7e0 18 00 a2 af     sw         v0,0x18(sp)
        800bf7e4 06 7b 02 24     li         v0,0x7b06
        800bf7e8 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf7ec 03 00 02 24     li         v0,0x3
        800bf7f0 20 00 a2 af     sw         v0,0x20(sp)
        800bf7f4 74 ff 62 22     addi       v0,s3,-0x8c
        800bf7f8 00 24 02 00     sll        currentPosX,v0,0x10
        800bf7fc 24 00 a0 af     sw         zero,0x24(sp)
        800bf800 21 80 40 00     move       s0,v0
        800bf804 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf808 0b 00 05 24     li         a1,0xb
        800bf80c 17 00 06 24     li         a2,0x17
        800bf810 4a e2 02 0c     jal        CreateRectPolyFT4                                
        800bf814 07 00 07 24     _li        a3,0x7
        800bf818 74 00 02 24     li         v0,0x74
        800bf81c 10 00 a2 af     sw         v0,0x10(sp)
        800bf820 a2 00 02 24     li         v0,0xa2
        800bf824 14 00 a2 af     sw         v0,0x14(sp)
        800bf828 05 00 02 24     li         v0,0x5
        800bf82c 18 00 a2 af     sw         v0,0x18(sp)
        800bf830 06 7b 02 24     li         v0,0x7b06
        800bf834 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf838 03 00 02 24     li         v0,0x3
        800bf83c 20 00 a2 af     sw         v0,0x20(sp)
        800bf840 00 24 10 00     sll        currentPosX,s0,0x10
        800bf844 24 00 a0 af     sw         zero,0x24(sp)
        800bf848 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf84c 1c 00 05 24     li         a1,0x1c
        800bf850 0b 00 06 24     li         a2,0xb
        800bf854 4a e2 02 0c     jal        CreateRectPolyFT4                               
        800bf858 07 00 07 24     _li        a3,0x7
        800bf85c 4c 00 02 24     li         v0,0x4c
        800bf860 10 00 a2 af     sw         v0,0x10(sp)
        800bf864 a9 00 02 24     li         v0,0xa9
        800bf868 14 00 a2 af     sw         v0,0x14(sp)
        800bf86c 05 00 02 24     li         v0,0x5
        800bf870 18 00 a2 af     sw         v0,0x18(sp)
        800bf874 06 7b 02 24     li         v0,0x7b06
        800bf878 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf87c 03 00 02 24     li         v0,0x3
        800bf880 20 00 a2 af     sw         v0,0x20(sp)
        800bf884 00 24 10 00     sll        currentPosX,s0,0x10
        800bf888 24 00 a0 af     sw         zero,0x24(sp)
        800bf88c 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf890 2d 00 05 24     li         a1,0x2d
        800bf894 13 00 06 24     li         a2,0x13
        800bf898 4a e2 02 0c     jal        CreateRectPolyFT4                                
        800bf89c 07 00 07 24     _li        a3,0x7
        800bf8a0 00 24 10 00     sll        currentPosX,s0,0x10
        800bf8a4 10 00 a0 af     sw         zero,0x10(sp)
        800bf8a8 b0 00 02 24     li         v0,0xb0
        800bf8ac 14 00 a2 af     sw         v0,0x14(sp)
        800bf8b0 05 00 02 24     li         v0,0x5
        800bf8b4 18 00 a2 af     sw         v0,0x18(sp)
        800bf8b8 06 7b 02 24     li         v0,0x7b06
        800bf8bc 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf8c0 03 00 02 24     li         v0,0x3
        800bf8c4 20 00 a2 af     sw         v0,0x20(sp)
        800bf8c8 24 00 a0 af     sw         zero,0x24(sp)
        800bf8cc 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf8d0 3e 00 05 24     li         a1,0x3e
        800bf8d4 11 00 06 24     li         a2,0x11
        800bf8d8 4a e2 02 0c     jal        CreateRectPolyFT4                              
        800bf8dc 07 00 07 24     _li        a3,0x7
        800bf8e0 21 80 00 00     clear      s0
        800bf8e4 0a 00 11 24     li         s1,0xa
        800bf8e8 15 00 00 10     b          0x800bf940
        800bf8ec 8f ff 72 22     _addi      s2,s3,-0x71
                             LAB_800bf8f0                                  
        800bf8f0 8a 00 02 24     li         v0,0x8a
        800bf8f4 10 00 a2 af     sw         v0,0x10(sp)
        800bf8f8 8c 00 02 24     li         v0,0x8c
        800bf8fc 14 00 a2 af     sw         v0,0x14(sp)
        800bf900 05 00 02 24     li         v0,0x5
        800bf904 18 00 a2 af     sw         v0,0x18(sp)
        800bf908 06 7b 02 24     li         v0,0x7b06
        800bf90c 1c 00 a2 af     sw         v0,0x1c(sp)
        800bf910 03 00 02 24     li         v0,0x3
        800bf914 20 00 a2 af     sw         v0,0x20(sp)
        800bf918 00 24 12 00     sll        currentPosX,s2,0x10
        800bf91c 00 2c 11 00     sll        a1,s1,0x10
        800bf920 0a 00 07 24     li         a3,0xa
        800bf924 24 00 a0 af     sw         zero,0x24(sp)
        800bf928 03 24 04 00     sra        currentPosX,currentPosX,0x10
        800bf92c 03 2c 05 00     sra        a1,a1,0x10
        800bf930 4a e2 02 0c     jal        CreateRectPolyFT4                              
        800bf934 21 30 e0 00     _move      a2,a3
        800bf938 01 00 10 22     addi       s0,s0,0x1
        800bf93c 11 00 31 22     addi       s1,s1,0x11
                             LAB_800bf940                                  
        800bf940 05 00 01 2a     slti       at,s0,0x5
        800bf944 ea ff 20 14     bne        at,zero,0x800bf8f0
        800bf948 00 00 00 00     _nop
        800bf94c 0e 92 82 87     lh         v0,-0x6df2(gp)=>DAT_80134d3a                  
        800bf950 01 00 01 24     li         at,0x1
        800bf954 8d ff 43 20     addi       v1,v0,-0x73
        800bf958 e3 38 02 3c     lui        v0,0x38e3
        800bf95c 39 8e 42 34     ori        v0,v0,0x8e39
        800bf960 18 00 43 00     mult       v0,v1
        800bf964 10 10 00 00     mfhi       v0
        800bf968 c2 1f 03 00     srl        v1,v1,0x1f
        800bf96c 83 10 02 00     sra        v0,v0,0x2
        800bf970 21 10 43 00     addu       v0,v0,v1
        800bf974 ff 00 45 30     andi       a1,v0,0xff
        800bf978 0c 92 82 87     lh         v0,-0x6df4(gp)=>DAT_80134d38                    
        800bf97c 00 00 00 00     nop
        800bf980 91 ff 44 20     addi       currentPosX,v0,-0x6f
        800bf984 88 88 02 3c     lui        v0,0x8888
        800bf988 89 88 42 34     ori        v0,v0,0x8889
        800bf98c 18 00 44 00     mult       v0,currentPosX
        800bf990 c2 1f 04 00     srl        v1,currentPosX,0x1f
        800bf994 10 10 00 00     mfhi       v0
        800bf998 21 10 44 00     addu       v0,v0,currentPosX
        800bf99c c3 10 02 00     sra        v0,v0,0x3
        800bf9a0 21 10 43 00     addu       v0,v0,v1
        800bf9a4 ff 00 43 30     andi       v1,v0,0xff
        800bf9a8 03 00 61 14     bne        v1,at,0x800bf9b8
        800bf9ac 00 00 00 00     _nop
        800bf9b0 14 00 00 10     b          0x800bfa04
        800bf9b4 05 00 03 24     _li        v1,0x5
                             LAB_800bf9b8                                   
        800bf9b8 02 00 01 24     li         at,0x2
        800bf9bc 03 00 61 14     bne        v1,at,0x800bf9cc
        800bf9c0 00 00 00 00     _nop
        800bf9c4 0f 00 00 10     b          0x800bfa04
        800bf9c8 01 00 03 24     _li        v1,0x1
                             LAB_800bf9cc                                    
        800bf9cc 03 00 01 24     li         at,0x3
        800bf9d0 03 00 61 14     bne        v1,at,0x800bf9e0
        800bf9d4 00 00 00 00     _nop
        800bf9d8 0a 00 00 10     b          0x800bfa04
        800bf9dc 04 00 03 24     _li        v1,0x4
                             LAB_800bf9e0                                  
        800bf9e0 04 00 01 24     li         at,0x4
        800bf9e4 03 00 61 14     bne        v1,at,0x800bf9f4
        800bf9e8 00 00 00 00     _nop
        800bf9ec 05 00 00 10     b          0x800bfa04
        800bf9f0 02 00 03 24     _li        v1,0x2
                             LAB_800bf9f4                                   
        800bf9f4 05 00 01 24     li         at,0x5
        800bf9f8 02 00 61 14     bne        v1,at,0x800bfa04
        800bf9fc 00 00 00 00     _nop
        800bfa00 03 00 03 24     li         v1,0x3
                             LAB_800bfa04                                    
        800bfa04 c0 10 03 00     sll        v0,v1,0x3
        800bfa08 20 10 a2 00     add        v0,a1,v0
        800bfa0c 06 00 01 24     li         at,0x6
        800bfa10 03 00 61 14     bne        v1,at,0x800bfa20
        800bfa14 ff 00 50 30     _andi      s0,v0,0xff
        800bfa18 01 00 02 26     addiu      v0,s0,0x1
        800bfa1c ff 00 50 30     andi       s0,v0,0xff
                             LAB_800bfa20                                   
        800bfa20 ad 97 03 0c     jal        HasMove                                        
        800bfa24 21 20 00 02     _move      currentPosX,s0
        800bfa28 9a 00 40 10     beq        v0,zero,0x800bfc94
        800bfa2c 00 00 00 00     _nop
        800bfa30 12 80 02 3c     lui        v0,0x8012
        800bfa34 00 19 10 00     sll        v1,s0,0x4
        800bfa38 3c 62 42 24     addiu      v0,v0,0x623c
        800bfa3c 21 88 43 00     addu       s1,v0,v1
        800bfa40 40 00 a0 a7     sh         zero,0x40(sp)
        800bfa44 90 00 02 24     li         v0,0x90
        800bfa48 42 00 a2 a7     sh         v0,0x42(sp)
        800bfa4c 78 00 02 24     li         v0,0x78
        800bfa50 44 00 a2 a7     sh         v0,0x44(sp)
        800bfa54 0c 00 02 24     li         v0,0xc
        800bfa58 21 90 00 02     move       s2,s0
        800bfa5c 46 00 a2 a7     sh         v0,0x46(sp)
        800bfa60 f1 32 04 0c     jal        ClearTextSubArea                                
        800bfa64 40 00 a4 27     _addiu     currentPosX,sp,0x40
        800bfa68 12 80 02 3c     lui        v0,0x8012
        800bfa6c 80 18 12 00     sll        v1,s2,0x2
        800bfa70 54 60 42 24     addiu      v0,v0,0x6054
        800bfa74 21 10 43 00     addu       v0,v0,v1
        800bfa78 00 00 44 8c     lw         currentPosX,0x0(v0)=>PTR_s_Fire_Tower_80126054   
        800bfa7c 21 28 00 00     clear      a1
        800bfa80 c9 33 04 0c     jal        RenderString                                     
        800bfa84 90 00 06 24     _li        a2,0x90
        800bfa88 0c 00 02 24     li         v0,0xc
        800bfa8c 10 00 a2 af     sw         v0,0x10(sp)
        800bfa90 14 00 a0 af     sw         zero,0x14(sp)
        800bfa94 90 00 02 24     li         v0,0x90
        800bfa98 18 00 a2 af     sw         v0,0x18(sp)
        800bfa9c 03 00 02 24     li         v0,0x3
        800bfaa0 1c 00 a2 af     sw         v0,0x1c(sp)
        800bfaa4 01 00 02 24     li         v0,0x1
        800bfaa8 20 00 a2 af     sw         v0,0x20(sp)
        800bfaac 76 ff 85 22     addi       a1,s4,-0x8a
        800bfab0 21 20 00 00     clear      currentPosX
        800bfab4 f8 ff 06 24     li         a2,-0x8
        800bfab8 d4 96 03 0c     jal        renderSpriteFromVRAM                           
        800bfabc 78 00 07 24     _li        a3,0x78
        800bfac0 db 8a 02 08     j          0x800a2b6c
        800bfac4 00 00 00 00     _nop
		
		                     LAB_800a2b6c                                   
        800a2b6c 04 00 22 86     lh         v0,0x4(s1)
        800a2b70 00 00 00 00     nop
        800a2b74 13 80 01 3c     lui        at,0x8013
        800a2b78 48 f3 21 8c     lw         at,-0xcb8(at)=>->Partner                         
        800a2b7c 21 20 00 00     clear      a0
        800a2b80 00 00 25 8c     lw         a1,0x0(at)=>Partner
        800a2b84 00 00 00 00     nop
        800a2b88 c0 08 05 00     sll        at,a1,0x3
        800a2b8c 22 08 25 00     sub        at,at,a1
        800a2b90 80 28 01 00     sll        a1,at,0x2
        800a2b94 12 80 01 3c     lui        at,0x8012
        800a2b98 c7 25 21 24     addiu      at,at,0x25c7
        800a2b9c 21 08 25 00     addu       at,at,a1
        800a2ba0 00 00 25 80     lb         a1,0x0(at)=>MoveBoost
        800a2ba4 00 00 00 00     nop
        800a2ba8 06 00 05 16     bne        s0,a1,0x800a2bc4
        800a2bac 00 00 00 00     _nop
        800a2bb0 02 00 04 24     li         a0,0x2
        800a2bb4 00 00 00 00     nop
        800a2bb8 03 00 25 84     lh         a1,0x3(at)=>BoostValue
        800a2bbc 00 00 00 00     nop
        800a2bc0 20 10 45 00     add        v0,v0,a1
                             LAB_800a2bc4                                   
        800a2bc4 9c ff 66 22     addi       a2,s3,-0x64
        800a2bc8 b2 fe 02 08     j          0x800bfac8
        800a2bcc 00 00 00 00     _nop

                             LAB_800bfac8                                   
        800bfac8 10 00 a2 af     sw         v0,0x10(sp)
        800bfacc 03 00 05 24     li         a1,0x3
        800bfad0 14 00 a5 af     sw         a1,0x14(sp)
        800bfad4 21 80 06 00     move       s0,a2
        800bfad8 04 00 05 24     li         a1,0x4
        800bfadc 29 96 03 0c     jal        RenderItemBagNumber                              
        800bfae0 0a 00 07 24     _li        a3,0xa
        800bfae4 06 00 23 92     lbu        v1,0x6(s1)=>MoveMP
        800bfae8 03 00 05 24     li         a1,0x3
        800bfaec 40 10 03 00     sll        v0,v1,0x1
        800bfaf0 20 10 43 00     add        v0,v0,v1
        800bfaf4 10 00 a2 af     sw         v0,0x10(sp)
        800bfaf8 14 00 a5 af     sw         a1,0x14(sp)
        800bfafc 21 20 00 00     clear      currentPosX
        800bfb00 06 00 06 26     addiu      a2,s0,0x6
        800bfb04 29 96 03 0c     jal        RenderItemBagNumber                            
        800bfb08 1b 00 07 24     _li        a3,0x1b
        800bfb0c 0c 00 02 24     li         v0,0xc
        800bfb10 10 00 a2 af     sw         v0,0x10(sp)
        800bfb14 08 00 22 92     lbu        v0,0x8(s1)=>MoveRange
        800bfb18 21 20 00 00     clear      currentPosX
        800bfb1c ff ff 43 20     addi       v1,v0,-0x1
        800bfb20 40 10 03 00     sll        v0,v1,0x1
        800bfb24 20 10 43 00     add        v0,v0,v1
        800bfb28 c0 10 02 00     sll        v0,v0,0x3
        800bfb2c 14 00 a2 af     sw         v0,0x14(sp)
        800bfb30 78 00 02 24     li         v0,0x78
        800bfb34 18 00 a2 af     sw         v0,0x18(sp)
        800bfb38 03 00 02 24     li         v0,0x3
        800bfb3c 1c 00 a2 af     sw         v0,0x1c(sp)
        800bfb40 01 00 02 24     li         v0,0x1
        800bfb44 20 00 a2 af     sw         v0,0x20(sp)
        800bfb48 08 00 05 26     addiu      a1,s0,0x8
        800bfb4c 2b 00 06 24     li         a2,0x2b
        800bfb50 d4 96 03 0c     jal        renderSpriteFromVRAM                            
        800bfb54 14 00 07 24     _li        a3,0x14
        800bfb58 0a 00 22 92     lbu        v0,0xa(s1)=>TechEffect
        800bfb5c 07 00 01 24     li         at,0x7
        800bfb60 05 00 41 14     bne        v0,at,0x800bfb78
        800bfb64 00 00 00 00     _nop
        800bfb68 20 00 07 24     li         a3,0x20
        800bfb6c b0 00 02 24     li         v0,0xb0
        800bfb70 2a 00 00 10     b          0x800bfc1c
        800bfb74 14 00 a2 af     _sw        v0,0x14(sp)
                             LAB_800bfb78                                   
        800bfb78 06 00 01 24     li         at,0x6
        800bfb7c 05 00 41 14     bne        v0,at,0x800bfb94
        800bfb80 00 00 00 00     _nop
        800bfb84 1c 00 07 24     li         a3,0x1c
        800bfb88 94 00 02 24     li         v0,0x94
        800bfb8c 23 00 00 10     b          0x800bfc1c
        800bfb90 14 00 a2 af     _sw        v0,0x14(sp)
                             LAB_800bfb94                                    
        800bfb94 05 00 01 24     li         at,0x5
        800bfb98 05 00 41 14     bne        v0,at,0x800bfbb0
        800bfb9c 00 00 00 00     _nop
        800bfba0 20 00 07 24     li         a3,0x20
        800bfba4 74 00 02 24     li         v0,0x74
        800bfba8 1c 00 00 10     b          0x800bfc1c
        800bfbac 14 00 a2 af     _sw        v0,0x14(sp)
                             LAB_800bfbb0                                   
        800bfbb0 04 00 01 24     li         at,0x4
        800bfbb4 05 00 41 14     bne        v0,at,0x800bfbcc
        800bfbb8 00 00 00 00     _nop
        800bfbbc 1d 00 07 24     li         a3,0x1d
        800bfbc0 57 00 02 24     li         v0,0x57
        800bfbc4 15 00 00 10     b          0x800bfc1c
        800bfbc8 14 00 a2 af     _sw        v0,0x14(sp)
                             LAB_800bfbcc                                    
        800bfbcc 03 00 01 24     li         at,0x3
        800bfbd0 05 00 41 14     bne        v0,at,0x800bfbe8
        800bfbd4 00 00 00 00     _nop
        800bfbd8 20 00 07 24     li         a3,0x20
        800bfbdc 38 00 02 24     li         v0,0x38
        800bfbe0 0e 00 00 10     b          0x800bfc1c
        800bfbe4 14 00 a2 af     _sw        v0,0x14(sp)
                             LAB_800bfbe8                                   
        800bfbe8 02 00 01 24     li         at,0x2
        800bfbec 05 00 41 14     bne        v0,at,0x800bfc04
        800bfbf0 00 00 00 00     _nop
        800bfbf4 1c 00 07 24     li         a3,0x1c
        800bfbf8 1c 00 02 24     li         v0,0x1c
        800bfbfc 07 00 00 10     b          0x800bfc1c
        800bfc00 14 00 a2 af     _sw        v0,0x14(sp)
                             LAB_800bfc04                                  
        800bfc04 01 00 01 24     li         at,0x1
        800bfc08 11 00 41 14     bne        v0,at,0x800bfc50
        800bfc0c 00 00 00 00     _nop
        800bfc10 1a 00 07 24     li         a3,0x1a
        800bfc14 00 00 02 24     li         v0,0x0
        800bfc18 14 00 a2 af     sw         v0,0x14(sp)
                             LAB_800bfc1c                                  
        800bfc1c 0c 00 02 24     li         v0,0xc
        800bfc20 10 00 a2 af     sw         v0,0x10(sp)
        800bfc24 84 00 02 24     li         v0,0x84
        800bfc28 18 00 a2 af     sw         v0,0x18(sp)
        800bfc2c 03 00 02 24     li         v0,0x3
        800bfc30 1c 00 a2 af     sw         v0,0x1c(sp)
        800bfc34 01 00 02 24     li         v0,0x1
        800bfc38 20 00 a2 af     sw         v0,0x20(sp)
        800bfc3c 21 20 00 00     clear      currentPosX
        800bfc40 21 28 00 02     move       a1,s0
        800bfc44 3c 00 06 24     li         a2,0x3c
        800bfc48 d4 96 03 0c     jal        renderSpriteFromVRAM                             
        800bfc4c 00 00 00 00     _nop
                             LAB_800bfc50                                   
        800bfc50 0b 00 23 92     lbu        v1,0xb(s1)=>AllMovementData[0].accuracy
        800bfc54 03 00 05 24     li         a1,0x3
        800bfc58 10 00 a3 af     sw         v1,0x10(sp)
        800bfc5c 14 00 a5 af     sw         a1,0x14(sp)
        800bfc60 21 20 00 00     clear      currentPosX
        800bfc64 06 00 06 26     addiu      a2,s0,0x6
        800bfc68 29 96 03 0c     jal        RenderItemBagNumber                              
        800bfc6c 4e 00 07 24     _li        a3,0x4e
        800bfc70 00 00 00 00     nop
        800bfc74 00 00 00 00     nop
        800bfc78 00 00 00 00     nop
        800bfc7c 00 00 00 00     nop
        800bfc80 00 00 00 00     nop
        800bfc84 00 00 00 00     nop
        800bfc88 00 00 00 00     nop
        800bfc8c 00 00 00 00     nop
        800bfc90 00 00 00 00     nop
                             LAB_800bfc94                                   
        800bfc94 3c 00 bf 8f     lw         ra,0x3c(sp)
        800bfc98 38 00 b4 8f     lw         s4,0x38(sp)
        800bfc9c 34 00 b3 8f     lw         s3,0x34(sp)
        800bfca0 30 00 b2 8f     lw         s2,0x30(sp)
        800bfca4 2c 00 b1 8f     lw         s1,0x2c(sp)
        800bfca8 28 00 b0 8f     lw         s0,0x28(sp)
        800bfcac 08 00 e0 03     jr         ra
        800bfcb0 48 00 bd 27     _addiu     sp,sp,0x48



//Health shoes change

void HandleBasicAnimations(void)
{
//code ignored

	if (((PlayerAnimation == 2) || (PlayerAnimation == 3)) && (GetItemCount(0x25) != 0)) 
	{
		if (0x13 < ShoeStepCount)
		{
			Partner.EntityData.DigimonStats.CurrentDigimonHP = Partner.EntityData.DigimonStats.CurrentDigimonHP + 5;
			Partner.EntityData.DigimonStats.CurrentDigimonMP = Partner.EntityData.DigimonStats.CurrentDigimonMP + 5;
			
			if (Partner.EntityData.DigimonStats.MaxDigimonHP < Partner.EntityData.DigimonStats.CurrentDigimonHP) 
				Partner.EntityData.DigimonStats.CurrentDigimonHP = Partner.EntityData.DigimonStats.MaxDigimonHP;
		
			if (Partner.EntityData.DigimonStats.MaxDigimonMP < Partner.EntityData.DigimonStats.CurrentDigimonMP) 		  
				Partner.EntityData.DigimonStats.CurrentDigimonMP = Partner.EntityData.DigimonStats.MaxDigimonMP;
		  
			ShoeStepCount = 0;
		}
		ShoeStepCount = ShoeStepCount + 1;
	}
}

Disassembly:
							HandleBasicAnimations
							
                             LAB_800df098                                   
        800df098 15 80 01 3c     lui        at,0x8015
        800df09c 9a 57 22 90     lbu        v0,0x579a(at)  //PlayerAnimation    
        800df0a0 02 00 01 24     li         at,0x2
        800df0a4 04 00 41 10     beq        v0,at,0x800df0b8
        800df0a8 00 00 00 00     _nop
        800df0ac 03 00 01 24     li         at,0x3
        800df0b0 2c 00 41 14     bne        v0,at,0x800df164
        800df0b4 00 00 00 00     _nop
                             LAB_800df0b8                                    
        800df0b8 78 14 03 0c     jal        GetItemCount                                     
        800df0bc 25 00 04 24     _li        a0,0x25
        800df0c0 28 00 40 10     beq        v0,zero,0x800df164
        800df0c4 00 00 00 00     _nop
        800df0c8 fa 92 82 93     lbu        v0,-0x6d06(gp)  //ShoeStepCount                   
        800df0cc 00 00 00 00     nop
        800df0d0 14 00 41 2c     sltiu      at,v0,0x14
        800df0d4 1f 00 20 14     bne        at,zero,0x800df154
        800df0d8 00 00 00 00     _nop
        800df0dc 15 80 01 3c     lui        at,0x8015
        800df0e0 f4 57 22 84     lh         v0,0x57f4(at) //CurrentDigimonHP
        800df0e4 00 00 00 00     nop
        800df0e8 05 00 42 20     addi       v0,v0,0x5
        800df0ec f4 57 22 a4     sh         v0,0x57f4(at)
        800df0f0 f6 57 22 84     lh         v0,0x57f6(at) //CurrentDigimonMP 
        800df0f4 00 00 00 00     nop
        800df0f8 05 00 42 20     addi       v0,v0,0x5
        800df0fc f6 57 22 a4     sh         v0,0x57f6(at)
        800df100 15 80 01 3c     lui        at,0x8015
        800df104 f4 57 23 84     lh         v1,0x57f4(at)
        800df108 15 80 01 3c     lui        at,0x8015
        800df10c f0 57 22 84     lh         v0,0x57f0(at)  //MaxDigimonHP
        800df110 00 00 00 00     nop
        800df114 2a 08 43 00     slt        at,v0,v1
        800df118 03 00 20 10     beq        at,zero,0x800df128
        800df11c 21 20 40 00     _move      a0,v0
        800df120 15 80 01 3c     lui        at,0x8015
        800df124 f4 57 24 a4     sh         a0,0x57f4(at)
                             LAB_800df128                                     
        800df128 15 80 01 3c     lui        at,0x8015
        800df12c f6 57 23 84     lh         v1,0x57f6(at)
        800df130 15 80 01 3c     lui        at,0x8015
        800df134 f2 57 22 84     lh         v0,0x57f2(at)  //MaxDigimonMP
        800df138 00 00 00 00     nop
        800df13c 2a 08 43 00     slt        at,v0,v1
        800df140 03 00 20 10     beq        at,zero,0x800df150
        800df144 21 20 40 00     _move      a0,v0
        800df148 15 80 01 3c     lui        at,0x8015
        800df14c f6 57 24 a4     sh         a0,0x57f6(at)
                             LAB_800df150                                   
        800df150 fa 92 80 a3     sb         zero,-0x6d06(gp)  //ShoeStepCount                
                             LAB_800df154                                   
        800df154 fa 92 82 93     lbu        v0,-0x6d06(gp)  //ShoeStepCount                  
        800df158 00 00 00 00     nop
        800df15c 01 00 42 24     addiu      v0,v0,0x1
        800df160 fa 92 82 a3     sb         v0,-0x6d06(gp)  //ShoeStepCount                  
