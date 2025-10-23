//Updated most of the custom code from the hack


//Pretty much the same function, but without jumps to other adresses

//This is not the exact function, just a clean approximation
int GetRookieEvoValue(int digimonType)
{
  uint requirementeScore/division;
  int targetEvolution;
  short currentScore;
  int points;
  byte[6] evoData;
  short bestScore;
  int targetDigimon;
  
  targetDigimon = digimonType - 1;
  if (digimonType == 63) //make Panyjamon use the evolution targets of Weregarurumon (only activates with the Panjyamon patch)
    targetDigimon = 61;
  
  evoData = EvolutionData[targetDigimon].Evolutions;  //EvolutionData, 11 bytes in total: byte[5] preEvolutions & byte[6] Evolutions
  targetEvolution = -1;  
  bestScore = 0;

  for (int i = 0; i < 6; i++)
  {
    points = 0; //Set the points to 0 each start of the loop

    targetDigimon = evoData[i];

    if (targetDigimon == 63) //Change the requirements for Panjyamon/WereGarurumon
      targetDigimon = 01; //Maeson has a 62 here
    
    if (targetDigimon == 64) //Change the requirements for Gigadramon
      targetDigimon = 15; //Maeson has a 62 here
    
    if (targetDigimon == 65) //Change the requirements for MetalEtemon 
      targetDigimon = 29; //Maeson has a 62 here


    if (targetDigimon != -1)
    {
      requirementScore = CalculateRequirementScoreEvo(digimonType, iVar1, ((EvoReq[targetDigimon].Flag & 16) / 16),   //Get the care mistake flag
                        ((EvoReq[targetDigimon].Flag & 1),  //Get the battles flag
                         targetDigimon)
    
      if (2 < requirementScore))
      {
        targetDigimon = evoData[i];
        if (targetDigimon == 63) //Change the requirements for Panjyamon/WereGarurumon
          targetDigimon = 1; 
    
        if (targetDigimon == 64) //Change the requirements for Gigadramon
          targetDigimon = 15; 
    
        if (targetDigimon == 65) //Change the requirements for MetalEtemon 
          targetDigimon = 29; 

        if ((EvoReq[targetDigimon].HP != -1) 
          points = MaxPartnerHP / 10 ;   
      
        division = EvoReq[targetDigimon].HP != -1; //sets the division to 0 if the HP is not a required stat, else is set to 1

        if (EvoReq[targetDigimon].MP != -1) 
        {
          points = points + MaxPartnerMP / 10;
          division++;
        }
        if (EvoReq[targetDigimon].Offense != -1) 
        {
          points = points + PartnerOff;
          division++;
        }
        if (EvoReq[targetDigimon].Defense != -1)
        {  
          points = points + PartnerDef;
          division++;
        }
        if (EvoReq[targetDigimon].Speed != -1) 
        {
          points = points + PartnerSpeed;
          division++;
        }
        if (EvoReq[targetDigimon].Brains != -1) 
        {
          points = points + PartnerBrains;
          division++;
        }
        currentScore = (points / division);

        if (bestScore < currentScore) 
        {
          targetEvolution = targetDigimon;
          bestScore = currentScore;
        }
      }
  
    }
  if (((targetEvolution == -1) && (95 < EvoTimer))
        && (AllDigimonData[digimonType].level == 03)) //check if rookie
    targetEvolution = 11; //set numemon
  
  return targetEvolution;
}



Disassembly:

                             GetRookieEvoValue                              
        800e2bb4 c8 ff bd 27     addiu      sp,sp,-0x38
        800e2bb8 34 00 bf af     sw         ra,0x34(sp)
        800e2bbc 30 00 b6 af     sw         s6,0x30(sp)
        800e2bc0 2c 00 b5 af     sw         s5,0x2c(sp)
        800e2bc4 28 00 b4 af     sw         s4,0x28(sp)
        800e2bc8 24 00 b3 af     sw         s3,0x24(sp)
        800e2bcc 21 b0 04 00     move       s6,a0
        800e2bd0 3f 00 01 24     li         at,0x3f
        800e2bd4 02 00 24 14     bne        at,a0,0x800e2be0
        800e2bd8 ff ff c3 22     _addi      v1,s6,-0x1
        800e2bdc ff ff 63 20     addi       v1,v1,-0x1
                             LAB_800e2be0                                    
        800e2be0 80 10 03 00     sll        v0,v1,0x2
        800e2be4 20 10 43 00     add        v0,v0,v1
        800e2be8 40 10 02 00     sll        v0,v0,0x1
        800e2bec 20 00 b2 af     sw         s2,0x20(sp)
        800e2bf0 1c 00 b1 af     sw         s1,0x1c(sp)
        800e2bf4 20 18 43 00     add        v1,v0,v1
        800e2bf8 13 80 02 3c     lui        v0,0x8013
        800e2bfc 18 00 b0 af     sw         s0,0x18(sp)
        800e2c00 6c b6 42 24     addiu      v0,v0,-0x4994
        800e2c04 21 10 43 00     addu       v0,v0,v1
        800e2c08 00 84 00 00     sll        s0,zero,0x10
        800e2c0c 00 9c 00 00     sll        s3,zero,0x10
        800e2c10 05 00 52 24     addiu      s2,v0,0x5
        800e2c14 ff ff 14 24     li         s4,-0x1
        800e2c18 21 88 00 00     clear      s1
        800e2c1c 03 84 10 00     sra        s0,s0,0x10
        800e2c20 03 9c 13 00     sra        s3,s3,0x10
        800e2c24 b6 00 00 10     b          0x800e2f00
        800e2c28 21 a8 00 00     _clear     s5
                             LAB_800e2c2c                                 
        800e2c2c 00 00 42 82     lb         v0,0x0(s2) //EvolutionData[targetDigimon].Evolutions      
        800e2c30 00 00 00 00     nop
        800e2c34 3f 00 01 24     li         at,0x3f
        800e2c38 02 00 22 14     bne        at,v0,0x800e2c44
        800e2c3c 40 00 01 24     _li        at,0x40
        800e2c40 01 00 02 24     li         v0,0x1
                             LAB_800e2c44                                   
        800e2c44 02 00 22 14     bne        at,v0,0x800e2c50
        800e2c48 41 00 01 24     _li        at,0x41
        800e2c4c 0f 00 02 24     li         v0,0xf
                             LAB_800e2c50                                    
        800e2c50 02 00 22 14     bne        at,v0,0x800e2c5c
        800e2c54 ff ff 01 24     _li        at,-0x1
        800e2c58 1d 00 02 24     li         v0,0x1d
                             LAB_800e2c5c                                   
        800e2c5c 03 00 41 14     bne        v0,at,0x800e2c6c
        800e2c60 21 28 40 00     _move      a1,v0
        800e2c64 a5 00 00 10     b          0x800e2efc
        800e2c68 01 00 52 26     _addiu     s2,s2,0x1
                             LAB_800e2c6c                                   
        800e2c6c c0 10 05 00     sll        v0,a1,0x3
        800e2c70 22 10 45 00     sub        v0,v0,a1
        800e2c74 80 18 02 00     sll        v1,v0,0x2
        800e2c78 13 80 02 3c     lui        v0,0x8013
        800e2c7c 06 ac 42 24     addiu      v0,v0,-0x53fa
        800e2c80 21 10 43 00     addu       v0,v0,v1
        800e2c84 00 00 43 80     lb         v1,0x0(v0) //EvoReq[targetDigimon].Flag
        800e2c88 21 20 c0 02     move       a0,s6
        800e2c8c 00 16 14 00     sll        v0,s4,0x18
        800e2c90 03 16 02 00     sra        v0,v0,0x18
        800e2c94 10 00 a2 af     sw         v0,0x10(sp)
        800e2c98 10 00 62 30     andi       v0,v1,0x10
        800e2c9c 03 11 02 00     sra        v0,0x4
        800e2ca0 00 36 02 00     sll        a2,v0,0x18
        800e2ca4 01 00 62 30     andi       v0,v1,0x1
        800e2ca8 00 3e 02 00     sll        a3,v0,0x18
        800e2cac 03 36 06 00     sra        a2,a2,0x18
        800e2cb0 ae 89 03 0c     jal        0x800e26b8  //CalculateRequirementScoreEvo                    
        800e2cb4 03 3e 07 00     _sra       a3,a3,0x18
        800e2cb8 03 00 41 28     slti       at,v0,0x3
        800e2cbc 8b 00 20 14     bne        at,zero,0x800e2eec
        800e2cc0 00 00 00 00     _nop
        800e2cc4 00 00 44 82     lb         a0,0x0(s2) //evoData[i]
        800e2cc8 00 00 00 00     nop
        800e2ccc 3f 00 01 24     li         at,0x3f
        800e2cd0 02 00 24 14     bne        at,a0,0x800e2cdc
        800e2cd4 40 00 01 24     _li        at,0x40
        800e2cd8 01 00 04 24     li         a0,0x1
                             LAB_800e2cdc                                   
        800e2cdc 02 00 24 14     bne        at,a0,0x800e2ce8
        800e2ce0 41 00 01 24     _li        at,0x41
        800e2ce4 0f 00 04 24     li         a0,0xf
                             LAB_800e2ce8                                    
        800e2ce8 02 00 24 14     bne        at,a0,0x800e2cf4
        800e2cec ff ff 01 24     _li        at,-0x1
        800e2cf0 1d 00 04 24     li         a0,0x1d
                             LAB_800e2cf4                                 
        800e2cf4 c0 10 04 00     sll        v0,a0,0x3
        800e2cf8 22 10 44 00     sub        v0,v0,a0
        800e2cfc 21 18 80 00     move       v1,a0
        800e2d00 13 80 04 3c     lui        a0,0x8013
        800e2d04 80 28 02 00     sll        a1,v0,0x2
        800e2d08 ee ab 84 24     addiu      a0,a0,-0x5412
        800e2d0c 21 20 85 00     addu       a0,a0,a1
        800e2d10 00 00 84 84     lh         a0,0x0(a0) //EvoReq[targetDigimon].HP       
        800e2d14 00 00 00 00     nop
        800e2d18 12 00 81 10     beq        a0,at,0x800e2d64
        800e2d1c 21 10 a0 00     _move      v0,a1
        800e2d20 15 80 01 3c     lui        at,0x8015
        800e2d24 66 66 04 3c     lui        a0,0x6666
        800e2d28 f0 57 25 84     lh         a1,0x57f0(at) //MaxPartnerHP                     
        800e2d2c 67 66 84 34     ori        a0,a0,0x6667
        800e2d30 18 00 85 00     mult       a0,a1
        800e2d34 10 20 00 00     mfhi       a0
        800e2d38 c2 2f 05 00     srl        a1,a1,0x1f
        800e2d3c 83 20 04 00     sra        a0,a0,0x2
        800e2d40 21 20 85 00     addu       a0,a0,a1
        800e2d44 00 24 04 00     sll        a0,a0,0x10
        800e2d48 03 24 04 00     sra        a0,a0,0x10
        800e2d4c 20 20 04 02     add        a0,s0,a0
        800e2d50 00 84 04 00     sll        s0,a0,0x10
        800e2d54 01 00 24 22     addi       a0,s1,0x1
        800e2d58 00 8c 04 00     sll        s1,a0,0x10
        800e2d5c 03 84 10 00     sra        s0,s0,0x10
        800e2d60 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2d64                                   
        800e2d64 13 80 04 3c     lui        a0,0x8013
        800e2d68 f0 ab 84 24     addiu      a0,a0,-0x5410
        800e2d6c 21 20 82 00     addu       a0,a0,v0
        800e2d70 00 00 84 84     lh         a0,0x0(a0)  //EvoReq[targetDigimon].MP        
        800e2d74 ff ff 01 24     li         at,-0x1
        800e2d78 12 00 81 10     beq        a0,at,0x800e2dc4
        800e2d7c 00 00 00 00     _nop
        800e2d80 15 80 01 3c     lui        at,0x8015
        800e2d84 66 66 04 3c     lui        a0,0x6666
        800e2d88 f2 57 25 84     lh         a1,0x57f2(at)  //MaxPartnerMP                     
        800e2d8c 67 66 84 34     ori        a0,a0,0x6667
        800e2d90 18 00 85 00     mult       a0,a1
        800e2d94 10 20 00 00     mfhi       a0
        800e2d98 c2 2f 05 00     srl        a1,a1,0x1f
        800e2d9c 83 20 04 00     sra        a0,a0,0x2
        800e2da0 21 20 85 00     addu       a0,a0,a1
        800e2da4 00 24 04 00     sll        a0,a0,0x10
        800e2da8 03 24 04 00     sra        a0,a0,0x10
        800e2dac 20 20 04 02     add        a0,s0,a0
        800e2db0 00 84 04 00     sll        s0,a0,0x10
        800e2db4 01 00 24 22     addi       a0,s1,0x1
        800e2db8 00 8c 04 00     sll        s1,a0,0x10
        800e2dbc 03 84 10 00     sra        s0,s0,0x10
        800e2dc0 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2dc4                                   
        800e2dc4 13 80 04 3c     lui        a0,0x8013
        800e2dc8 f2 ab 84 24     addiu      a0,a0,-0x540e
        800e2dcc 21 20 82 00     addu       a0,a0,v0
        800e2dd0 00 00 84 84     lh         a0,0x0(a0)  //EvoReq[targetDigimon].Offense                                                                                                     
        800e2dd4 ff ff 01 24     li         at,-0x1
        800e2dd8 0a 00 81 10     beq        a0,at,0x800e2e04
        800e2ddc 00 00 00 00     _nop
        800e2de0 15 80 01 3c     lui        at,0x8015
        800e2de4 e0 57 24 84     lh         a0,0x57e0(at)   //PartnerOffense           
        800e2de8 00 00 00 00     nop
        800e2dec 20 20 04 02     add        a0,s0,a0
        800e2df0 00 84 04 00     sll        s0,a0,0x10
        800e2df4 01 00 24 22     addi       a0,s1,0x1
        800e2df8 00 8c 04 00     sll        s1,a0,0x10
        800e2dfc 03 84 10 00     sra        s0,s0,0x10
        800e2e00 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2e04                                    
        800e2e04 13 80 04 3c     lui        a0,0x8013
        800e2e08 f4 ab 84 24     addiu      a0,a0,-0x540c
        800e2e0c 21 20 82 00     addu       a0,a0,v0
        800e2e10 00 00 84 84     lh         a0,0x0(a0)  //EvoReq[targetDigimon].Defense          
                                                                                           
        800e2e14 ff ff 01 24     li         at,-0x1
        800e2e18 0a 00 81 10     beq        a0,at,0x800e2e44
        800e2e1c 00 00 00 00     _nop
        800e2e20 15 80 01 3c     lui        at,0x8015
        800e2e24 e2 57 24 84     lh         a0,0x57e2(at)   //PartnerDefense       
        800e2e28 00 00 00 00     nop
        800e2e2c 20 20 04 02     add        a0,s0,a0
        800e2e30 00 84 04 00     sll        s0,a0,0x10
        800e2e34 01 00 24 22     addi       a0,s1,0x1
        800e2e38 00 8c 04 00     sll        s1,a0,0x10
        800e2e3c 03 84 10 00     sra        s0,s0,0x10
        800e2e40 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2e44                                 
        800e2e44 13 80 04 3c     lui        a0,0x8013
        800e2e48 f6 ab 84 24     addiu      a0,a0,-0x540a
        800e2e4c 21 20 82 00     addu       a0,a0,v0
        800e2e50 00 00 84 84     lh         a0,0x0(a0)  //EvoReq[targetDigimon].Speed      
        800e2e54 ff ff 01 24     li         at,-0x1
        800e2e58 0a 00 81 10     beq        a0,at,0x800e2e84
        800e2e5c 00 00 00 00     _nop
        800e2e60 15 80 01 3c     lui        at,0x8015
        800e2e64 e4 57 24 84     lh         a0,0x57e4(at)   //PartnerSpeed           
        800e2e68 00 00 00 00     nop
        800e2e6c 20 20 04 02     add        a0,s0,a0
        800e2e70 00 84 04 00     sll        s0,a0,0x10
        800e2e74 01 00 24 22     addi       a0,s1,0x1
        800e2e78 00 8c 04 00     sll        s1,a0,0x10
        800e2e7c 03 84 10 00     sra        s0,s0,0x10
        800e2e80 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2e84                                  
        800e2e84 13 80 04 3c     lui        a0,0x8013
        800e2e88 f8 ab 84 24     addiu      a0,a0,-0x5408
        800e2e8c 21 10 82 00     addu       v0,a0,v0
        800e2e90 00 00 42 84     lh         v0,0x0(v0)  //EvoReq[targetDigimon].Brains                                                                                                 
        800e2e94 ff ff 01 24     li         at,-0x1
        800e2e98 0a 00 41 10     beq        v0,at,0x800e2ec4
        800e2e9c 00 00 00 00     _nop
        800e2ea0 15 80 01 3c     lui        at,0x8015
        800e2ea4 e6 57 22 84     lh         v0,0x57e6(at)    //PartnerBrains   
        800e2ea8 00 00 00 00     nop
        800e2eac 20 10 02 02     add        v0,s0,v0
        800e2eb0 00 84 02 00     sll        s0,v0,0x10
        800e2eb4 01 00 22 22     addi       v0,s1,0x1
        800e2eb8 00 8c 02 00     sll        s1,v0,0x10
        800e2ebc 03 84 10 00     sra        s0,s0,0x10
        800e2ec0 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2ec4                                   
        800e2ec4 1a 00 11 02     div        s0,s1
        800e2ec8 12 10 00 00     mflo       v0
        800e2ecc 00 84 02 00     sll        s0,v0,0x10
        800e2ed0 03 84 10 00     sra        s0,s0,0x10
        800e2ed4 2a 08 70 02     slt        at,s3,s0
        800e2ed8 04 00 20 10     beq        at,zero,0x800e2eec
        800e2edc 00 00 00 00     _nop
        800e2ee0 00 9c 10 00     sll        s3,s0,0x10
        800e2ee4 00 00 54 82     lb         s4,0x0(s2) //EvolutionData[0].Evolutions[i]
                                                                                             
        800e2ee8 03 9c 13 00     sra        s3,s3,0x10
                             LAB_800e2eec                                   
        800e2eec 00 84 00 00     sll        s0,zero,0x10
        800e2ef0 21 88 00 00     clear      s1
        800e2ef4 03 84 10 00     sra        s0,s0,0x10
        800e2ef8 01 00 52 26     addiu      s2,s2,0x1
                             LAB_800e2efc                                    
        800e2efc 01 00 b5 22     addi       s5,s5,0x1
                             LAB_800e2f00                                    
        800e2f00 06 00 a1 2a     slti       at,s5,0x6
        800e2f04 49 ff 20 14     bne        at,zero,0x800e2c2c
        800e2f08 00 00 00 00     _nop
        800e2f0c 40 20 16 00     sll        a0,s6,0x1
        800e2f10 20 20 96 00     add        a0,a0,s6
        800e2f14 80 20 04 00     sll        a0,a0,0x2
        800e2f18 20 20 96 00     add        a0,a0,s6
        800e2f1c 80 08 04 00     sll        at,a0,0x2
        800e2f20 13 80 04 3c     lui        a0,0x8013
        800e2f24 d1 ce 84 24     addiu      a0,a0,-0x312f
        800e2f28 21 20 81 00     addu       a0,a0,at
        800e2f2c 00 00 84 90     lbu        a0,0x0(a0) //AllDigimonData[digimonType].level
        800e2f30 03 00 01 24     li         at,0x3
        800e2f34 0b 00 81 14     bne        a0,at,0x800e2f64
        800e2f38 00 00 00 00     _nop
        800e2f3c 14 80 01 3c     lui        at,0x8014
        800e2f40 b6 84 22 84     lh         v0,-0x7b4a(at)  //EvoTimer                    
        800e2f44 00 00 00 00     nop
        800e2f48 60 00 41 28     slti       at,v0,0x60
        800e2f4c 05 00 20 14     bne        at,zero,0x800e2f64
        800e2f50 00 00 00 00     _nop
        800e2f54 ff ff 01 24     li         at,-0x1
        800e2f58 02 00 81 16     bne        s4,at,0x800e2f64
        800e2f5c 00 00 00 00     _nop
        800e2f60 0b 00 14 24     li         s4,0xb
                             LAB_800e2f64                                  
        800e2f64 00 14 14 00     sll        v0,s4,0x10
        800e2f68 34 00 bf 8f     lw         ra,0x34(sp)
        800e2f6c 30 00 b6 8f     lw         s6,0x30(sp)
        800e2f70 2c 00 b5 8f     lw         s5,0x2c(sp)
        800e2f74 28 00 b4 8f     lw         s4,0x28(sp)
        800e2f78 24 00 b3 8f     lw         s3,0x24(sp)
        800e2f7c 20 00 b2 8f     lw         s2,0x20(sp)
        800e2f80 1c 00 b1 8f     lw         s1,0x1c(sp)
        800e2f84 18 00 b0 8f     lw         s0,0x18(sp)
        800e2f88 03 14 02 00     sra        v0,v0,0x10
        800e2f8c 08 00 e0 03     jr         ra
        800e2f90 38 00 bd 27     _addiu     sp,sp,0x38



			HandleEvoItems Sukamon code

                             LAB_800e2f94                                   
        800e2f94 53 00 01 24     li         at,0x53
        800e2f98 45 82 24 14     bne        at,a0,0x800c389c
        800e2f9c 00 00 00 00     _nop
        800e2fa0 15 80 01 3c     lui        at,0x8015
        800e2fa4 a8 57 23 8c     lw         v1,0x57a8(at)  //PartnerID               
        800e2fa8 00 00 00 00     nop
        800e2fac ff 00 65 30     andi       a1,v1,0xff
        800e2fb0 1d 19 04 0c     jal        0x80106474  //WritePStat                                     
        800e2fb4 05 00 04 24     _li        a0,0x5
        800e2fb8 15 80 01 3c     lui        at,0x8015
        800e2fbc f0 57 22 84     lh         v0,0x57f0(at)    //MaxPartnerHP               
        800e2fc0 f2 57 23 84     lh         v1,0x57f2(at)    //MaxPartnerMP                 
        800e2fc4 14 80 01 3c     lui        at,0x8014
        800e2fc8 d6 84 22 a4     sh         v0,-0x7b2a(at)  //OldHP                          
        800e2fcc d8 84 23 a4     sh         v1,-0x7b28(at)  //OldMP                           
        800e2fd0 15 80 01 3c     lui        at,0x8015
        800e2fd4 e0 57 22 84     lh         v0,0x57e0(at)   //PartnerOff                  
        800e2fd8 e2 57 23 84     lh         v1,0x57e2(at)   //PartnerDef                     
        800e2fdc 14 80 01 3c     lui        at,0x8014
        800e2fe0 da 84 22 a4     sh         v0,-0x7b26(at)  //OldOff                          
        800e2fe4 dc 84 23 a4     sh         v1,-0x7b24(at)  //OldDef                           
        800e2fe8 15 80 01 3c     lui        at,0x8015
        800e2fec e4 57 22 84     lh         v0,0x57e4(at)   //PartnerSpeed                    
        800e2ff0 e6 57 23 84     lh         v1,0x57e6(at)   //PartnerBrains                  
        800e2ff4 14 80 01 3c     lui        at,0x8014
        800e2ff8 de 84 22 a4     sh         v0,-0x7b22(at)  //OldSpeed                        
        800e2ffc e0 84 23 a4     sh         v1,-0x7b20(at)  //OldBrains                       
        800e3000 53 00 04 24     li         a0,0x53
        800e3004 2a 82 00 10     b          0x800c38b0
        800e3008 21 18 00 00     _clear     v1


                             LAB_800e300c                                
        800e300c 0e 80 04 3c     lui        a0,0x800e
        800e3010 40 30 84 24     addiu      a0,a0,0x3040  //MedalRenderData
        800e3014 04 00 05 24     li         a1,0x4
        800e3018 d8 e0 02 0c     jal        0x800b8360   //CreatePrimitiveLines                                  
        800e301c 05 00 06 24     _li        a2,0x5
        800e3020 12 80 04 3c     lui        a0,0x8012
        800e3024 54 47 84 24     addiu      a0,a0,0x4754  //MedalRenderData2
        800e3028 f1 f7 02 08     j          0x800bdfc4
        800e302c 00 00 00 00     _nop

                             WereGarurumon PreEvolutions                    
        800e3030 ff              db         FFh
        800e3031 17              db         17h
        800e3032 16              db         11h
        800e3033 31              db         31h
        800e3034 ff              db         FFh
                             Gigadramon PreEvolutions
        800e3035 15              db         15h
        800e3036 09              db         09h
        800e3037 24              db         24h
        800e3038 05              db         05h
        800e3039 ff              db         FFh
                             MetalEtemon PreEvolutions
        800e303a ff              db         FFh
        800e303b 26              db         26h
        800e303c 22              db         22h
        800e303d 14              db         14h
        800e303e ff              db         FFh

        800e303f 00              db         0h
                             MedalRenderData                              
        800e3040 6e ff           short     FF6Eh                   X1                               
        800e3042 bd ff           short     FFBDh                   Y1
        800e3044 92 00           short     92h                     X2
        800e3046 bd ff           short     FFBDh                   Y2
        800e3048 00              db        0h                      colour
        800e3049 00              db        0h                      filler

        800e304a 6d ff           short     FF6Dh                   X1
        800e304c be ff           short     FFBEh                   Y1
        800e304e 93 00           short     93h                     X2
        800e3050 be ff           short     FFBEh                   Y2
        800e3052 01              db        1h                      colour
        800e3053 00              db        0h                      filler

        800e3054 6e ff           short     FF6Eh                   X1
        800e3056 bf ff           short     FFBFh                   Y1
        800e3058 92 00           short     92h                     X2
        800e305a bf ff           short     FFBFh                   Y2
        800e305c 00              db        0h                      colour
        800e305d 00              db        0h                      filler

        800e305e 47 00           short     47h                     X1
        800e3060 bf ff           short     FFBFh                   Y1
        800e3062 92 00           short     92h                     X2
        800e3064 bf ff           short     FFBFh                   Y2
        800e3066 00              db        0h                      colour
        800e3067 00              db        0h                      filler


                             GiveRandomItem                                  
        800e3068 66 19 04 0c     jal        0x80106598  //GetByteFromScript                                
        800e306c 24 00 a4 27     _addiu     a0,sp,0x24
        800e3070 22 00 a4 27     addiu      a0,sp,0x22
        800e3074 a5 19 04 0c     jal        0x80106694  //GetTwoBytesFromScript                          
        800e3078 20 00 a5 27     _addiu     a1,sp,0x20
        800e307c 22 00 a4 93     lbu        a0,0x22(sp)
        800e3080 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                    
        800e3084 00 00 00 00     _nop
        800e3088 24 00 a4 93     lbu        a0,0x24(sp)
        800e308c 20 00 a5 93     lbu        a1,0x20(sp)
        800e3090 20 20 44 00     add        a0,v0,a0
        800e3094 90 14 03 0c     jal        0x800c5240  //AddInventoryItem                                 
        800e3098 00 00 00 00     _nop
        800e309c 05 00 40 14     bne        v0,zero,0x800e30b4
        800e30a0 00 00 00 00     _nop
        800e30a4 70 19 04 0c     jal        0x801065c0  //SetTrigger                                      
        800e30a8 21 20 00 00     _clear     a0
        800e30ac 03 00 00 10     b          0x800e30bc
        800e30b0 00 00 00 00     _nop
                             LAB_800e30b4                                   
        800e30b4 7f 19 04 0c     jal        0x801065fc  //UnsetTrigger                                   
        800e30b8 21 20 00 00     _clear     a0
                             LAB_800e30bc                                   
        800e30bc 4e 7f 00 10     b          0x80102df8
        800e30c0 00 00 00 00     _nop

                             SetStarter3                                      
        800e30c4 fe 00 04 24     li         a0,0xfe
        800e30c8 1d 19 04 0c     jal        0x80106474  //WritePStat                                     
        800e30cc 02 00 05 24     _li        a1,0x2
        800e30d0 11 00 02 24     li         v0,0x11
        800e30d4 ca 32 04 08     j          0x8010cb28
        800e30d8 ce 94 82 a7     _sh        v0,-0x6b32(gp)  //DAT_80134ffa   
                
                             SetStarter4                                   
        800e30dc fe 00 04 24     li         a0,0xfe
        800e30e0 1d 19 04 0c     jal        0x80106474  //WritePStat                                      
        800e30e4 03 00 05 24     _li        a1,0x3
        800e30e8 11 00 02 24     li         v0,0x11
        800e30ec ca 32 04 08     j          0x8010cb28
        800e30f0 ce 94 82 a7     _sh        v0,-0x6b32(gp)  //DAT_80134ffa 

                   
                             SetStarterTech3                                 
        800e30f4 03 00 01 24     li         at,0x3
        800e30f8 08 00 22 14     bne        at,v0,0x800e311c
        800e30fc 00 00 00 00     _nop
        800e3100 31 00 04 24     li         a0,0x31
        800e3104 15 80 01 3c     lui        at,0x8015
        800e3108 ec 57 24 a0     sb         a0,0x57ec(at)  //Tech1                
        800e310c c5 97 03 0c     jal        0x800e5f14  //LearnMove                                     
        800e3110 13 00 04 24     _li        a0,0x13
        800e3114 31 2e 00 10     b          0x800ee9dc
        800e3118 04 00 10 24     _li        s0,0x4
                             SetStarterTech4                               
        800e311c 01 00 01 24     li         at,0x1
        800e3120 07 00 22 14     bne        at,v0,0x800e3140
        800e3124 00 00 00 00     _nop
        800e3128 31 00 04 24     li         a0,0x31
        800e312c 15 80 01 3c     lui        at,0x8015
        800e3130 ec 57 24 a0     sb         a0,0x57ec(at)  //Tech1                     
        800e3134 c5 97 03 0c     jal        0x800e5f14  //LearnMove                                       
        800e3138 0c 00 04 24     _li        a0,0xc
        800e313c 12 00 10 24     li         s0,0x12
                             LAB_800e3140                                   
        800e3140 26 2e 00 10     b          0x800ee9dc
        800e3144 00 00 00 00     _nop


//Kunemon Start


void main(double param_1)
{
//code ignored
      if ((Choice == 3) || (Choice == 1))
      {
        Tech1 = 49;
        LearnMove(37);  //Poison Claw
        DigimonID = 32;
      }
      else 
      {
        Tech1  = 46;
        LearnMove(12);  //Static Elec
        DigimonID = 32;
      }
//code ignored
}
                             SetStarterTech3                     
        800e30f4 03 00 01 24     li         at,0x3
        800e30f8 0b 00 22 10     beq        at,v0,0x800e3128
        800e30fc 00 00 00 00     _nop
        800e3100 01 00 01 24     li         at,0x1
        800e3104 08 00 22 10     beq        at,v0,0x800e3128
        800e3108 00 00 00 00     _nop
        800e310c 2e 00 04 24     li         a0,0x2e
        800e3110 15 80 01 3c     lui        at,0x8015
        800e3114 ec 57 24 a0     sb         a0,0x57ec(at) //Tech1                    
        800e3118 c5 97 03 0c     jal        0x800e5f14  //LearnMove                                           
        800e311c 0c 00 04 24     _li        a0,0xc
        800e3120 2e 2e 00 10     b          0x800ee9dc
        800e3124 20 00 10 24     _li        s0,0x20
                             SetStarterTech4                                   
        800e3128 31 00 10 24     li         s0,0x31
        800e312c 15 80 01 3c     lui        at,0x8015
        800e3130 ec 57 24 a0     sb         a0,0x57ec(at)  //Tech1                
        800e3134 c5 97 03 0c     jal        0x800e5f14  //LearnMove                                 
        800e3138 25 00 04 24     _li        a0,0x25
        800e313c 20 00 10 24     li         s0,0x20
        800e3140 26 2e 00 10     b          0x800ee9dc
        800e3144 00 00 00 00     _nop


//Unloads the door at Etemon's tree

                             UnloadDoor                                     
        800e3148 66 19 04 0c     jal        GetByteFromScript                              
        800e314c 33 00 a4 27     _addiu     param_1,sp,0x33
        800e3150 33 00 a2 83     lb         v0,0x33(sp)
        800e3154 00 00 00 00     nop
        800e3158 04 00 40 14     bne        v0,zero,LAB_800e316c
        800e315c 00 00 00 00     _nop
        800e3160 a9 0f 04 24     li         param_1,0xfa9
        800e3164 02 8c 02 0c     jal        UnsetObject                                    
        800e3168 21 28 00 00     _clear     a1
                             LAB_800e316c                                   
        800e316c 61 0d 04 08     j          0x80103584
        800e3170 00 00 00 00     _nop


//Gets the evolution for Poyomon

void Script28to3f(int scriptID)
{
  //code ignored
    GetByteFromScript(&local_5); //Just advances the script
    targetDigi = -1;
    DigimonLevel = AllDigimonData[PartnerID].level;
    if (DigimonLevel != 5) //Is not an ultimate
    {
      if (DigimonLevel == 1)       
        targetDigi = GetFreshEvoValue(PartnerID);
      
      else 
      {
        targetDigi = GetRookieEvoValue(PartnerID);
        if ((DigimonLevel == 3) && (targetDigi == -1)) 
          targetDigi = 0xb;      
     }
    }
    WritePStat(110,targetDigi);
//code ignored
}

        80115548 74 31 0e 80     addr       Script28to3f::GetEvoTargetScript

                             GetEvoTargetScript                             
        800e3174 66 19 04 0c     jal        0x80106598  //GetByteFromScript                              
        800e3178 33 00 a4 27     _addiu     a0,sp,0x33
        800e317c 15 80 04 3c     lui        a0,0x8015
        800e3180 a8 57 84 8c     lw         a0,0x57a8(a0)  //PartnerID          
        800e3184 ff 00 05 24     li         a1,0xff
        800e3188 40 10 04 00     sll        v0,a0,0x1
        800e318c 20 10 44 00     add        v0,v0,a0
        800e3190 80 10 02 00     sll        v0,v0,0x2
        800e3194 20 10 44 00     add        v0,v0,a0
        800e3198 80 18 02 00     sll        v1,v0,0x2
        800e319c 13 80 02 3c     lui        v0,0x8013
        800e31a0 d1 ce 42 24     addiu      v0,v0,-0x312f
        800e31a4 21 10 43 00     addu       v0,v0,v1
        800e31a8 00 00 42 90     lbu        v0,0x0(v0)  // AllDigimonData[PartnerID].level
        800e31ac 05 00 01 24     li         at,0x5
        800e31b0 11 00 41 10     beq        v0,at,0x800e31f8
        800e31b4 01 00 01 24     _li        at,0x1
        800e31b8 05 00 41 14     bne        v0,at,0x800e31d0
        800e31bc 34 00 a2 a3     _sb        v0,0x34(sp)
        800e31c0 51 89 03 0c     jal        0x800e2544  //GetFreshEvoValue                                
        800e31c4 00 00 00 00     _nop
        800e31c8 0b 00 00 10     b          0x800e31f8
        800e31cc 21 28 02 00     _move      a1,v0
                             LAB_800e31d0                                  
        800e31d0 ed 8a 03 0c     jal        0x800e2bb4  //GetRookieEvoValue                                
        800e31d4 00 00 00 00     _nop
        800e31d8 34 00 a4 83     lb         a0,0x34(sp)
        800e31dc 03 00 01 24     li         at,0x3
        800e31e0 05 00 81 14     bne        a0,at,0x800e31f8
        800e31e4 21 28 02 00     _move      a1,v0
        800e31e8 ff 00 01 24     li         at,0xff
        800e31ec 02 00 22 14     bne        at,v0,0x800e31f8
        800e31f0 00 00 00 00     _nop
        800e31f4 0b 00 05 24     li         a1,0xb
                             LAB_800e31f8                                    
        800e31f8 1d 19 04 0c     jal        0x80106474  //WritePStat                                      
        800e31fc 6e 00 04 24     _li        a0,0x6e
        800e3200 61 0d 04 08     j          0x80103584
        800e3204 00 00 00 00     _nop



                             WereGarurumonNodes
        800e3208 ff ff           dw         FFFFh
        800e320a ff 00           dw         FFh
        800e320c 00 01           dw         100h
        800e320e 01 02           dw         201h
        800e3210 02 03           dw         302h
        800e3212 03 02           dw         203h
        800e3214 04 05           dw         504h
        800e3216 05 06           dw         605h
        800e3218 06 02           dw         206h
        800e321a 07 08           dw         807h
        800e321c 08 09           dw         908h
        800e321e 09 01           dw         109h
        800e3220 0a 0b           dw         B0Ah
        800e3222 0b 0c           dw         C0Bh
        800e3224 0c 0d           dw         D0Ch
        800e3226 0d 0e           dw         E0Dh
        800e3228 0e 0b           dw         B0Eh
        800e322a 0f 10           dw         100Fh
        800e322c 10 11           dw         1110h
        800e322e 11 12           dw         1211h
        800e3230 00 00           dw         0h
        800e3232 00 00           dw         0h



//Add the care mistakes and battles to the menu, fix some visual issues

//Same as before, this is not exact function, just a cleaner representation
void RenderStatsMenu(void)
{
  //code ignored

    renderSpriteFromVRAM(0,-108, -66, 144, 12, 0, 60,5,0); //Renders the longer partner name

    //renderSpriteFromVRAM(colorID, posX, posY, uvWidht, uvHeight, uvX, uvY, offset, hasShadow)

  //code ignored

    partnerStats = *Partner.DigimonStats;  //This is an array of shorts: 0 is Offense, 1 is Defense, 2 is Speed, 3 is Brains
    posY = 31;
    loop = 0;
    do 
    {

      //RenderNumber(colorID, posX, posY, digits, value, flag)

      renderNumber(0, 35, posY, 4, PartnerStats[loop], 5);

      //RenderSegmentedStatsBar(Filled, total, lenght, posX, posY)

      RenderSegmentedStatsBar(PartnerStats[loop], 999, 50, 36, posY + 11);
      posY = posY + 15;
      keepLooping = loop < 3;
      loop = loop + 1;
    } while (keepLooping);    

    renderNumber(0,0x23,-0x22,2,CareMistakes,5);  //Renders the amount of care mistakes
    renderNumber(0,0x6d,-0x22,2,Battles,5);  //Renders the amount of battles

    //RenderSquare(posX, posY, width, height, param_5)    
	
    RenderSquare(192, 84, 30, 14, 5);  //Square for the care mistakes
    RenderSquare(264, 84, 30, 14, 5);  //Square for the battles
    RenderNumber(0, -84, 36, 3, Tiredness, 0);  //Renders the current tiredness

    RenderSegmentedBar(-0x54,0x52,VirusBar * 3,6,200,200,0x3c,0,5);

  //code ignored
}

located at: 800bb610

	800bb7c4 90 00 07 24     _li        a3,0x90


        800bb954 0c 00 02 24     li         v0,0xc
        800bb958 10 00 a2 af     sw         v0,0x10(sp)
        800bb95c 15 80 01 3c     lui        at,0x8015
        800bb960 f0 57 24 84     lh         a0,0x57f0(at)  //MaxPartnerHP
        800bb964 0f 27 05 24     li         a1,0x270f
        800bb968 64 00 06 24     li         a2,0x64
        800bb96c 0a fb 02 0c     jal        RenderSegmentedStatsBar                          
        800bb970 24 00 07 24     _li        a3,0x24
        800bb974 1b 00 02 24     li         v0,0x1b
        800bb978 10 00 a2 af     sw         v0,0x10(sp)
        800bb97c 15 80 01 3c     lui        at,0x8015
        800bb980 f2 57 24 84     lh         a0,0x57f2(at)  //MaxPartnerMP
        800bb984 0f 27 05 24     li         a1,0x270f
        800bb988 64 00 06 24     li         a2,0x64
        800bb98c 0a fb 02 0c     jal        RenderSegmentedStatsBar                          
        800bb990 24 00 07 24     _li        a3,0x24
        800bb994 15 80 10 3c     lui        s0,0x8015
        800bb998 e0 57 10 26     addiu      s0,s0,0x57e0  //Partner.DigimonStats
        800bb99c 1f 00 11 24     li         s1,0x1f
        800bb9a0 10 00 a2 af     sw         v0,0x10(sp)
        800bb9a4 21 90 00 00     clear      s2

                             LAB_800bb9a4                                    
        800bb9a8 00 00 02 86     lh         v0,0x0(s0)  //partnerStats[loop]
        800bb9ac 21 20 00 00     clear      a0
        800bb9b0 10 00 a2 af     sw         v0,0x10(sp)
        800bb9b4 05 00 02 24     li         v0,0x5
        800bb9b8 14 00 a2 af     sw         v0,0x14(sp)
        800bb9bc 23 00 05 24     li         a1,0x23
        800bb9c0 21 30 11 00     move       a2,s1
        800bb9c4 11 95 03 0c     jal        0x800e5444  //renderNumber                                     
        800bb9c8 04 00 07 24     _li        a3,0x4
        800bb9cc 0b 00 22 26     addiu      v0,s1,0xb
        800bb9d0 00 00 04 86     lh         a0,0x0(s0)  //partnerStats[loop]
        800bb9d4 e7 03 05 24     li         a1,0x3e7
        800bb9d8 32 00 06 24     li         a2,0x32
        800bb9dc 10 00 a2 af     sw         v0,0x10(sp)
        800bb9e0 0a fb 02 0c     jal        0x800bec28  //RenderSegmentedStatsBar                         
        800bb9e4 24 00 07 24     _li        a3,0x24
        800bb9e8 02 00 10 26     addiu      s0,s0,0x2
        800bb9ec 0f 00 31 26     addiu      s1,s1,0xf
        800bb9f0 03 00 41 2a     slti       at,s2,0x3
        800bb9f4 ec ff 20 14     bne        at,zero,0x800bb9a8
        800bb9f8 01 00 52 26     _addiu     s2,s2,0x1
        800bb9fc 14 80 01 3c     lui        at,0x8014
        800bba00 b2 84 22 84     lh         v0,-0x7b4e(at)  //CareMistakes                  
        800bba04 21 20 00 00     clear      a0
        800bba08 10 00 a2 af     sw         v0,0x10(sp)
        800bba0c 05 00 02 24     li         v0,0x5
        800bba10 14 00 a2 af     sw         v0,0x14(sp)
        800bba14 25 00 05 24     li         a1,0x25
        800bba18 de ff 06 24     li         a2,-0x22
        800bba1c 11 95 03 0c     jal        0x800e5444  //renderNumber                                    
        800bba20 03 00 07 24     _li        a3,0x3
        800bba24 23 00 05 24     li         a1,0x23
        800bba28 14 80 01 3c     lui        at,0x8014
        800bba2c b4 84 22 84     lh         v0,-0x7b4c(at)  //Battles                         
        800bba30 21 20 00 00     clear      a0
        800bba34 10 00 a2 af     sw         v0,0x10(sp)
        800bba38 05 00 02 24     li         v0,0x5
        800bba3c 14 00 a2 af     sw         v0,0x14(sp)
        800bba40 6d 00 05 24     li         a1,0x6d
        800bba44 de ff 06 24     li         a2,-0x22
        800bba48 11 95 03 0c     jal        0x800e5444  //renderNumber                                  
        800bba4c 03 00 07 24     _li        a3,0x3
        800bba50 05 00 02 24     li         v0,0x5
        800bba54 10 00 a2 af     sw         v0,0x10(sp)
        800bba58 c0 00 04 24     li         a0,0xc0
        800bba5c 54 00 05 24     li         a1,0x54
        800bba60 1e 00 06 24     li         a2,0x1e
        800bba64 cd f9 02 0c     jal        0x800be734  //RenderSquare                                    
        800bba68 0e 00 07 24     _li        a3,0xe
        800bba6c 05 00 02 24     li         v0,0x5
        800bba70 10 00 a2 af     sw         v0,0x10(sp)
        800bba74 08 01 04 24     li         a0,0x108
        800bba78 54 00 05 24     li         a1,0x54
        800bba7c 1e 00 06 24     li         a2,0x1e
        800bba80 cd f9 02 0c     jal        0x800be734  //RenderSquare                                    
        800bba84 0e 00 07 24     _li        a3,0xe
        800bba88 23 00 05 24     li         a1,0x23
        800bba8c 14 80 01 3c     lui        at,0x8014
        800bba90 82 84 22 84     lh         v0,-0x7b7e(at)  //Tiredness                      
        800bba94 03 00 04 24     li         a0,0x3
        800bba98 10 00 a2 af     sw         v0,0x10(sp)
        800bba9c 00 00 02 24     li         v0,0x0
        800bbaa0 14 00 a2 af     sw         v0,0x14(sp)
        800bbaa4 ad ff 05 24     li         a1,-0x53
        800bbaa8 21 00 06 24     li         a2,0x21
        800bbaac 11 95 03 0c     jal        0x800e5444  //renderNumber                             
        800bbab0 03 00 07 24     _li        a3,0x3
        800bbab4 21 80 00 00     clear      s0
        800bbab8 21 88 00 00     clear      s1
        800bbabc 4c 00 00 10     b          0x800bbb88
        800bbac0 09 00 12 24     _li        s2,0x9







New UI stats data:

Square UI 
Format: Pos X (short), Pos Y (short), Width (short) and height (short)

32 00 34 00 70 00 0E 00 
32 00 44 00 F4 00 0E 00 
32 00 54 00 1A 00 0E 00 
7A 00 54 00 1A 00 0E 00 
4B 00 AB 00 33 00 08 00 
4B 00 BA 00 33 00 08 00 
4B 00 C9 00 33 00 08 00 
C2 00 82 00 68 00 05 00 
C2 00 91 00 68 00 05 00 
C2 00 A0 00 36 00 05 00 
C2 00 AF 00 36 00 05 00 
C2 00 BE 00 36 00 05 00 
C2 00 CD 00 36 00 05 00 

Text UI

Format: Pos X (short), Pos Y (short), Width (short), UV X (byte) and UV Y (byte) (this one always assumes the height is 12)

7A FF 20 00 30 00 48 30 
82 FF 40 00 24 00 18 30 
82 FF 4F 00 24 00 78 30 
FC FF 04 00 18 00 9C 30 
FC FF 13 00 18 00 B4 30 
FC FF 22 00 24 00 00 24 
FC FF 31 00 24 00 24 24 
FC FF 40 00 24 00 70 24 
FC FF 4F 00 24 00 48 24 

Poly FT4 UI

Format: Pos X (short), Pos Y (short), Width (byte), height (byte), UV X (byte) and UV Y (byte)

74 FF AF FF 35 0B 00 4B 
74 FF F4 FF 35 0B 00 56 
EE FF F4 FF 35 0B 00 61 
76 FF BE FF 1A 0A 80 30 
76 FF CE FF 1A 0A 9A 30 
BD FF DE FF 1C 0A B4 30 
7B FF DE FF 13 0A 80 3A 
34 00 C2 FF 19 08 00 38 
09 00 C2 FF 19 08 19 38 
55 00 C2 FF 19 08 32 38 
AC FF 22 00 21 0E 81 52 
EF FF 04 00 0B 0B 93 3A 
EF FF 13 00 0B 0B 9E 3A 
EF FF 22 00 0B 0B A9 3A 
EF FF 31 00 0B 0B B4 3A 
EF FF 40 00 0B 0B BF 3A 
EF FF 4F 00 0A 0B CA 3A 
82 FF 31 00 28 0C D6 3A 
FC FF DE FF 22 0A 80 46 
4A 00 DE FF 1C 0A A3 46 
77 FF 4F 00 09 0B 2C 74


Located at: 14D649C4


CLUT data:
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
00 00 00

Located at: 14D64380


Line Primitive:

Format: Pos X1 (short), Pos Y1 (short), Pos X2 (short), Pos Y2 (short), color (byte) and empty (byte)

E5 FF F1 FF E5 FF 61 00 00 00 
E6 FF F0 FF E6 FF 62 00 01 00 
E7 FF F1 FF E7 FF 61 00 00 00 
6E FF EE FF 92 00 EE FF 00 00 
6D FF EF FF 93 00 EF FF 01 00 
6E FF F0 FF 92 00 F0 FF 00 00

Located at: 14D6451C



//Render bubble status adjustment

//These are just the position Y of the current status of your digimon (sick, hungry...), the squares going up and down on the stats menu

        800bed8c 90 ff 54 20     _addi      s4,v0,-0x70

	800bee74 90 ff a2 22     addi       v0,s5,-0x70




//Fix some visual issues in the overworld UI

//The first two is the width of the small bars at the bottom right of the screen (happiness and discipline)
//The last one is the value to render properly the discipline bar filling

        800a9414 4c 00 06 24     li         a2,0x4c

        800a9450 4c 00 06 24     li         a2,0x4c

        800a97a4 02 00 06 24     li         a2,0x2



// Name length changes

        8010a1f4 00 00 00 00     nop
        
        8010a20c 00 00 46 a0     sb         a2,0x0(v0)=>DAT_801b1d1c            

        8010a21c 00 00 40 a0     sb         zero,0x0(v0)=>DAT_801b1d1d                    
    
        8010a228 05 00 01 24     li         at,0x5

        8010a230 00 00 00 00     _nop

        8010a264 0b 00 01 24     li         at,0xb

        8010ab78 21 18 02 00     move       v1,v0

        80109fd8 00 00 00 00     nop

        80109fe4 0c 00 01 24     li         at,0xc






// Birdramon flying locations change, check the original function in the repository

void SetBirdramonFlyingLocations(void)
{
//code ignored
   for (int i = 0; i < 8; i++) 
   {
//code ignored
      if (CurrentMoney < (short)BirdramonLocations[i].travelFee) 
//code ignored
    }
}

        80107b28 00 00 43 84     lh         v1,0x0(v0)  //TravelFee
         
        80107b70 06 00 52 22     addi       s2,s2,0x6                   
        80107b74 08 00 21 2e     sltiu      at,s1,0x8


Location Data:

* Format: byte mapID, byte placement, short trigger, short cost

26 09 DD 00 E8 03 
23 00 ED 00 E8 03 
46 09 BE 00 E8 03 
51 00 BC 00 DC 05 
5D 01 5F 01 D0 07 
8C 01 3F 01 C4 09 
77 09 93 00 C4 09 
69 00 D2 00 C4 09

Located at: 14D725C4


// Merit limit changes

        80103024 30 75 41 28     slti       at,v0,0x7530

        80103030 2f 75 02 24     li         v0,0x752f

        80103064 30 75 41 28     slti       at,v0,0x7530

        80103070 2f 75 02 24     li         v0,0x752f




        8010be54 30 75 41 28     slti       at,v0,0x7530

        8010be60 2f 75 02 24     li         v0,0x752f   


//Fix memory leak at Shogun Gekomon merit shop
     
        8010bddc ce 94 82 a7     sh         v0,-0x6b32(gp)
        8010bde0 e8 94 83 a7     sh         v1,-0x6b18(gp)

       



// Render Digimon name below Player name in the save/load menu

RenderLoadDataBoxes(int currentBox,short cPosX,short cPosY)
{
//code ignored
  if ((&SaveFileStrings)[currentBox].fileNumber != 0) {
    DrawText(prim_00,cPosX + 0x28,cPosY + 5,'\x18',vStart,0x90,0xc,colour);
    prim_00 = prim + 0x1e;
    DrawText(prim + 0x14,cPosX + 0x28,cPosY + 0x13,'\x18',cVar1 + '\x18',0xe0,0xc,colour);
  }
//code ignored
}


        8010d968 21 20 00 02     move       a0,s0
        8010d96c 28 00 65 22     addi       a1,s3,0x28
        8010d970 10 00 b2 af     sw         s2,0x10(sp)
        8010d974 90 00 02 24     li         v0,0x90
        8010d978 14 00 a2 af     sw         v0,0x14(sp)
        8010d97c 0c 00 02 24     li         v0,0xc
        8010d980 18 00 a2 af     sw         v0,0x18(sp)
        8010d984 1c 00 b1 af     sw         s1,0x1c(sp)
        8010d988 28 00 90 24     addiu      s0,a0,0x28
        8010d98c 21 b0 a0 00     move       s6,a1
        8010d990 21 30 a0 02     move       a2,s5
        8010d994 58 34 04 0c     jal        0x8010d160  //DrawText                                        
        8010d998 18 00 07 24     _li        a3,0x18
        8010d99c 0c 00 42 22     addi       v0,s2,0xc
        8010d9a0 10 00 a2 af     sw         v0,0x10(sp)
        8010d9a4 e0 00 02 24     li         v0,0xe0
        8010d9a8 14 00 a2 af     sw         v0,0x14(sp)
        8010d9ac 0c 00 02 24     li         v0,0xc
        8010d9b0 18 00 a2 af     sw         v0,0x18(sp)
        8010d9b4 21 20 00 02     move       a0,s0
        8010d9b8 1c 00 b1 af     sw         s1,0x1c(sp)
        8010d9bc 28 00 90 24     addiu      s0,a0,0x28
        8010d9c0 13 00 86 22     addi       a2,s4,0x13
        8010d9c4 21 28 c0 02     move       a1,s6
        8010d9c8 58 34 04 0c     jal        0x8010d160  //DrawText                                         
        8010d9cc 18 00 07 24     _li        a3,0x18
        8010d9d0 0b 00 00 10     b          0x8010da00
        8010d9d4 00 00 00 00     _nop
        8010d9d8 00 00 00 00     nop
        8010d9dc 00 00 00 00     nop
        8010d9e0 00 00 00 00     nop
        8010d9e4 00 00 00 00     nop
        8010d9e8 00 00 00 00     nop
        8010d9ec 00 00 00 00     nop
        8010d9f0 00 00 00 00     nop
        8010d9f4 00 00 00 00     nop
        8010d9f8 00 00 00 00     nop
        8010d9fc 00 00 00 00     nop



void RenderSaveDataConfirmation(void)
{
//code ignored
else 
  {
    DrawText(prim + 2, 88, 46, 24, 15, 144, 12, 0);
    DrawText(prim + 3, 88, 60, 0, 24, 128, 12, 0);
    prim_00 = prim + 5;  
  }
//code ignored
}


        8010dcf0 0c 00 03 24     li         v1,0xc
        8010dcf4 21 20 00 02     move       a0,s0
        8010dcf8 10 00 a3 af     sw         v1,0x10(sp)
        8010dcfc 90 00 02 24     li         v0,0x90
        8010dd00 14 00 a2 af     sw         v0,0x14(sp)
        8010dd04 18 00 a3 af     sw         v1,0x18(sp)
        8010dd08 1c 00 a0 af     sw         zero,0x1c(sp)
        8010dd0c 28 00 90 24     addiu      s0,a0,0x28
        8010dd10 58 00 05 24     li         a1,0x58
        8010dd14 2e 00 06 24     li         a2,0x2e
        8010dd18 58 34 04 0c     jal        0x8010d160  //DrawText                                       
        8010dd1c 18 00 07 24     _li        a3,0x18
        8010dd20 18 00 02 24     li         v0,0x18
        8010dd24 10 00 a2 af     sw         v0,0x10(sp)
        8010dd28 80 00 02 24     li         v0,0x80
        8010dd2c 14 00 a2 af     sw         v0,0x14(sp)
        8010dd30 0c 00 02 24     li         v0,0xc
        8010dd34 18 00 a2 af     sw         v0,0x18(sp)
        8010dd38 21 20 00 02     move       a0,s0
        8010dd3c 1c 00 a0 af     sw         zero,0x1c(sp)
        8010dd40 28 00 90 24     addiu      s0,a0,0x28
        8010dd44 58 00 05 24     li         a1,0x58
        8010dd48 3c 00 06 24     li         a2,0x3c
        8010dd4c 58 34 04 0c     jal        0x8010d160  //DrawText                                         
        8010dd50 21 38 00 00     _clear     a3
        8010dd54 28 00 10 26     addiu      s0,s0,0x28
        8010dd58 19 00 00 10     b          0x8010ddc0
        8010dd5c f0 00 02 24     _li        v0,0xf0
        8010dd60 00 00 00 00     nop
        8010dd64 00 00 00 00     nop
        8010dd68 00 00 00 00     nop
        8010dd6c 00 00 00 00     nop
        8010dd70 00 00 00 00     nop
        8010dd74 00 00 00 00     nop
        8010dd78 00 00 00 00     nop
        8010dd7c 00 00 00 00     nop
        8010dd80 00 00 00 00     nop
        8010dd84 00 00 00 00     nop
        8010dd88 00 00 00 00     nop


