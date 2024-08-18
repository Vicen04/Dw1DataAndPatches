This is a file that shows all the extra changes done in the hack
Check the wiki to learn how this can be modified to make your own version.



//Evolution fix and changes
//The function used to get the evolution target for a rookie and a champion was put together using only the rookie one
//If you want to see the original code, check SydMontague repository

int GetRookieEvoValue(int digimonType)

{
  uint requirementeScore/division;
  int iVar1;
  short sVar2;
  int points;
  char *targetType;
  short sVar3;
  int targetDigimon;
  int iVar4;
  
  targetDigimon = digimonType + -1;
  if (digimonType == 63) //make Panyjamon use the evolution targets of Weregarurumon, Maeson has this but never uses it due to Panyjamon being a Ultimate in his hack
    targetDigimon = 61;
  
  targetType = &DataForTargetDigimon + targetDigimon * 11;
  targetDigimon = -1;
  sVar3 = 0;

  for (iVar4 = 0; iVar4 < 6; iVar4 = iVar4 + 1)
  {
    points = 0 //Set the points to 0 each start of the loop

    iVar1 = *targetType;

    if (iVar1 == 63) //Change the digimon data Panjyamon uses as target data
      iVar1 = 48; //Maeson has a 62 here
    
    if (iVar1 == 64) //Change the digimon data Gigadramon  uses as target data
      iVar1 = 54; //Maeson has a 62 here
    
    if (iVar1 == 65) //Change the digimon data MetalEtemon uses as target data
      iVar1 = 42; //Maeson has a 62 here

//this is actually done after checking the (iVar1 != -1), but to make it easier to read, I left it here
    requirementScore = CalculateRequirementScoreEvo(digimonType,iVar1,
                        (((&DataFromDigimonRaise)[iVar1 * 28] & 16) / 16),   //Get the care mistake flag
                        ((&DataFromDigimonRaise)[iVar1 * 28] & 1),  //Get the battles flag
                         targetDigimon)
    
    if ((iVar1 != -1) && (2 < requirementScore))
    {
      iVar1 = *targetType;

      if (iVar1 == 63) //Change the digimon data Panyjamon uses as stats requirement data
        iVar1 = 48; //Maeson has a 62 here
    
      if (iVar1 == 64) //Change the digimon data Gigadramon uses as stats requirement data
        iVar1 = 54; //Maeson has a 62 here
     
      if (iVar1 == 65) //Change the digimon data MetalEtemon uses as stats requirement data
        iVar1 = 42; //Maeson has a 62 here

      if ((&TargetHP)[iVar1 * 14] != -1) 
        points = MaxDigimonHP / 10 ;   
      
      division = (&TargetHP)[iVar1 * 14] != -1; //sets the division to 0 if the HP is not a required stat, else is set to 1

      if ((&TargetMP)[iVar1 * 14] != -1) 
      {
        points = points + MaxDigimonMP / 10;
        division = division + 1;
      }
      if ((&TargetOff)[iVar1 * 14] != -1) 
      {
        points = points + DigimonOff;
        division = division + 1;
      }
      if ((&TargetDef)[iVar1 * 14] != -1)
      {
        points = points + DigimonDef;
        division = division + 1;
      }
      if ((&TargetSpeed)[iVar1 * 14] != -1) 
      {
        points = points + DigimonSpeed;
        division = division + 1;
      }
      if ((&TargetBrains)[iVar1 * 14] != -1) 
      {
        points = points + DigimonBrains;
        division = division + 1;
      }
      sVar2 = (points / division);

      if (sVar3 < sVar2) 
      {
        targetDigimon = *targetType;
        sVar3 = sVar2;
      }
    }
    targetType = targetType + 1;
  }
  if (((targetDigimon == -1) && (95 < EvoTimer))
        && ((&DigimonLevel)[digimonType * 0x34] == 03)) //check if rookie
    targetDigimon = 11; //set numemon
  
  return targetDigimon;
}

Disassembly:
                                                   
                             GetRookieEvoValue   

         Offset       Hex         Command                            
        800e2bb4 c8 ff bd 27     addiu      sp,sp,-0x38
        800e2bb8 34 00 bf af     sw         ra,0x34(sp)
        800e2bbc 30 00 b6 af     sw         s6,0x30(sp)
        800e2bc0 2c 00 b5 af     sw         s5,0x2c(sp)
        800e2bc4 28 00 b4 af     sw         s4,0x28(sp)
        800e2bc8 24 00 b3 af     sw         s3,0x24(sp)
        800e2bcc 43 01 00 10     b          0x800e30dc
        800e2bd0 3f 00 01 24     _li        at,0x3f


//Jumps here
                                 LAB_800e30dc                                    
        800e30dc 21 b0 04 00     move       s6,a0
        800e30e0 02 00 24 14     bne        at,a0,LAB_800e30ec
        800e30e4 ff ff c3 22     _addi      v1,s6,-0x1
        800e30e8 ff ff 63 20     addi       v1,v1,-0x1
                             LAB_800e30ec                                      
        800e30ec b9 fe 00 10     b          LAB_800e2bd4
        800e30f0 00 00 00 00     _nop


//Goes back
                             LAB_800e2bd4                                    
        800e2bd4 80 10 03 00     sll        v0,v1,0x2
        800e2bd8 20 10 43 00     add        v0,v0,v1
        800e2bdc 40 10 02 00     sll        v0,v0,0x1
        800e2be0 20 00 b2 af     sw         s2,0x20(sp)
        800e2be4 1c 00 b1 af     sw         s1,0x1c(sp)
        800e2be8 20 18 43 00     add        v1,v0,v1
        800e2bec 13 80 02 3c     lui        v0,0x8013
        800e2bf0 18 00 b0 af     sw         s0,0x18(sp)
        800e2bf4 6c b6 42 24     addiu      v0,v0,-0x4994
        800e2bf8 21 10 43 00     addu       v0,v0,v1
        800e2bfc 00 84 00 00     sll        s0,zero,0x10
        800e2c00 00 9c 00 00     sll        s3,zero,0x10
        800e2c04 05 00 52 24     addiu      s2,v0,0x5
        800e2c08 ff ff 14 24     li         s4,-0x1
        800e2c0c 21 88 00 00     clear      s1
        800e2c10 03 84 10 00     sra        s0,s0,0x10
        800e2c14 03 9c 13 00     sra        s3,s3,0x10
        800e2c18 a4 00 00 10     b          0x800e2eac
        800e2c1c 21 a8 00 00     _clear     s5
                             LAB_800e2c20                                     
        800e2c20 b9 00 00 10     b          0x00e2f08
        800e2c24 00 00 00 00     _nop

//jumps here
                             LAB_800e2f08                                    
        800e2f08 00 00 42 82     lb         v0,0x0(s2) //DataForTargetDigimon               
        800e2f0c 00 00 00 00     nop
        800e2f10 3f 00 01 24     li         at,0x3f
        800e2f14 02 00 22 14     bne        at,v0,0x800e2f20
        800e2f18 00 00 00 00     _nop
//Vice
        800e2f1c 30 00 02 24     li         v0,0x30 
//Maeson 
        800e2f1c 3e 00 02 24     li         v0,0x3e
                             LAB_800e2f20                                     
        800e2f20 40 00 01 24     li         at,0x40
        800e2f24 02 00 22 14     bne        at,v0,0x800e2f30
        800e2f28 00 00 00 00     _nop
//Vice
        800e2f2c 36 00 02 24     li         v0,0x36 
//Maeson 
        800e2f2c 3e 00 02 24     li         v0,0x3e
                             LAB_800e2f30                                   
        800e2f30 41 00 01 24     li         at,0x41
        800e2f34 02 00 22 14     bne        at,v0,0x800e2f40
        800e2f38 00 00 00 00     _nop
//Vice
        800e2f3c 2a 00 02 24     li         v0,0x2a 
//Maeson 
        800e2f3c 3e 00 02 24     li         v0,0x3e
                             LAB_800e2f40                                  
        800e2f40 ff ff 01 24     li         at,-0x1
        800e2f44 38 ff 00 10     b          0x800e2c28
        800e2f48 00 00 00 00     _nop
                             LAB_800e2f4c                                   
        800e2f4c 00 00 44 82     lb         a0,0x0(s2) //DataForTargetDigimon               
        800e2f50 00 00 00 00     nop
        800e2f54 3f 00 01 24     li         at,0x3f 
        800e2f58 02 00 24 14     bne        at,a0,0x800e2f64
        800e2f5c 00 00 00 00     _nop
//Vice
        800e2f60 30 00 04 24     li         a0,0x30 
//Maeson 
        800e2f60 3e 00 04 24     li         a0,0x3e 
                             LAB_800e2f64                                    
        800e2f64 40 00 01 24     li         at,0x40
        800e2f68 02 00 24 14     bne        at,a0,0x800e2f74
        800e2f6c 00 00 00 00     _nop
//Vice
        800e2f70 36 00 04 24     li         a0,0x36
//Maeson 
        800e2f70 3e 00 04 24     li         a0,0x3e 
                             LAB_800e2f74                                  
        800e2f74 41 00 01 24     li         at,0x41
        800e2f78 02 00 24 14     bne        at,a0,LAB_800e2f84
        800e2f7c 00 00 00 00     _nop
//Vice
        800e2f80 2a 00 04 24     li         a0,0x2a 
//Maeson 
        800e2f80 3e 00 04 24     li         a0,0x3e 
                             LAB_800e2f84                                  
        800e2f84 ff ff 01 24     li         at,-0x1
        800e2f88 43 ff 00 10     b          0x800e2c98
        800e2f8c 00 00 00 00     _nop


//jumps back
                             LAB_800e2c28                                   
        800e2c28 03 00 41 14     bne        v0,at,0x800e2c38
        800e2c2c 21 28 40 00     _move      a1,v0
        800e2c30 9d 00 00 10     b          0x800e2ea8
        800e2c34 01 00 52 26     _addiu     s2,s2,0x1
                             LAB_800e2c38                                   
        800e2c38 c0 10 05 00     sll        v0,a1,0x3
        800e2c3c 22 10 45 00     sub        v0,v0,a1
        800e2c40 80 18 02 00     sll        v1,v0,0x2
        800e2c44 13 80 02 3c     lui        v0,0x8013
        800e2c48 06 ac 42 24     addiu      v0,v0,-0x53fa
        800e2c4c 21 10 43 00     addu       v0,v0,v1
        800e2c50 00 00 43 80     lb         v1,0x0(v0) //DataFromDigimonRaise                
        800e2c54 21 20 c0 02     move       a0,s6
        800e2c58 00 16 14 00     sll        v0,s4,0x18
        800e2c5c 03 16 02 00     sra        v0,v0,0x18
        800e2c60 10 00 a2 af     sw         v0,local_28(sp)
        800e2c64 10 00 62 30     andi       v0,v1,0x10
        800e2c68 03 11 02 00     sra        v0,v0,0x4
        800e2c6c 00 36 02 00     sll        a2,v0,0x18
        800e2c70 01 00 62 30     andi       v0,v1,0x1
        800e2c74 00 3e 02 00     sll        a3,v0,0x18
        800e2c78 03 36 06 00     sra        a2,a2,0x18
        800e2c7c ae 89 03 0c     jal        0x800e26b8  //CalculateRequirementScoreEvo                    
        800e2c80 03 3e 07 00     _sra       a3,a3,0x18
        800e2c84 03 00 41 28     slti       at,v0,0x3
        800e2c88 82 00 20 14     bne        at,zero,0x800e2e94
        800e2c8c 00 00 00 00     _nop
        800e2c90 ae 00 00 10     b          0x_800e2f4c
        800e2c94 00 00 00 00     _nop
                             LAB_800e2c98                                
        800e2c98 c0 10 04 00     sll        v0,a0,0x3
        800e2c9c 22 10 44 00     sub        v0,v0,a0
        800e2ca0 21 18 80 00     move       v1,digimonType
        800e2ca4 13 80 04 3c     lui        a0,0x8013
        800e2ca8 80 28 02 00     sll        a1,v0,0x2
        800e2cac ee ab 84 24     addiu      a0,a0,-0x5412
        800e2cb0 21 20 85 00     addu       a0,a0,a1
        800e2cb4 00 00 84 84     lh         a0,0x0(a0) //TargetHP                                                                                               
        800e2cb8 00 00 00 00     nop
        800e2cbc 12 00 81 10     beq        a0,at,0x800e2d08
        800e2cc0 21 10 a0 00     _move      v0,a1
        800e2cc4 15 80 01 3c     lui        at,0x8015
        800e2cc8 66 66 04 3c     lui        a0,0x6666
        800e2ccc f0 57 25 84     lh         a1,0x57f0(at) //MaxDigimonHP                    
        800e2cd0 67 66 84 34     ori        a0,a0,0x6667
        800e2cd4 18 00 85 00     mult       a0,a1
        800e2cd8 10 20 00 00     mfhi       a0
        800e2cdc c2 2f 05 00     srl        a1,a1,0x1f
        800e2ce0 83 20 04 00     sra        a0,a0,0x2
        800e2ce4 21 20 85 00     addu       a0,a0,a1
        800e2ce8 00 24 04 00     sll        a0,a0,0x10
        800e2cec 03 24 04 00     sra        a0,a0,0x10
        800e2cf0 20 20 04 02     add        a0,s0,a0
        800e2cf4 00 84 04 00     sll        s0,a0,0x10
        800e2cf8 01 00 24 22     addi       a0,s1,0x1
        800e2cfc 00 8c 04 00     sll        s1,a0,0x10
        800e2d00 03 84 10 00     sra        s0,s0,0x10
        800e2d04 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2d08                                   
        800e2d08 13 80 04 3c     lui        a0,0x8013
        800e2d0c f0 ab 84 24     addiu      a0,a0,-0x5410
        800e2d10 21 20 82 00     addu       a0,a0,v0
        800e2d14 00 00 84 84     lh         a0,0x0(a0) //TargetMP           
                                                                                            
        800e2d18 ff ff 01 24     li         at,-0x1
        800e2d1c 12 00 81 10     beq        a0,at,0x800e2d68
        800e2d20 00 00 00 00     _nop
        800e2d24 15 80 01 3c     lui        at,0x8015
        800e2d28 66 66 04 3c     lui        a0,0x6666
        800e2d2c f2 57 25 84     lh         a1,0x57f2(at) //MaxDigimonMP                      
        800e2d30 67 66 84 34     ori        a0,a0,0x6667
        800e2d34 18 00 85 00     mult       a0,a1
        800e2d38 10 20 00 00     mfhi       a0
        800e2d3c c2 2f 05 00     srl        a1,a1,0x1f
        800e2d40 83 20 04 00     sra        a0,a0,0x2
        800e2d44 21 20 85 00     addu       a0,a0,a1
        800e2d48 00 24 04 00     sll        a0,a0,0x10
        800e2d4c 03 24 04 00     sra        a0,a0,0x10
        800e2d50 20 20 04 02     add        a0,s0,a0
        800e2d54 00 84 04 00     sll        s0,a0,0x10
        800e2d58 01 00 24 22     addi       a0,s1,0x1
        800e2d5c 00 8c 04 00     sll        s1,a0,0x10
        800e2d60 03 84 10 00     sra        s0,s0,0x10
        800e2d64 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2d68                                   
        800e2d68 13 80 04 3c     lui        a0,0x8013
        800e2d6c f2 ab 84 24     addiu      a0,a0,-0x540e
        800e2d70 21 20 82 00     addu       a0,a0,v0
        800e2d74 00 00 84 84     lh         a0,0x0(a0) //TargetOff         
                                                                                            
        800e2d78 ff ff 01 24     li         at,-0x1
        800e2d7c 0a 00 81 10     beq        a0,at,0x800e2da8
        800e2d80 00 00 00 00     _nop
        800e2d84 15 80 01 3c     lui        at,0x8015
        800e2d88 e0 57 24 84     lh         a0,0x57e0(at)  //DigimonOff             
        800e2d8c 00 00 00 00     nop
        800e2d90 20 20 04 02     add        a0,s0,a0
        800e2d94 00 84 04 00     sll        s0,a0,0x10
        800e2d98 01 00 24 22     addi       a0,s1,0x1
        800e2d9c 00 8c 04 00     sll        s1,a0,0x10
        800e2da0 03 84 10 00     sra        s0,s0,0x10
        800e2da4 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2da8                                  
        800e2da8 13 80 04 3c     lui        a0,0x8013
        800e2dac f4 ab 84 24     addiu      a0,a0,-0x540c
        800e2db0 21 20 82 00     addu       a0,a0,v0
        800e2db4 00 00 84 84     lh         a0,0x0(a0) //TargetDef          
                                                                                            
        800e2db8 ff ff 01 24     li         at,-0x1
        800e2dbc 0a 00 81 10     beq        a0,at,0x800e2de8
        800e2dc0 00 00 00 00     _nop
        800e2dc4 15 80 01 3c     lui        at,0x8015
        800e2dc8 e2 57 24 84     lh         a0,0x57e2(at) //DigimonDef                
        800e2dcc 00 00 00 00     nop
        800e2dd0 20 20 04 02     add        a0,s0,a0
        800e2dd4 00 84 04 00     sll        s0,a0,0x10
        800e2dd8 01 00 24 22     addi       a0,s1,0x1
        800e2ddc 00 8c 04 00     sll        s1,a0,0x10
        800e2de0 03 84 10 00     sra        s0,s0,0x10
        800e2de4 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2de8                                    
        800e2de8 13 80 04 3c     lui        a0,0x8013
        800e2dec f6 ab 84 24     addiu      a0,a0,-0x540a
        800e2df0 21 20 82 00     addu       a0,a0,v0
        800e2df4 00 00 84 84     lh         a0,0x0(a0) //TargetSpeed       
                                                                                             
        800e2df8 ff ff 01 24     li         at,-0x1
        800e2dfc 0a 00 81 10     beq        a0,at,0x800e2e28
        800e2e00 00 00 00 00     _nop
        800e2e04 15 80 01 3c     lui        at,0x8015
        800e2e08 e4 57 24 84     lh         a0,0x57e4(at)  //DigimonSpeed           
        800e2e0c 00 00 00 00     nop
        800e2e10 20 20 04 02     add        a0,s0,a0
        800e2e14 00 84 04 00     sll        s0,a0,0x10
        800e2e18 01 00 24 22     addi       a0,s1,0x1
        800e2e1c 00 8c 04 00     sll        s1,a0,0x10
        800e2e20 03 84 10 00     sra        s0,s0,0x10
        800e2e24 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2e28                                    
        800e2e28 13 80 04 3c     lui        a0,0x8013
        800e2e2c f8 ab 84 24     addiu      a0,a0,-0x5408
        800e2e30 21 10 82 00     addu       v0,a0,v0
        800e2e34 00 00 42 84     lh         v0,0x0(v0) //TargetBrains 
                                                                                            
        800e2e38 ff ff 01 24     li         at,-0x1
        800e2e3c 0a 00 41 10     beq        v0,at,0x800e2e68
        800e2e40 00 00 00 00     _nop
        800e2e44 15 80 01 3c     lui        at,0x8015
        800e2e48 e6 57 22 84     lh         v0,0x57e6(at) //DigimonBrains
        800e2e4c 00 00 00 00     nop
        800e2e50 20 10 02 02     add        v0,s0,v0
        800e2e54 00 84 02 00     sll        s0,v0,0x10
        800e2e58 01 00 22 22     addi       v0,s1,0x1
        800e2e5c 00 8c 02 00     sll        s1,v0,0x10
        800e2e60 03 84 10 00     sra        s0,s0,0x10
        800e2e64 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800e2e68                                   
        800e2e68 1a 00 11 02     div        s0,s1
        800e2e6c 12 10 00 00     mflo       v0
        800e2e70 00 84 02 00     sll        s0,v0,0x10
        800e2e74 03 84 10 00     sra        s0,s0,0x10
        800e2e78 2a 08 70 02     slt        at,s3,s0
        800e2e7c 05 00 20 10     beq        at,zero,0x800e2e94
        800e2e80 00 00 00 00     _nop
        800e2e84 00 9c 10 00     sll        s3,s0,0x10
        800e2e88 00 00 54 82     lb         s4,0x0(s2) //DataForTargetDigimon  
        800e2e8c 00 00 00 00     nop
        800e2e90 03 9c 13 00     sra        s3,s3,0x10
                             LAB_800e2e94                                   
        800e2e94 00 84 00 00     sll        s0,zero,0x10
        800e2e98 21 88 00 00     clear      s1
        800e2e9c 03 84 10 00     sra        s0,s0,0x10
        800e2ea0 01 00 52 26     addiu      s2,s2,0x1
        800e2ea4 00 00 00 00     nop
                             LAB_800e2ea8                                   
        800e2ea8 01 00 b5 22     addi       s5,s5,0x1
                             LAB_800e2eac                                   
        800e2eac 06 00 a1 2a     slti       at,s5,0x6
        800e2eb0 5b ff 20 14     bne        at,zero,0x800e2c20
        800e2eb4 00 00 00 00     _nop
        800e2eb8 ff ff 01 24     li         at,-0x1
        800e2ebc 06 00 81 16     bne        s4,at,0x800e2ed8
        800e2ec0 00 00 00 00     _nop
        800e2ec4 14 80 01 3c     lui        at,0x8014
        800e2ec8 b6 84 22 84     lh         v0,-0x7b4a(at) //EvoTimer
        800e2ecc 00 00 00 00     nop
        800e2ed0 2f 00 00 10     b          0x800e2f90
        800e2ed4 00 00 00 00     _nop

                             LAB_800e2ed8                                   
        800e2ed8 00 14 14 00     sll        v0,s4,0x10
        800e2edc 34 00 bf 8f     lw         ra,0x34(sp)
        800e2ee0 30 00 b6 8f     lw         s6,0x30(sp)
        800e2ee4 2c 00 b5 8f     lw         s5,0x2c(sp)
        800e2ee8 28 00 b4 8f     lw         s4,0x28(sp)
        800e2eec 24 00 b3 8f     lw         s3,0x24(sp)
        800e2ef0 20 00 b2 8f     lw         s2,0x20(sp)
        800e2ef4 1c 00 b1 8f     lw         s1,0x1c(sp)
        800e2ef8 18 00 b0 8f     lw         s0,0x18(sp)
        800e2efc 03 14 02 00     sra        v0,v0,0x10
        800e2f00 08 00 e0 03     jr         ra
        800e2f04 38 00 bd 27     _addiu     sp,sp,0x38


//read if the EvoTimer is bigger than 95

                             LAB_800e2f90                                   
        800e2f90 60 00 41 28     slti       at,v0,0x60
        800e2f94 0f 00 20 14     bne        at,zero,0x800e2fd4
        800e2f98 00 00 00 00     _nop
        800e2f9c 40 20 16 00     sll        a0,s6,0x1
        800e2fa0 20 20 96 00     add        a0,a0,s6
        800e2fa4 80 20 04 00     sll        a0,a0,0x2
        800e2fa8 20 20 96 00     add        a0,a0,s6
        800e2fac 80 08 04 00     sll        at,a0,0x2
        800e2fb0 13 80 04 3c     lui        a0,0x8013
        800e2fb4 d1 ce 84 24     addiu      a0,a0,-0x312f
        800e2fb8 21 20 81 00     addu       a0,a0,at
        800e2fbc 00 00 84 90     lbu        a0,0x0(a0) //DigimonLevel
        800e2fc0 00 00 00 00     nop
        800e2fc4 03 00 01 24     li         at,0x3
        800e2fc8 02 00 81 14     bne        a0,at,0x800e2fd4
        800e2fcc 00 00 00 00     _nop
        800e2fd0 0b 00 14 24     li         s4,0xb
                             LAB_800e2fd4                                   
        800e2fd4 c0 ff 00 10     b          0x800e2ed8
        800e2fd8 00 00 00 00     _nop



//Changes in the evolution items
//If you want to see the original code, check SydMontague repository

void HandleEvoItems(int EvoItemValue)

{
  ushort evolutionTarget;
  
  if (EvoItemValue < 125) //Makes sure that is inside the normal items value
  {
    if (EvoItemValue == 114) // Moon Mirror, Maeson has a 83 (Electro Ring) here; this is read if you evolve to Sukamon using an item
    {
      EvoItemValue = 5;
      WritePStat(EvoItemValue,DigimonType); //Store the old digimon in case the player interacts with King Sukamon
      OldHp = MaxDigimonHP;
      OldMP = MaxDigimonMP;
      OldOff = DigimonOff;
      OldDef = DigimonDef;
      OldSpeed = DigimonSpeed;
      OldBrains = DigimonBrains;
      EvoItemValue = 114;  //it was overwritten before, so it has to be set again
    }
	;//old code
    if (EvoItemValue == 70) //Chain Melon, exclusive to my hack, Maeson does not have this
      evolutionTarget = 62;
	  
	;// new code
	 if (EvoItemValue == 124) //AS decoder
      evolutionTarget = 62;
    
    else 
    {
      evolutionTarget = (&ItemsValues)[EvoItemValue];
      if (((&DigimonLevel)[(&ItemsValues)[EvoItemValue] * 0x34] - 1) != (&DigimonLevel)[*EntityPtr * 0x34]) //checks that the digimon is 1 level lower than the digimon it has to evolve
        return;
      
    }
  }
  else 
  {
    if (EvoItemValue == 126) // Noble Mane
      evolutionTarget = 63;
    
    if (EvoItemValue == 125) //Giga Hand
      evolutionTarget = 64; //Maeson has a 62 here
    
    if (EvoItemValue == 127) // Metal Banana
      evolutionTarget = 65;
    
  }
  EvoItemFlag = 1; // disable stat gains
  _DigimonEvoValue = evolutionTarget;
  FUN_800c55fc(); //sets the item to be destroyed
  FUN_800db238();
  SetMenuState(6);
  SetDigimonState(13);                   
  return;
}

Disassembly:

                      HandleEvoItems

         Offset       Hex         Command  
        800c3860 e8 ff bd 27     addiu      sp,sp,-0x18
        800c3864 7d 00 81 28     slti       at,a0,0x7d
        800c3868 dc 7d 20 14     bne        at,zero,0x800e2fdc
        800c386c 10 00 bf af     _sw        ra,0x10(sp)

//only if the item is higher than 124
        800c3870 7e 00 01 24     li         at,0x7e
        800c3874 02 00 81 14     bne        a0,at,0x800c3880
        800c3878 7d 00 01 24     _li        at,0x7d
        800c387c 3f 00 02 24     li         v0,0x3f
                             LAB_800c3880                                   
        800c3880 02 00 81 14     bne        a0,at,0x800c388c
        800c3884 7f 00 01 24     _li        at,0x7f
        800c3888 40 00 02 24     li         v0,0x40
                             LAB_800c388c                                   
        800c388c 02 00 24 14     bne        at,a0,0x800c3898
        800c3890 00 00 00 00     _nop
        800c3894 41 00 02 24     li         v0,0x41
                             LAB_800c3898                                    
        800c3898 25 00 00 10     b          0x800c3930

//jumps back here if the item is lower than 125

//Vice
                             LAB_800c389c                                  
        800c389c 46 00 01 24     _li        at,0x46
        800c38a0 03 00 81 14     bne        a0,at,0x800c38b0
        800c38a4 00 00 00 00     _nop
        800c38a8 21 00 00 10     b          0x800c3930
        800c38ac 3e 00 02 24     _li        v0,0x3e

//Vice 1.11.2 and Maeson 
                             LAB_800c389c       
        800c389c 7c 00 01 24     _li        at,0x7c
        800c38a0 03 00 81 14     bne        a0,at,0x800c38b0
        800c38a4 00 00 00 00     _nop
        800c38a8 21 00 00 10     b          0x800c3930
        800c38ac 3e 00 02 24     _li        v0,0x3e


                             LAB_800c38b0                       
        800c38b0 12 80 02 3c     lui        v0,0x8012
        800c38b4 13 80 06 3c     lui        a2,0x8013
        800c38b8 b9 ff 83 20     addi       v1,a0,-0x47
        800c38bc 5c 7c 42 24     addiu      v0,v0,0x7c5c
        800c38c0 21 10 43 00     addu       v0,v0,v1
        800c38c4 00 00 44 90     lbu        a0,0x0(v0) //ItemsValues                                                                                               
        800c38c8 d1 ce c6 24     addiu      a2,a2,-0x312f //DigimonLevel
        800c38cc 40 18 04 00     sll        v1,a0,0x1
        800c38d0 20 18 64 00     add        v1,v1,a0
        800c38d4 80 18 03 00     sll        v1,v1,0x2
        800c38d8 20 18 64 00     add        v1,v1,a0
        800c38dc 80 18 03 00     sll        v1,v1,0x2
        800c38e0 21 18 c3 00     addu       v1,a2,v1
        800c38e4 00 00 63 90     lbu        v1,0x0(v1) //DigimonLevel                         
        800c38e8 21 10 80 00     move       v0,a0
        800c38ec ff ff 64 20     addi       a0,v1,-0x1
        800c38f0 13 80 01 3c     lui        at,0x8013
        800c38f4 00 2c 04 00     sll        a1,a0,0x10
        800c38f8 48 f3 23 8c     lw         v1,-0xcb8(at) //EntityPtr
        800c38fc 03 2c 05 00     sra        a1,a1,0x10
        800c3900 00 00 64 8c     lw         a0,0x0(v1)
        800c3904 00 00 00 00     nop
        800c3908 40 18 04 00     sll        v1,a0,0x1
        800c390c 20 18 64 00     add        v1,v1,a0
        800c3910 80 18 03 00     sll        v1,v1,0x2
        800c3914 20 18 64 00     add        v1,v1,a0
        800c3918 80 18 03 00     sll        v1,v1,0x2
        800c391c 21 18 c3 00     addu       v1,a2,v1
        800c3920 00 00 63 90     lbu        v1,0x0(v1) //DigimonLevel
        800c3924 00 00 00 00     nop
        800c3928 0c 00 a3 14     bne        a1,v1,0x800c395c
        800c392c 00 00 00 00     _nop
                             LAB_800c3930                                   
        800c3930 24 93 82 a7     sh         v0,-0x6cdc(gp) //DigimonEvoValue
                             EndEvolution
        800c3934 01 00 02 24     li         v0,0x1
        800c3938 26 93 82 a3     sb         v0,-0x6cda(gp) //EvoItemFlag
        800c393c 7f 15 03 0c     jal        0x800c55fc    //FUN_800c55fc                                  
        800c3940 00 00 00 00     _nop
        800c3944 8e 6c 03 0c     jal        0x800db238  //FUN_800db238                                   
        800c3948 00 00 00 00     _nop
        800c394c 62 a8 02 0c     jal        0x800aa188  //SetMenuState                                     
        800c3950 06 00 04 24     _li        a0,0x6
        800c3954 34 7d 03 0c     jal        0x800df4d0  //SetDigimonState                                  
        800c3958 0d 00 04 24     _li        a0,0xd
                             LAB_800c395c                                    
        800c395c 10 00 bf 8f     lw         ra,0x10(sp)
        800c3960 00 00 00 00     nop
        800c3964 08 00 e0 03     jr         ra
        800c3968 18 00 bd 27     _addiu     sp,sp,0x18


//jumps here if the item is lower than 125
                             LAB_800e2fdc                                   
        800e2fdc 72 00 01 24     li         at,0x72
        800e2fe0 2e 82 24 14     bne        at,a0,0x800c389c
        800e2fe4 00 00 00 00     _nop
        800e2fe8 15 80 01 3c     lui        at,0x8015
        800e2fec a8 57 23 8c     lw         v1,0x57a8(at) //Digimon Type
        800e2ff0 00 00 00 00     nop
        800e2ff4 ff 00 65 30     andi       a1,v1,0xff
        800e2ff8 1d 19 04 0c     jal        0x80106474  //WritePStat                                      
        800e2ffc 05 00 04 24     _li        a0,0x5
        800e3000 15 80 01 3c     lui        at,0x8015
        800e3004 f0 57 22 84     lh         v0,0x57f0(at) //MaxDigimonHP
        800e3008 f2 57 23 84     lh         v1,0x57f2(at) //MaxDigimonMP
        800e300c 14 80 01 3c     lui        at,0x8014
        800e3010 d6 84 22 a4     sh         v0,-0x7b2a(at) //OldHP
        800e3014 d8 84 23 a4     sh         v1,-0x7b28(at) //OldMP
        800e3018 15 80 01 3c     lui        at,0x8015
        800e301c e0 57 22 84     lh         v0,0x57e0(at) //DigimonOff
        800e3020 e2 57 23 84     lh         v1,0x57e2(at) //DigimonDef
        800e3024 14 80 01 3c     lui        at,0x8014
        800e3028 da 84 22 a4     sh         v0,-0x7b26(at) //OldOff
        800e302c dc 84 23 a4     sh         v1,-0x7b24(at) //OldDef
        800e3030 15 80 01 3c     lui        at,0x8015
        800e3034 e4 57 22 84     lh         v0,0x57e4(at) //DigimonSpeed
        800e3038 e6 57 23 84     lh         v1,0x57e6(at) //DigimonBrains
        800e303c 14 80 01 3c     lui        at,0x8014
        800e3040 de 84 22 a4     sh         v0,-0x7b22(at) //OldSpeed
        800e3044 e0 84 23 a4     sh         v1,-0x7b20(at) //OldBrains
        800e3048 72 00 04 24     li         a0,0x72
        800e304c 13 82 00 10     b          0x800c389c
        800e3050 21 18 00 00     _clear     v1



//Change the trigger to enable the extra digimon flags

Original:

void SetDigimonAdquiredTrigger(DigimonID)
{
  if (DigimonID < 63) 
    SetTrigger(DigimonID + 0x200 & 0xffff);
  
  return;
}


Changed:

void SetDigimonAdquiredTrigger(DigimonID)
{
  if (DigimonID < 66) //this causes a small glitch
    SetTrigger(DigimonID + 0x200 & 0xffff); 
  
  return;
}


Disassembly:

                    SetDigimonAdquiredTrigger

                Offset       Hex         Command  
Original:                          

        800ff7fc 3f 00 81 2c     sltiu      at,a0,0x3f

Changed:
       
        800ff7fc 42 00 81 2c     sltiu      at,a0,0x42


GLITCH: 
* If you obtain Panyjamon before speaking with Jijimon about the recruitment of Agumon, the game will skip that dialogue. If you recruit Agumon and speak about Jijimon about it, the game will show Panyjamon in the chart even if you never obtained it.
* If you obtain Gigadramon before speaking with Jijimon about the recruitment of Betamon, the game will skip that dialogue. If you recruit Betamon and speak about Jijimon about it, the game will show Gigadramon in the chart even if you never obtained it.
* If you obtain Metal Etemon before speaking with Jijimon about the recruitment of Greymon, the game will skip that dialogue. If you recruit Greymon and speak about Jijimon about it, the game will show Metal Etemon in the chart even if you never obtained it.
 


//Chart changes, this is just to display the extra digimon in the small chart

//here I'm overwriting and changing the way the medal data is rendered to be able to add later the digimon missing in the chart
Original:

void RenderMedalMenu(void)

{
//code ignored

  FUN_800b8360(&MedalRenderDataOriginal,14,5);  //this does render the lines surrounding the medals

//code ignored
}

Changed:

void RenderMedalMenu(void)

{
//code ignored

  FUN_800b8360(&MedalRenderData,4,5);  //Split in two, to read in different addresses
  FUN_800b8360(&MedalRenderDataOriginal + 40,11,5);

//code ignored
}


Disassembly:

                           RenderMedalMenu

                Offset       Hex         Command  
Original:
                             LAB_800bdfbc                                   
        800bdfbc 12 80 04 3c     lui        a0,0x8012
        800bdfc0 2c 47 84 24     addiu      a0,a0,0x472c    //MedalRenderDataOriginal                 


Changed:
                             LAB_800bdfbc                                   
        800bdfbc 20 8c 03 08     j          0x800e3080
        800bdfc0 00 00 00 00     _nop


                             LAB_800e3080                                  
        800e3080 0e 80 04 3c     lui        a0,0x800e
        800e3084 b4 30 84 24     addiu      a0,a0,0x30b4 //MedalRendeData
        800e3088 04 00 05 24     li         a1,0x4
        800e308c d8 e0 02 0c     jal        0x800b8360 //0x800b8360  //In charge on rendering                                 
        800e3090 05 00 06 24     _li        a2,0x5
        800e3094 12 80 04 3c     lui        a0,0x8012
        800e3098 54 47 84 24     addiu      a0,a0,0x4754  //MedalRenderDataOriginal +40 offset
        800e309c f1 f7 02 08     j          0x800bdfc4
        800e30a0 00 00 00 00     _nop

                             PanjyamonPreEvoTargets  
      
                               800e30a4 ff             
                               800e30a5 ff             
                               800e30a6 11         //Gabumon
                               800e30a7 ff             
                               800e30a8 ff             

                             gigadramonPreEvo

                               800e30a9 ff           
                               800e30aa 3f        //Panjyamon
                               800e30ab 15        //Birdramon
                               800e30ac ff                         
                               800e30ad ff             

                             metalEtemonPreEvo

                               800e30ae ff            
                               800e30af 09        //Meramon
                               800e30b0 3f        //Panjyamon
                               800e30b1 ff                     
                               800e30b2 ff             
                               800e30b3 00             

                             MedalRenderData

                              800e30b4 6e ff         
                              800e30b6 bd ff         
                              800e30b8 92 00         
                              800e30ba bd ff          
                              800e30bc 00 00           
                              800e30be 6d ff         
                              800e30c0 be ff          
                              800e30c2 93 00          
                              800e30c4 be ff           
                              800e30c6 01 00         
                              800e30c8 6e ff          
                              800e30ca bf ff           
                              800e30cc 92 00           
                              800e30ce bf ff          
                              800e30d0 00 00          
                              800e30d2 47 00          
                              800e30d4 bf ff           
                              800e30d6 92 00           
                              800e30d8 bf ff           
                              800e30da 00 00           


//Add data that can be read by the chart where the old Medal data was

14D64E24 Digimon chart render data:

Old:
6e ff bd ff 92 00 bd ff 
00 00 6d ff be ff 93 00 
be ff 01 00 6e ff bf ff 
92 00 bf ff 00 00 47 00

New:
12 01 b0 00 20 80 00 00 //Machinedramon
a5 00 b0 00 a0 50 05 00 //Panjyamon
bd 00 b0 00 e0 10 05 00 //Gigadramon
d5 00 b0 00 c0 10 05 00 //MetalEtemon


//Add a new chart colour, so it looks silver

14D643EE Add new chart colour 1:

Old: 00 00
New: 47 7C

14D6443E Add new chart colour 2:

Old: 00 00
New: 47 7C

//Enable the use of the new colour on the big chart and enable the extra digimon

Original:

void LoadChartBig(void)
{
  puVar6 = &ColoursBigChart;
  puVar3 = new short[6]; //located in local_C which has a size of 0A
  iVar7 = 5;
  do {
    iVar7 = --iVar7;
    *puVar3 = *puVar6; //Set the short in the current value
    puVar6 = puVar6 + 2; //get the next short
    puVar3 = puVar3 + 2; // This is just moving the array to the next value (ex: puVar3[0] to puVar3[1])
  } while (0 < iVar7);

//code ignored 

 for (iVar7 = 0; iVar7 < 62; iVar7 = iVar7 + 1) 
 {
 //code to render the sprites
 }
 
 for (iVar7 = 0; iVar7 < 62; iVar7 = iVar7 + 1) 
 {
 //code to render the squares
 }

//code ignored
}

Changed:

void LoadChartBig(void)
{
  puVar6 = &ColoursBigChart;
  puVar3 = new short[6]; 
  iVar7 = 6;
  do {
    iVar7 = --iVar7;
    *puVar3 = *puVar6; 
    puVar6 = puVar6 + 1;
    puVar3 = puVar3 + 1;
  } while (0 < iVar7);

//code ignored 

 for (iVar7 = 0; iVar7 < 65; iVar7 = iVar7 + 1) 
 {
 //code to render the sprites
 }
 
 for (iVar7 = 0; iVar7 < 65; iVar7 = iVar7 + 1) 
 {
 //code to render the squares
 }

//code ignored
}

Disassembly:

                        LoadChartBig


                    Offset       Hex         Command  
Original:
        800bd960 05 00 19 24     li         t9,0x5  

        800bdba8 3e 00 01 2a     slti       at,s0,0x3e
                             
        800bdc34 3e 00 01 2a     slti       at,s0,0x3e



Changed:
        800bd960 06 00 19 24     li         t9,0x6

        800bdba8 41 00 01 2a     slti       at,s0,0x41
                             
        800bdc34 41 00 01 2a     slti       at,s0,0x41


//function with code deleted to get enough commands for the next section

Original:

int * LoadDigimonModelExtra(int DigimonValue,int param_2,uchar **param_3)

{  
  if ((param_1 < 0) || (0xb3 < param_1)) 
    unaff_s0 = (int *)0x0;
  
  else if (param_2 == 0) 
  {
     //code ignored
  }
  else
  {
    
    //code ignored
  }
  return unaff_s0;
}

Changed:

int * LoadDigimonModelExtra(int DigimonValue,int param_2,uchar **param_3)
{
  
  if (((DigimonValue < 0) || (0xb3 < DigimonValue)) || (param_2 == 0))  //now (param_2 == 0) just returns 0, that section of he code was never used anyway
    unaff_s0 = 0;  
  else 
  {
    //code ignored
  }
  return unaff_s0;
}


Disassembly:
                        
                    LoadDigimonModelExtra

         Offset       Hex         Command 

Original:
        800a2a64 21 88 80 00     move       s1,a0
        800a2a68 04 00 20 06     bltz       s1,0x800a2a7c
        800a2a6c 21 90 c0 00     _move      s2,a2
        800a2a70 b4 00 21 2a     slti       at,s1,0xb4
        800a2a74 03 00 20 14     bne        at,zero,0x800a2a84
        800a2a78 00 00 00 00     _nop
                             LAB_800a2a7c   
        800a2a7c 18 01 00 10     b          0x800a2ee0
        800a2a80 21 10 00 00     _clear     v0


Changed:
        800a2a64 21 88 80 00     move       s1,a0
        800a2a68 06 00 20 06     bltz       s1,0x800a2a84
        800a2a6c 21 90 c0 00     _move      s2,a2
        800a2a70 b4 00 21 2a     slti       at,s1,0xb4
        800a2a74 03 00 20 10     beq        at,zero,0x800a2a84
        800a2a78 00 00 00 00     _nop
        800a2a7c 8d 00 a0 14     bne        a1,zero,0x800a2cb4
        800a2a80 00 00 00 00     _nop
                             LAB_800a2a84           
        800a2a84 16 01 00 10     b          0x800a2ee0
        800a2a88 21 10 00 00     _clear     v0



//Enable the use of the new colour in the small chart and add an exception for each extra digimon

LoadChartSmall()
{
  puVar12 = &ColoursSmallChart;
  puVar7 = new short[6]; //located in local_C which has a size of 0A
  iVar13 = 5;
  do {
    iVar13 = iVar13 + -1;
    *puVar7 = *puVar12; //Set the short in the current value
    puVar12 = puVar12 + 1; //get the next short
    puVar7 = puVar7 + 1; // This is just moving the array to the next value (ex: puVar7[0] to puVar7[1])
  } while (0 < iVar13);
  
  iVar14 = DigimonValue - 1;
  uVar5 = 0;
  uVar7 = 0;
  
  for (iVar8 = 0; iVar8 < 5; iVar8 = iVar8 + 1)
  {
     pbVar4 = &PreEvolutionTargetData + iVar14 * 11;
 
    if (pbVar4[iVar8] != -1) 
      uVar7 = uVar7 + 1; //this is used to know if the number of preevolutions is odd or not
    
  }
  for (iVar9 = 0; iVar9 < 6; iVar9 = iVar9 + 1)
  {
 
    if ((&DataForTargetDigimon)[iVar9 + iVar14 * 11] != -1) {
      uVar5 = uVar5 + 1; //this is used to know if the number of evolutions is odd or not
    }
  }

//code ignored

//render pre evolution lines
    iVar9 = DigimonValue;
    pbVar2 = &PreEvolutionTargetData + (iVar9 - 1) * 11; 

//code ignored

//render evolution lines
    iVar4 = DigimonValue;

//code ignored

//render pre evolution sprites

    iVar4 = DigimonValue;
    pbVar2 = &PreEvolutionTargetData + (iVar4 - 1) * 11; 

//code ignored     

//render evolution sprites

    iVar4 = DigimonValue;

//code ignored

}

LoadChartSmall()
{
  puVar12 = &ColoursSmallChart;
  puVar7 = new short[6];
  iVar13 = 6;
  do {
    iVar13 = iVar13 + -1;
    *puVar7 = *puVar12;
    puVar12 = puVar12 + 1;
    puVar7 = puVar7 + 1;
  } while (0 < iVar13);
  
  iVar14 = DigimonValue - 1;
  uVar5 = 0;
  uVar7 = 0;
  
  for (iVar8 = 0; iVar8 < 5; iVar8 = iVar8 + 1)
  {
    if (iVar14 < 63) //check if bigger than Machinedramon
     pbVar4 = &PreEvolutionTargetData + iVar14 * 11;
    
    else 
      pbVar4 = &PanjyamonPreEvoTargets + (DigimonValue - 63) * 5; //Read the data in a new location
    
    if (pbVar4[iVar8] != -1) 
      uVar7 = uVar7 + 1;   
    
  }
  iVar8 = iVar14 * 11;
  for (iVar9 = 0; iVar9 < 6; iVar9 = iVar9 + 1)
  {
    if (61 < iVar14) //check if bigger than MegaSeadramon
	{
      if (iVar14 != 63) break;  //Stop counting if not Panyjamon
      iVar8 = iVar14 * 11 - 11; //make it use Machinedramon data
    }
    if ((&DataForTargetDigimon)[iVar9 + iVar8] != -1) {
      uVar5 = uVar5 + 1; 
    }
  }

//code ignored

//render pre evolution lines
    iVar9 = DigimonValue;
    if (iVar9 < 63) 
      pbVar2 = &PreEvolutionTargetData + (iVar9 - 1) * 11; //Use the normal data until machinedramon
    
    else 
      pbVar2 = &PanjyamonPreEvoTargets + (iVar9 - 63) * 5; //Read the data in a new location

//code ignored

//render evolution lines

    iVar4 = DigimonValue;
    if (61 < iVar4) //If the value is Machinedramon or bigger
   {
      if (iVar4 != 63) break; //If the value is Panjyamon
      iVar4 = 62;
    }

//code ignored

//render pre evolution sprites

    iVar4 = DigimonValue;
    if (iVar4 < 63) 
      pbVar2 = &PreEvolutionTargetData + (iVar4 - 1) * 11; //Use the normal data until machinedramon
    
    else 
      pbVar2 = &PanjyamonPreEvoTargets + (iVar4 - 63) * 5; //Read the data in a new location
     
//code ignored

//render evolution sprites

    iVar4 = DigimonValue;
    if (61 < iVar4) //If the value is Machinedramon or bigger
    {
      if (iVar4 != 63) break; //If the value is Panjyamon
      iVar4 = 62;
    }
//code ignored
}


Disassembly:

                      LoadChartSmall
         Offset       Hex         Command 

Original:

        800ba968 05 00 19 24     li         t9,0x5
		
		                          
        800ba9d0 13 80 03 3c     lui        v1,0x8013
        800ba9d4 6c b6 63 24     addiu      v1,v1,-0x4994

		
		800baa1c 0d 00 00 10     b          0x800baa54
        800baa20 20 20 66 00     _add       a0,v1,a2                                                              
        800baa24 13 80 03 3c     lui        v1,0x8013
        800baa28 71 b6 63 24     addiu      v1,v1,-0x498f
		
		
        800baa58 f2 ff 20 14     bne        at,zero,0x800baa24


        800bab0c 00 00 00 00     nop
        800bab0c ff ff 43 20     addi       v1,v0,-0x1
        800bab10 80 10 03 00     sll        v0,v1,0x2
		
		
        800bacd8 00 00 00 00     nop
        800bacdc ff ff 43 20     addi       v1,v0,-0x1		
        800bace0 80 10 03 00     sll        v0,v1,0x2
		
		
        800bafe0 00 00 00 00     nop
        800bafe4 ff ff 43 20     addi       v1,v0,-0x1
        800bafe8 80 10 03 00     sll        v0,v1,0x2
		
		
        800bb124 00 00 00 00     nop
        800bb128 ff ff 43 20     addi       v1,v0,-0x1
        800bb12c 80 10 03 00     sll        v0,v1,0x2


Changed:
 
        800ba968 06 00 19 24     li         t9,0x6
		
		
        800ba9d0 9f a0 00 10     b          LAB_800a2c50
        800ba9d4 00 00 00 00     _nop
	
//jumps here
                             LAB_800a2c50                                   
        800a2c50 3f 00 c1 28     slti       at,a2,0x3f
        800a2c54 08 00 01 14     bne        zero,at,0x800a2c78
        800a2c58 00 00 00 00     _nop
        800a2c5c 0e 80 03 3c     lui        v1,0x800e
        800a2c60 a4 30 63 24     addiu      v1,v1,0x30a4
        800a2c64 c2 ff c1 24     addiu      at,a2,-0x3e
        800a2c68 21 18 23 00     addu       v1,at,v1
        800a2c6c 80 08 01 00     sll        at,at,0x2
        800a2c70 5a 5f 00 10     b          0x800ba9dc
        800a2c74 21 18 61 00     _addu      v1,v1,at
                             LAB_800a2c78                                   
        800a2c78 13 80 03 3c     lui        v1,0x8013
        800a2c7c 56 5f 00 10     b          0x800ba9d8
        800a2c80 6c b6 63 24     _addiu     v1,v1,-0x4994
		
		
		
		800baa58 f2 ff 20 14     bne        at,zero,0x800baa24
		
//jumps here
                             LAB_800a2c84                                
        800a2c84 3d 00 c1 28     slti       at,a2,0x3d
        800a2c88 07 00 01 14     bne        zero,at,0x800a2ca8
        800a2c8c 3e 00 01 24     _li        at,0x3e
        800a2c90 21 20 14 00     move       a0,s4
        800a2c94 03 00 26 10     beq        at,a2,0x800a2ca4
        800a2c98 00 00 00 00     _nop
        800a2c9c 70 5f 00 10     b          0x800baa60
        800a2ca0 ff ff 03 24     _li        v1,-0x1
                             LAB_800a2ca4                                   
        800a2ca4 f5 ff 84 24     addiu      a0,a0,-0xb
                             LAB_800a2ca8                                  
        800a2ca8 13 80 03 3c     lui        v1,0x8013
        800a2cac 5f 5f 00 10     b          0x800baa2c
        800a2cb0 71 b6 63 24     _addiu     v1,v1,-0x498f
		
	
		

        800bab08 21 20 00 00     clear      a0
        800bab0c 3f a0 00 10     b          0x800a2c0c
        800bab10 00 00 00 00     _nop

//jumps here
                             LAB_800a2c0c                        
        800a2c0c 3f 00 41 28     slti       at,v0,0x3f
        800a2c10 0a 00 01 14     bne        zero,at,0x800a2c3c
        800a2c14 00 00 00 00     _nop
        800a2c18 0e 80 03 3c     lui        v1,0x800e
        800a2c1c a4 30 63 24     addiu      v1,v1,0x30a4 //PanjyamonPreEvoTargets
        800a2c20 c1 ff 42 24     addiu      v0,v0,-0x3f
        800a2c24 80 08 02 00     sll        at,v0,0x2
        800a2c28 21 10 22 00     addu       v0,at,v0
        800a2c2c bf 5f 04 10     beq        zero,a0,0x800bab2c
        800a2c30 21 10 62 00     _addu      v0,v1,v0
        800a2c34 f3 60 00 10     b          0x00bb004
        800a2c38 00 00 00 00     _nop
                             LAB_800a2c3c                                  
        800a2c3c ff ff 43 20     addi       v1,v0,-0x1
        800a2c40 b4 5f 04 10     beq        zero,a0,0x800bab14
        800a2c44 80 10 03 00     _sll       v0,v1,0x2
        800a2c48 e8 60 00 10     b          0x800bafec
        800a2c4c 00 00 00 00     _nop


      
	  
	    800bacd8 21 20 00 00     clear      a0
        800bacdc bc 9f 00 10     b          0x800a2bd0
        800bace0 00 00 00 00     _nop

//jumps here
                             LAB_800a2bd0                              
        800a2bd0 3e 00 41 28     slti       at,v0,0x3e
        800a2bd4 07 00 01 14     bne        zero,at,0x800a2bf4
        800a2bd8 3f 00 01 24     _li        at,0x3f
        800a2bdc 05 00 22 10     beq        at,v0,0x800a2bf4
        800a2be0 3e 00 02 24     _li        v0,0x3e
        800a2be4 cd 60 04 10     beq        zero,a0,0x800baf1c
        800a2be8 00 00 00 00     _nop
        800a2bec 99 61 00 10     b          0x800bb254
        800a2bf0 00 00 00 00     _nop
                             LAB_800a2bf4                                 
        800a2bf4 ff ff 43 20     addi       v1,v0,-0x1
        800a2bf8 80 10 03 00     sll        v0,v1,0x2
        800a2bfc 39 60 04 10     beq        zero,a0,0x800bace4
        800a2c00 00 00 00 00     _nop
        800a2c04 4a 61 00 10     b          0x800bb130
        800a2c08 00 00 00 00     _nop



        800bafe0 01 00 04 24     li         a0,0x1
        800bafe4 09 9f 00 10     b          0x800a2c0c //check LAB_800a2c0c
        800bafe8 00 00 00 00     _nop



        800bb124 01 00 04 24     li         a0,0x1
        800bb128 a9 9e 00 10     b          0x800a2bd0 //check LAB_800a2bd0
        800bb12c 00 00 00 00     _nop

                          

//Enable the selection of the extra digimon in the chart

Original:

void HandleChartInput(void)
{
//code ignored    
          else if (((3 < HorizontalPos) && (HorizontalPos < 7)) || (VerticalPos = 7, HorizontalPos == 8))  //move upwards if there's no square        
            VerticalPosCorrection = 6;

//code ignored

        iVar6 = 0;
        for (iVar7 = 0;(iVar6 < 62 &&((HorizontalPos + 2 != *(&HorizontalPosDigimonCharList + iVar7) ||
             (VerticalPos * 19 + 43 != *(&VerticalPosDigimonCharList + iVar7)))));
            iVar7 = iVar7 + 8) //check which digimon you are currently at based on its position in the chart 
          iVar6 = iVar6 + 1;  
       
        CurrentDigimonSquare = iVar6 + 1;
        
//code ignored
}


Changed:

void HandleChartInput(void)
{
//code ignored    
          else if (((6 < HorizontalPos) && (HorizontalPos < 7)) || (VerticalPos = 7, HorizontalPos == 9))  //No restrictions anymore here       
            VerticalPosCorrection = 6;

//code ignored

        iVar6 = 0;
        for (iVar7 = 0;(iVar6 < 65 &&((HorizontalPos + 2 != *(&HorizontalPosDigimonCharList + iVar7) ||
             (VerticalPos * 19 + 43 != *(&VerticalPosDigimonCharList + iVar7)))));
            iVar7 = iVar7 + 8) //now it counts up to 64
          iVar6 = iVar6 + 1;

        CurrentDigimonSquare = iVar6 + 1;
        
//code ignored
}


Disassembly:

                      HandleChartInput

         Offset       Hex         Command 

Original:
                                   
        800b97b8 04 00 a1 28     slti       at,a1,0x4

        800b97d0 08 00 01 24     li         at,0x8

        800b9920 3e 00 81 28     slti       at,a0,0x3e

Changed:
                                   
        800b97b8 07 00 a1 28     slti       at,a1,0x7

        800b97d0 09 00 01 24     li         at,0x9

        800b9920 41 00 81 28     slti       at,a0,0x41





//Factorial town changes
//This is a function that compares data in the script, only the part with the change is shown

Original:
uint FUN_801066bc(uint param_1)
{
switch(param_1 & 7)
 {
// code ignored
  case 1:    
    in_v0 = (in_a1 != in_a2);
    break;
// code ignored
 }
}


Changed:

uint FUN_801066bc(uint param_1)
{
switch(param_1 & 7)
 {
// code ignored
  case 1:
    if (((in_a2 == 13) && (*(short *)(_ScriptPointer + -2) == 103)) &&  //this is to check if the script is asking for mamemon (13) and the script is the factorial town evolution check
       (((in_a1 == 5 || (((in_a1 == 10 || (in_a1 == 18)) || (in_a1 == 6)))) ||  //Maeson has a 8 rather than a 18 and a 20 rather than a 6 
        (((in_a1 == 42 || (in_a1 == 48)) || (in_a1 == 53))))))    //The digimons are: Greymon, Seadramon, Elecmon (Tyrannomon in Maeson), Devimon (Angemon in Maeson), Etemon, Leomon and Nanimon
      in_a1 = 0xd;    //Overwritte this to match the script
    
    in_v0 = (in_a1 != in_a2);
    break;
// code ignored
 }
}

//Second change necessary for factorial town, this is just code excecuted based on the script

Original:

void HandleScript/SukamonReturn(int param_1)
{
//code ignored
  case 10:
    iVar1 = ReadPStat(254);  //Gets the target evolution  
    _DigimonEvoValue = iVar1;
    SetDigimonState(13);
    _DAT_ffff9304 = 0;
  }
//code ignored
}

Changed:

void HandleScript/SukamonReturn(int param_1)
{
//code ignored
  case 10:
    iVar1 = ReadPStat(254);  //Gets the target evolution
    if (iVar1 == 41)  //If the evolution was supposed to be Giromon
    {
      if (DigimonType == 5) //Greymon
        iVar1 = 62;  //Machinedramon
      
      if (DigimonType == 10)  //Seadramon
        iVar1 = 62;  //Machinedramon, 61 with Maeson which is MegaSeadramon
      
      if (DigimonType == 18)  //Elecmon, 8 with Maeson which is Tyrannomon
        iVar1 = 63;  //Panyjamon, 62 with Maeson which is Machinedramon
      
      if (DigimonType == 6) //Devimon, 20 with Maeson which is Angemon
        iVar1 = 28;  //Vademon
      
      if (DigimonType == 42) //Etemon
        iVar1 = 40; //Andromon
      
      if (DigimonType == 48) //Leomon
        iVar1 = 40; //Andromon
      
    }
    else if (iVar1 == 27) //If the evolution was supposed to be MetalMamemon
    {
      if (DigimonType == 5) //Greymon
        iVar1 = 12;  //MetalGreymon
      
      if (DigimonType == 10)  //Seadramon
        iVar1 = 64;  //Gigadramon
      
      if (DigimonType == 18) //Elecmon, 8 with Maeson which is Tyrannomon
        iVar1 = 33; //Unimon, 13 with Maeson which is Mamemon
      
      if (DigimonType == 0x14) //Devimon, 20 with Maeson which is Angemon
        iVar1 = 40; //Andromon
      
      if (DigimonType == 0x2a)  //Etemon
        iVar1 = 65; //MetalEtemon
      
      if (DigimonType == 0x30) //Leomon
        iVar1 = 63; //Panyjamon
      
    }
    _DigimonEvoValue = iVar1;
    SetDigimonState(13);
    _DAT_ffff9304 = 0;
  }
//code ignored
}


Disassembly:


                            FUN_801066bc

         Offset       Hex         Command 

Original:

                             case 1                     
        801066f8 26 10 a6 00     xor        v0,a1,a2
        801066fc 0a 00 00 10     b          0x80106728
        80106700 2b 10 02 00     _sltu      v0,zero,v0


Changed:

                             case 1                      
        801066f8 3d 8c 03 08     j          0x800e30f4
        801066fc 00 00 00 00     _nop
        80106700 00 00 00 00     nop

//jumps here
                             LAB_800e30f4                                   
        800e30f4 0d 00 01 24     li         at,0xd
        800e30f8 16 00 26 14     bne        at,a2,0x800e3154
        800e30fc 00 00 00 00     _nop
        800e3100 b0 94 82 8f     lw         v0,-0x6b50(gp) //ScriptPointer 
        800e3104 00 00 00 00     nop
        800e3108 fe ff 42 80     lb         v0,-0x2(v0)  // ScriptPointer - 2 value
        800e310c 67 00 01 24     li         at,0x67
        800e3110 10 00 41 14     bne        v0,at,0x800e3154
        800e3114 05 00 01 24     _li        at,0x5
        800e3118 0d 00 25 10     beq        at,a1,0x800e3150
        800e311c 0a 00 01 24     _li        at,0xa
        800e3120 0b 00 25 10     beq        at,a1,0x800e3150

//Vice
        800e3124 12 00 01 24     _li        at,0x12
//Maeson
        800e3124 08 00 01 24     _li        at,0x8

        800e3128 09 00 25 10     beq        at,a1,0x800e3150

//Vice
        800e312c 06 00 01 24     _li        at,0x6
//Maeson
        800e312c 14 00 01 24     _li        at,0x14

        800e3130 07 00 25 10     beq        at,a1,0x800e3150
        800e3134 2a 00 01 24     _li        at,0x2a
        800e3138 05 00 25 10     beq        at,a1,0x800e3150
        800e313c 30 00 01 24     _li        at,0x30
        800e3140 03 00 25 10     beq        at,a1,0x800e3150
        800e3144 35 00 01 24     _li        at,0x35
        800e3148 02 00 25 14     bne        at,a1,0x800e3154
        800e314c 00 00 00 00     _nop
                             LAB_800e3150                                   
        800e3150 0d 00 05 24     li         a1,0xd
                             LAB_800e3154                                  
        800e3154 26 10 a6 00     xor        v0,a1,a2
        800e3158 ca 19 04 08     j          0x80106728
        800e315c 2b 10 02 00     _sltu      v0,zero,v0





                 HandleScript/SukamonReturn

         Offset       Hex         Command 


Original:

        800df7d8 24 93 82 a7     sh         v0,-0x6cdc(gp) //DigimonEvoValue
        800df7dc 34 7d 03 0c     jal        0x800df4d0   //SetDigimonState                            
        800df7e0 0d 00 04 24     _li        a0,0xd


Changed:                     

        800df7d8 00 00 00 00     nop
        800df7dc 60 0e 00 10     b          0x800e3160
        800df7e0 00 00 00 00     _nop

//jumps here
                             LAB_800e3160                                   
        800e3160 15 80 01 3c     lui        at,0x8015
        800e3164 a8 57 24 8c     lw         a0,0x57a8(at)
        800e3168 00 00 00 00     nop
        800e316c 29 00 01 24     li         at,0x29
        800e3170 16 00 22 14     bne        at,v0,0x800e31cc
        800e3174 00 00 00 00     _nop
        800e3178 05 00 01 24     li         at,0x5
        800e317c 02 00 24 14     bne        at,a0,0x800e3188
        800e3180 0a 00 01 24     _li        at,0xa
        800e3184 36 00 02 24     li         v0,0x3e
                             LAB_800e3188                                  
        800e3188 02 00 24 14     bne        at,a0,0x800e3194

//Vice
        800e318c 12 00 01 24     _li        at,0x12
//Maeson
        800e318c 08 00 01 24     _li        at,0x8

//Vice
        800e3190 3e 00 02 24     li         v0,0x3e
//Maeson
        800e3190 3d 00 02 24     li         v0,0x3d

                             LAB_800e3194                                  
        800e3194 02 00 24 14     bne        at,a0,0x800e31a0

//Vice
        800e3198 06 00 01 24     _li        at,0x6
//Maeson
        800e3198 14 00 01 24     _li        at,0x14

//Vice
        800e319c 3f 00 02 24     li         v0,0x3f
//Maeson
        800e319c 3e 00 02 24     li         v0,0x3e

                             LAB_800e31a0                                   
        800e31a0 02 00 24 14     bne        at,a0,0x800e31ac
        800e31a4 2a 00 01 24     _li        at,0x2a
        800e31a8 1c 00 02 24     li         v0,0x1c
                             LAB_800e31ac                                   
        800e31ac 02 00 24 14     bne        at,a0,0x800e31b8
        800e31b0 30 00 01 24     _li        at,0x30
        800e31b4 28 00 02 24     li         v0,0x28
                             LAB_800e31b8                                   
        800e31b8 19 00 24 14     bne        at,a0,0x800e3220
        800e31bc 00 00 00 00     _nop
        800e31c0 28 00 02 24     li         v0,0x28
        800e31c4 16 00 00 10     b          0x800e3220
        800e31c8 00 00 00 00     _nop
                             LAB_800e31cc                                  
        800e31cc 1b 00 01 24     li         at,0x1b
        800e31d0 13 00 41 14     bne        v0,at,0x800e3220
        800e31d4 05 00 01 24     _li        at,0x5
        800e31d8 02 00 24 14     bne        at,a0,0x800e31e4
        800e31dc 0a 00 01 24     _li        at,0xa
        800e31e0 0c 00 02 24     li         v0,0xc
                             LAB_800e31e4                                 
        800e31e4 02 00 24 14     bne        at,a0,0x800e31f0

//Vice
        800e31e8 12 00 01 24     _li        at,0x12
//Maeson
        800e31e8 08 00 01 24     _li        at,0x8

        800e31ec 40 00 02 24     li         v0,0x40
                             LAB_800e31f0                                 
        800e31f0 02 00 24 14     bne        at,a0,0x800e31fc

//Vice
        800e31f4 06 00 01 24     _li        at,0x6
//Maeson
        800e31f4 14 00 01 24     _li        at,0x14

//Vice
        800e31f8 21 00 02 24     li         v0,0x21
//Maeson
        800e31f8 0d 00 02 24     li         v0,0xd

                             LAB_800e31fc                                 
        800e31fc 02 00 24 14     bne        at,a0,0x800e3208
        800e3200 2a 00 01 24     _li        at,0x2a
        800e3204 28 00 02 24     li         v0,0x28
                             LAB_800e3208                                  
        800e3208 02 00 24 14     bne        at,a0,0x800e3214
        800e320c 30 00 01 24     _li        at,0x30
        800e3210 41 00 02 24     li         v0,0x41
                             LAB_800e3214                                 
        800e3214 02 00 24 14     bne        at,a0,0x800e3220
        800e3218 00 00 00 00     _nop
        800e321c 3f 00 02 24     li         v0,0x3f
                             LAB_800e3220                                 
        800e3220 24 93 82 a7     sh         v0,-0x6cdc(gp) //DigimonEvoValue
        800e3224 34 7d 03 0c     jal        0x800df4d0  //SetDigimonState                                 
        800e3228 0d 00 04 24     _li        a0,0xd
        800e322c 6d f1 00 10     b          0x800df7e4
        800e3230 00 00 00 00     _nop



//HP/MP tournament recovery
//This is a function that I guess is to advance the tournament

Original:

int FUN_80060620(int param_1,int param_2)
{
//code ignored
  CurrentHP = CurrentHP + MaxHP / 5

  CurrentMP = CurrentMP + MaxMP / 5

//code ignored
}

Changed:

int FUN_80060620(int param_1,int param_2)
{
//code ignored
  CurrentHP = CurrentHP + (MaxHP / 5) * 4 //Maeson multiplies it by 2 rather than 4

  CurrentMP = CurrentMP + (MaxMP / 5) * 4 //Maeson multiplies it by 2 rather than 4

//code ignored
}


Disassembly:

                         FUN_80060620

         Offset       Hex         Command 
Original:

//HP
        800608e8 00 24 04 00     sll        a0,a0,0x10
        800608ec 14 00 43 84     lh         v1,0x14(v0)
        800608f0 03 24 04 00     sra        a0,a0,0x10

//MP
        80060918 00 24 04 00     sll        a0,a0,0x10
        8006091c 16 00 43 84     lh         v1,0x16(v0)
        80060920 03 24 04 00     sra        a0,a0,0x10

Vice:

//HP
        800608e8 C0 24 04 00     sll        a0,a0,0x3
        800608ec 14 00 43 84     lh         v1,0x14(v0)
        800608f0 43 24 04 00     sra        a0,a0,0x1

//MP
        80060918 C0 24 04 00     sll        a0,a0,0x3
        8006091c 16 00 43 84     lh         v1,0x16(v0)
        80060920 43 24 04 00     sra        a0,a0,0x1

Maeson:

//HP
        800608e8 C0 24 04 00     sll        a0,a0,0x3
        800608ec 14 00 43 84     lh         v1,0x14(v0)
        800608f0 83 24 04 00     sra        a0,a0,0x2

//MP
        80060918 C0 24 04 00     sll        a0,a0,0x3
        8006091c 16 00 43 84     lh         v1,0x16(v0)
        80060920 83 24 04 00     sra        a0,a0,0x2


//Tournament list expansion

Original:

CheckIfCanEnterTournament(int TournamentID)
{
  iVar2 = FUN_800e6f50(10,tournamentID); //get the script pointer
  iVar2 = FUN_800e6f9c(iVar2,3); //get the script address
  pbVar3 = iVar2 + 2;    //script address + 2

  do {
    currentDigimonType = *listAddress; //set the digimon type based in the address
    listAddress = listAddress + 1; //set the address for the next digimon

    if (253 < currentDigimonType) //the lists use 254 as a terminator
      return 0;
    
  } while (currentDigimonType != DigimonType);
  return 1;
}


Changed:

CheckIfCanEnterTournament(int TournamentID)
{
  iVar2 = FUN_800e6f50(10,tournamentID); 
  iVar2 = FUN_800e6f9c(iVar2,3); 
  listAddress = iVar2 + 1;    //script address + 1, it can read 1 address more

  do {
    currentDigimonType = *listAddress; 
    listAddress = listAddress + 1;
 
    if (106 < currentDigimonType)  //All the lists start with 107, so it makes sure it doesn't read the next list, this enables the code to read an extra address
      return 0;
    
  } while (currentDigimonType != DigimonType);
  return 1;
}


Disassembly:
                   CheckIfCanEnterTournament

         Offset       Hex         Command 

Original:

        8008123c 02 00 44 24     addiu      a0,v0,0x2

        8008126c fe 00 41 2c     sltiu      at,v0,0xfe


Changed:

        8008123c 01 00 44 24     addiu      a0,v0,0x1

        8008126c 6b 00 41 2c     sltiu      at,v0,0x6b





// Chain Melon change, exclusive to my hack

//check the repository for the original code, before 1.11.2
HandleItemRejection()
{
//code ignored
if (((69 < ItemBeingFeed) && (ItemBeingFeed < 115)) ||...  //check if this is an edible item
                           

// code ignored         

if (digimonTargetLevel == 3) //only the chain melon has this level
    digimonTargetLevel = 5;  //use an Ultimate level

// code ignored                     
}


Disassembly:

                          HandleItemRejection

         Offset       Hex         Command     
        800a7350 46 00 01 2a     slti       at,s0,0x46
		
		800a748c 03 00 01 24     li         at,0x3
        800a7490 02 00 22 14     bne        at,v0,0x800a749c
        800a7494 00 00 00 00     _nop
        800a7498 02 00 42 20     addi       v0,v0,0x2
                             LAB_800a749c                                   
        800a749c 01 00 84 24     addiu      a0,a0,0x1
        800a74a0 05 00 44 10     beq        v0,a0,0x800a74b8
        800a74a4 00 00 00 00     _nop
        800a74a8 00 00 00 00     nop
        800a74ac 00 00 00 00     nop
		
		
//Check SydMontague repository for the original code of this function

//Now the HappyMushroom has the effect of a Chain Melon, this one is only before 1.11.2
void HandleFood(uint FoodType)
{  

  else if (FoodType == 70 || FoodType == 69) //Chain Melon or HappyMushroom, it has the old Chain Melon data
  {
    lifetime = 20;
    energy = 50;
    happiiness = 50;
    tiredenessReduction = 50;
    sicknessValue = 5;
    weight = 3;
  }
}
                               HandleFood

                             LAB_800c4250                                   
        800c4250 00 00 00 00     nop 
        800c4254 00 00 00 00     nop 
        800c4258 00 00 00 00     nop 
        800c425c 00 00 00 00     nop 
        800c4260 00 00 00 00     nop 
        800c4264 00 00 00 00     nop 
        800c4268 00 00 00 00     nop 
        800c426c 00 00 00 00     nop 
        800c4270 00 00 00 00     nop 
        800c4274 00 00 00 00     nop 
        800c4278 00 00 00 00     nop 
        800c427c 00 00 00 00     nop 
        800c4280 00 00 00 00     nop 
        800c4284 00 00 00 00     nop 
        800c4288 00 00 00 00     nop       
                                                              
        800c428c 32 00 05 24     li         a1,0x32
        800c4290 21 20 a0 00     move       a0,a1
        800c4294 00 84 04 00     sll        s0,a0,0x10
        800c4298 14 00 04 24     li         a0,0x14
        800c429c 00 24 04 00     sll        a0,a0,0x10
        800c42a0 3c 00 a4 af     sw         a0,0x3c(sp)
        800c42a4 3c 00 a4 8f     lw         a0,0x3c(sp)
        800c42a8 00 14 05 00     sll        v0,a1,0x10
        800c42ac 03 24 04 00     sra        a0,a0,0x10
        800c42b0 3c 00 a4 af     sw         a0,0x3c(sp)
        800c42b4 05 00 04 24     li         a0,0x5
        800c42b8 00 9c 04 00     sll        s3,a0,0x10
        800c42bc 03 00 04 24     li         a0,0x3
        800c42c0 00 94 05 00     sll        s2,a1,0x10
        800c42c4 00 8c 04 00     sll        s1,a0,0x10
        800c42c8 03 14 02 00     sra        v0,v0,0x10
        800c42cc 03 84 10 00     sra        s0,s0,0x10
        800c42d0 03 94 12 00     sra        s2,s2,0x10
        800c42d4 03 9c 13 00     sra        s3,s3,0x10
        800c42d8 0e 00 00 10     b          0x800c4314
        800c42dc 03 8c 11 00     _sra       s1,s1,0x10


14D68CE8 Chain Melon pointer change: 6C 39 0C 80 to 60 38 0C 80 //this makes it call the evo item function rather than the food function



//AS Decoder change, 1.11.2 onwards
HandleItemRejection()
{
//code ignored
if (((... ||ItemBeingFeed < 124)  //check if this is an edible item
    (...||(124 < (int)ItemBeingFeed? && ((int)ItemBeingFeed? < 128)) //check if this is an evo item
                           

// code ignored         

if (ItemBeingFeed == 124) //only the chain melon has this level
    digimonTargetLevel = 5;  //use an Ultimate level

// code ignored                     
}


Disassembly:

                          HandleItemRejection

         Offset       Hex         Command     
        800a731c 7c 00 01 2a     slti       at,s0,0x7c
		                               
        800a7368 7c 00 01 2a     slti       at,s0,0x7c


		
		800a748c 7c 00 01 24     li         at,0x7c
        800a7490 02 00 30 14     bne        at,s0,0x800a749c
        800a7494 00 00 00 00     _nop
        800a7498 05 00 02 24     li         v0,0x5
                             LAB_800a749c                                   
        800a749c 01 00 84 24     addiu      a0,a0,0x1
        800a74a0 05 00 44 10     beq        v0,a0,0x800a74b8
        800a74a4 00 00 00 00     _nop
        800a74a8 00 00 00 00     nop
        800a74ac 00 00 00 00     nop

14D68DC0 AS Decoder pointer change: 00 00 00 00 to 60 38 0C 80 //this makes it call the evo item function rather than nothing

//Improved lifetime when evolving to ultimate (Exclusive to my hack)
//Check the repository for the original code
void EvolutionCode(int entityPtr,short *StatsPtr,int DigimonDataPtr,int targetDigimon) // Used to get and set the new digimon data during the evolution
{
//code ignored

if (((&DigimonLevel)[OffsetToTargetDigimonValues] == 5) && (evoItemFlag == 0)) //Add lifetime if ultimate
    Lifetime = Lifetime + 192; //now adds 192 hours rather than 96

//code ignored
	
}

Disassembly:

                       EvolutionCode
					   
         Offset       Hex         Command   
        800637d4 C0 00 42 20     addi       v0,v0,0xC0




//Change exclusive to the Maeson hack, this was the first code change I made
//Check SydMontague repository for the original code
uint CalculateRequirementScoreEvo(int DigimonType,int TargetDigimon,bool isMaxCareMistakes,bool isMaxBattles,char bestDigimon)
{

if ((BattlesRequired != -1) && (Battles <= BattlesRequired) && (BattlesRequired - 3 <= Battles) //this transform the battles into a range
     extraFlag = 1;
}


                    CalculateRequirementScoreEvo
					   
         Offset       Hex         Command   		
				
Changed:

        800e2a78 00 00 00 00     nop
        800e2a7c 00 00 00 00     nop
        800e2a80 54 00 62 84     lh         v0,0x54(v1) //Battles                           
        800e2a84 00 00 00 00     nop
        800e2a88 2a 08 82 00     slt        at,a0,v0
        800e2a8c 09 00 20 14     bne        at,zero,0x800e2ab4
        800e2a90 00 00 00 00     _nop
        800e2a94 fd ff 87 20     addi       a3,a0,-0x3
        800e2a98 2a 08 47 00     slt        at,v0,a3                               
        800e2a9c 05 00 20 14     bne        at,zero,0x800e2ab4                          
        800e2aa0 00 00 00 00     _nop
        800e2aa4 00 00 00 00     nop
        800e2aa8 00 00 00 00     nop
        800e2aac 00 00 00 00     nop
        800e2ab0 01 00 12 24     li         s2,0x1
		
		
		
//NEW code

//This is to avoid the extra digimon (Machinedramon in this case) to turn into a Sukamon, since Machinedramon can crash the game while evolving

Original:
int HandlePoopingInFloor(uint tileX,uint tileZ)
{
//code ignored
  if (DigimonType != 39) 
    VirusBar++;
  
  if (99 < DAT_80134C58) {
    DAT_80134C58 = 0;
  }
  
  //code ignored
}

int HandlePoopingInFloor(uint tileX,uint tileZ)
{
//code ignored
  if (DigimonType != 39 && DigimonType != 62) 
    VirusBar++;
  
  if (99 < DAT_80134C58) {
    DAT_80134C58 = 0;
  }
  
  //code ignored
}


Original:

                             LAB_800a6c3c                                    
        800a6c3c 15 80 01 3c     lui        at,0x8015
        800a6c40 a8 57 22 8c     lw         v0,0x57a8(at) // DigimonType                
        800a6c44 27 00 01 24     li         at,0x27
        800a6c48 07 00 41 10     beq        v0,at,0x800a6c68
        800a6c4c 00 00 00 00     _nop
        800a6c50 14 80 01 3c     lui        at,0x8014
        800a6c54 7e 84 22 84     lh         v0,-0x7b82(at) // VirusBar                    
        800a6c58 00 00 00 00     nop
        800a6c5c 01 00 42 20     addi       v0,v0,0x1
        800a6c60 14 80 01 3c     lui        at,0x8014
        800a6c64 7e 84 22 a4     sh         v0,-0x7b82(at) // VirusBar                    
                             LAB_800a6c68                                   
        800a6c68 2c 91 82 93     lbu        v0,-0x6ed4(gp) // DAT_80134C58
        800a6c6c 00 00 00 00     nop
        800a6c70 64 00 41 2c     sltiu      at,v0,0x64
        800a6c74 02 00 20 14     bne        at,zero,0x800a6c80
        800a6c78 00 00 00 00     _nop
        800a6c7c 2c 91 80 a3     sb         zero,-0x6ed4(gp) // DAT_80134C58



Changed:

                             LAB_800a6c3c                                 
        800a6c3c 15 80 01 3c     lui        at,0x8015
        800a6c40 a8 57 22 8c     lw         v0,0x57a8(at) // DigimonType                    
        800a6c44 27 00 01 24     li         at,0x27
        800a6c48 2c 91 83 93     lbu        v1,-0x6ed4(gp) // DAT_80134C58
        800a6c4c 08 00 41 10     beq        v0,at,0x800a6c70
        800a6c50 3e 00 01 24     _li        at,0x3e
        800a6c54 06 00 41 10     beq        v0,at,0x800a6c70
        800a6c58 00 00 00 00     _nop
        800a6c5c 14 80 01 3c     lui        at,0x8014
        800a6c60 7e 84 22 84     lh         v0,-0x7b82(at) // VirusBar                       
        800a6c64 00 00 00 00     nop
        800a6c68 01 00 42 24     addiu      v0,v0,0x1
        800a6c6c 7e 84 22 a4     sh         v0,-0x7b82(at) // VirusBar                        
                             LAB_800a6c70                                   
        800a6c70 64 00 61 2c     sltiu      at,v1,0x64
        800a6c74 02 00 20 14     bne        at,zero,0x800a6c80
        800a6c78 00 00 00 00     _nop
        800a6c7c 2c 91 80 a3     sb         zero,-0x6ed4(gp) // DAT_80134C58




Script changes (Most of these may be different in the Maeson version):


//Factorial town evolution text

140545A4 & 140549A2 Metal Mamemon text
Original: 82 94 82 95 82 92 82 8E 82 85 82 84 81 40 82 89 82 8E 82 94 82 8F 0D 00 82 6C 82 85 82 94 82 81 82 8C 82 6C 82 81 82 8D 82 85 82 8D 82 8F 82 8E 81 49 0D 00 // "Wow, it turned into MetalMamemon!"
Changed:  82 97 82 81 82 93 81 40 82 95 82 90 82 87 82 92 82 81 82 84 82 85 82 84 81 40 82 97 82 89 82 94 82 88 0D 00 82 6C 82 85 82 94 82 81 82 8C 81 49 81 40 0D 00 // "Wow, it was upgraded with Metal!"

1405491C & 14054510 Giromon text
Original: 82 94 82 95 82 92 82 8E 82 85 82 84 81 40 82 89 82 8E 82 94 82 8F 81 40 82 66 82 89 82 92 82 8F 82 8D 82 8F 82 8E 81 49 0D 00 // "Wow, it turned into Giromon!" 
Changed:  82 85 82 96 82 8F 82 8C 82 96 82 85 82 84 81 40 82 81 82 8E 82 99 82 97 82 81 82 99 81 48 81 40 81 40 81 40 81 40 81 40 0D 00 // "Wow, it evolved anyway!"


//Monochromon goals

The goal is calculated with this formula: goal = (value + 1) * 256

140006D3 Goal 1: from 06 to 04 //Praise (1792 to 1280)
140007B1 Goal 2: from 0B to 08 //Join the city requirement (3072 to 2304)


//Chain Melon text, exclusive to my hack before 1.11.2

14062678 and 14061188

Original:
82 81 81 40 82 93 82 89 82 87 82 8E 81 40 82 8F 82 86 81 40 82 8D 82 99 81 40 82 87 82 92 82 81 82 94 82 89 82 94 82 95  //Jijimon: "You are trustworthy. Here's a sign of my gratitude. It grew in our Meat farm."
82 84 82 85 81 42 0D 00 82 68 82 94 81 40 82 87 82 92 82 85 82 97 81 40 82 89 82 8E 81 40 82 8F 82 95 82 92 81 40 82 6C  
82 85 82 81 82 94 81 40 82 86 82 81 82 92 82 8D 81 42 0D 00 00 00 1B FD 1A 00 82 76 82 8F 82 81 82 88 81 49 81 40 82 60  //Player: "Woah! A Chained Melon?"
81 40 01 06 82 62 82 88 82 81 82 89 82 8E 82 85 82 84 0D 00 82 6C 82 85 82 8C 82 8F 82 8E 01 01 81 48 0D 00 00 00
Changed:
82 93 82 8F 82 8D 82 85 82 94 82 88 82 89 82 8E 82 87 81 40 82 68 81 40 82 86 82 8F 82 95 82 8E 82 84 81 40 82 81 82 86  //Jijimon: "You are trustworthy. Here's something I found after investigating that place."
82 94 82 85 82 92 0D 00 82 89 82 8E 82 96 82 85 82 93 82 94 82 89 82 87 82 81 82 94 82 89 82 8E 82 87 81 40 82 94 82 88 
82 81 82 94 81 40 82 90 82 8C 82 81 82 83 82 85 81 42 0D 00 00 00 1B FD 1A 00 82 76 82 8F 82 81 82 88 81 49 81 40 82 60  //Player: "Woah! A Tainted Melon?"
81 40 01 06 82 73 82 81 82 89 82 8E 82 94 82 85 82 84 0D 00 82 6C 82 85 82 8C 82 8F 82 8E 01 01 81 48 0D 00 00 00


//tournament changes

Tournament A and B now accept Machinedramon, Panjamon, Gigadramon and MetalEtemon
140A4BF8 Tournament B pointer: BC 2B to 44 2B //to read tournament D
140A4CFA Tournament A pointer: BC 2B to 44 2B //to read tournament D


140A6846 Tournament D now accepts Machinedramon: 42 to 3E (yes, it was able to accept Myotismon)
140A6882 Tournament C now accepts Machinedramon: FE to 3E
140A6906 Cup Version 0 now accepts Machinedramon: FE to 3E
140A6909 Fire cup accepts Phoenixmon: 00 to 3B
140A6911 Fire cup accepts Gigadramon: FE to 40
140A6922 Grapple cup accepts Andromon: FE to 28
140A692E Thunder cup accepts MetalEtemon: FE to 41
140A6948 Cool cup accepts Panyjamon: FE to 3F
140A6952 Mech cup accepts Gigadramon and Machinedramon: FE FF to 40 3E
140A6955 Filth cup accepts Etemon: 00 to 2A
140A6967 Dino cup rejects MetalEtemon and now accepts SkullGreymon, Gigadramon and Machinedramon: 41 FE FF to 1A 40 3E
140A696B Wing cup now accepts Gigadramon: 00 to 40
140A6988 Animal cup now accepts MetalEtemon: FE to 41
140A6996 Humaniod cup now accepts Panyjamon : FE to 3F


//Digimon spawn changes (thanks to the randomizer by meekrhino for having these, which saved me time searching for them)

13FD678F & 140B790F Mamemon spawn rate: 02 to 0A //2% to 10%

13FD64DB, 13FDD389, 13FE0121 & 140B765B Piximon spawn rate: 02 to 0A //2% to 10%

13FD831F & 140B949F MetalMamemon spawn rate: 02 to 0A //2% to 10%


//Item changes

14D68BB0 Electro Ring digimon value : 00 to 27   //Maeson has 0B here
14D68BCF Moon Mirror digimon value: 3E to 0B    //Maeson has 27 here

The "Electro Ring" was renamed to "King Crown", the description for this item changed
The "Moon Mirror" was renamed to "Ugly Mirror", the description for this item changed

//before 1.11.2
The "Chain Melon" was renamed to "Tainted Melon", the description for this item changed

//1.11.2 onwards
The "AS Decoder" was renamed to "Machi Coder", the description for this item changed

A few other items were renamed and/or the description was fixed/improved


//Digimon Changes

Megaseadramon: Added an extra move and the fire type
14D6F8CF Digimon data: 00 to 01  //this lets it use Prominence Beam
14D6F8C0 Third type: FF to 00 //no type to Fire type

10FAF1C Prominence Beam list
Original: 07 00 00 00 01 00 02 00 03 00  //A copy of Airdramon that is not used
Changed:  3D 00 03 00 32 00 00 00 03 00  //MegaSeadramon new data

Gabumon: changed an evolution
14D6CEBD evo target 5: 22 to 3F //Ogremon to Panyjamon 

Meramon: changed an evolution
14D6CE64 evo target 2: 28 to 41 //Andromon to MetalEtemon

Birdramon: added an evolution
14D6CEE8 evo target 2: FF to 40 //no evolution to Gigadramon

Weregarurumon: removed preevolutions, changed a preevolution and added 3 evolutions
14D6D1D3 Weregarurumon evo target data
Original: 30 16 17 34 FF FF FF FF FF FF FF //old Weregarurumon pre evolutions
Changed:  FF FF 3F FF FF FF 40 41 3E FF FF //new data to render the chart and to evolve Panyjamon

Geremon: changed the item dropped and chance
14D6FF39 item dropped and chance: 20 0A to 72 05 // 10% of a Port. potty to a 5% of the "Ugly Mirror" 

PlatinumSukamon: changed the item dropped and chance
14D6FF39 item dropped and chance: 0E 0A to 53 05 // 10% of a Medicine to a 5% of the "King Crown" 

Devimon NPC: changed the item dropped
14D70925 item drop: 1A to 7E //Quick chip to Noble Mane

Megadramon NPC: changed the item dropped
14D70925 item drop: 17 to 7D //Off chip to Giga Hand

Etemon NPC: changed the item dropped
14D70925 item drop: 19 to 7F //Brain chip to Metal Banana

ToyAgumon: now uses the value of Agumon for the movement lists
14D6BF5C ToyAgumon value in move list: FF FF to 03 00 //from its own value to 68 which is Agumon value

Soulmon: now uses the value of Bakemon for the movement lists
14D6BF8A Soulmon value in move list: FF FF to 25 00 //from its own value to 68 which is Bakemon value


//As a note, all of the NPC copies use the exact same value as the original digimon, so them appearing in the list is just a waste of bytes

Panjamon has changed: 
    *Gained the Ice type
    *Now it is Ice/Battle/Wind in that order
    *Sonic Jab with Giga Freeze
    *Counter with Thunder Justice
    *Tremar with Ice Statue
    *War cry with Aqua Magic
    *Muscle charge with Aurora Freeze
    *Static Elec with Ice Needle
    *Fist of the Beast King with Glacial Blast

14D6F926 Panjyamon data:
Original: 01 02 FF 00 0A 2B 2C 2E 2D 2F 28 2A 29 0C 0B 5B FF FF FF FF FF
Changed:  04 01 02 00 0A 10 2C 2E 08 2F 11 15 16 13 0B 6E FF FF FF FF FF


11510BC Giga Freeze list:
Original: AA 00 02 00 //Mojyamon NPC which never uses this
Changed:  30 00 04 00 //Leomon which is also the value for Panyjamon 


1123196 Thunder Justice list:
Original: 6B 00 01 00 00 00 14 00 38 FF //Soulmon which now uses the value of Bakemon, so this is unnecessary
Changed:  30 00 00 00 00 00 24 FF 60 FF //Leomon which is also the value for Panyjamon 


1156D60 Ice statue list: A7 to 30 //Coelamon NPC which is unable to use the move; now is Leomon which is the value that Panyjamon also uses


116DBBC Aqua Magic list: AA to 30 //Mojyamon NPC which is unable to use the move; now is Leomon which is the value that Panyjamon also uses


1173812 Aurora Freeze list: AA to 30 //Mojyamon NPC which is unable to use the move; now is Leomon which is the value that Panyjamon also uses


1162406 Ice Needle list:
Original: AA 00 08 00 00 00 00 00 00 00 //Mojyamon NPC which is unable to use the move
Changed:  30 00 07 00 82 00 19 00 00 00 //Leomon which is also the value for Panyjamon


//Glacial blast had to be fixed in order to be used

Glacial blast data:

14D67604 Target distance: 00 00 00 00 to 40 7E 05 00 // 0 to 360000
14D67608 Damage: 00 00 to D9 00 // 0 to 217
14D6760A MP: 00 to 28 // 0 to 40
14D6760B Invincible Time no change
14D6760C Range: 00 to 02 //None to Large
14D6760D type: FF to 04 //None to Ice
14D6760E Effect no change
14D6760F Accuracy: 00 to 64 // 0 to 100

136CC08 Glacial blast fix: 58 to 6E //this was broken in the movement list, the game was expecting the "Ice Blast" ID to be used rather than the "Glacial Blast" ID

136CCC0 Glacial blast move list:
Original: 3D 00 03 00 32 00 00 00 5A 00 //not only it was expecting "Ice Blast" ID, it was expected to be done by a MegaSeadramon
Changed:  30 00 00 00 00 00 74 FF A2 FE //Leomon which is also the value for Panyjamon


Animation changes:
B94B84 Move 1 animation (Giga Freeze): 
Original: E6 9F 00 00 //Sonic Jab animation
Changed:  F8 C6 00 00 //War Cry animation

B94B98 Move 6 animation (Ice Statue): 
Original: DE C0 00 00 //Tremar animation
Changed:  68 A4 00 00 //Dynamite Kick animation




- Gigadramon has changed:
    *Its Battle type with a Wind type
    *Its Ice type with Fire type
    *Now it is Fire/Mech/Wind in that order
    *Dynamite Kick with Red Inferno
    *Megaton Punch with Spit Fire
    *Giga Freeze with Thunder Justice
    *Winter Blast with Hurricane
    *Ice Statue with Infinity Burn
    *Power Crane with Fire Tower
    *Pulse Laser with Magma Bomb
    *Reverse Program with Prominence Beam
    *Metal Sprinter with Heat Laser
    *All Range Beam with Meltdown


14D6F95A Gigadramon data:
Original: 05 04 01 00 0A 2C 2E 10 12 11 18 1C 1B 1F 1A 19 1D 1E 4E FF FF
Changed:  00 05 02 00 0A 03 02 08 0F 06 00 1C 04 01 05 07 1D 1E 4E FF FF


1106608 Red Inferno list:
Original: A5 00 04 00 00 00 50 00 9C FF //Monochromon NPC which is unable to use the move
Changed:  36 00 04 00 5A 00 00 00 00 00 //Megadramon which is the value for Gigadramon


1100A0C Spit Fire list:
Original: A5 00 04 00 00 00 50 00 9C FF //Monochromon NPC which is unable to use the move
Changed:  36 00 08 00 4A 01 00 00 00 00 //Megadramon which is the value for Gigadramon


11231A0 Thunder Justice list:
Original: 9D 00 01 00 00 00 14 00 38 FF //Bakemon NPC which is unable to use the move
Changed:  36 00 04 00 5A 00 00 00 00 00 //Leomon which is also the value for Panyjamon 


114B492 Hurricane list: 9D to 36 //Bakemon NPC which is unable to use the move; now is Megadramon which is the value that Gigadramon also uses


1117AB0 Infinity Burn list: 03 to 36 //Agumon which is unable to use the move; now is Megadramon which is the value that Gigadramon also uses


10F538E Fire tower list: 8F to 36 //Birdramon NPC which is unable to use the move; now is Megadramon which is the value that Gigadramon also uses


110C2F8 Magma Bomb list: 9A to 36 //Ogremon NPC which already uses the original Ogremon data; now is Megadramon which is the value that Gigadramon also uses


10FAECC Prominence Beam list:
Original: A5 00 04 00 00 00 50 00 9C FF //Monochromon NPC which is unable to use the move
Changed:  36 00 04 00 5A 00 00 00 00 00 //Megadramon which is the value for Gigadramon


1111D8E Heat Laser list: A5 to 36 //Monochromon NPC which is unable to use the move; now is Megadramon which is the value that Gigadramon also uses


111D800 Meltdown list: 8F to 36 //Birdramon NPC which is unable to use the move; now is Megadramon which is the value that Gigadramon also uses


Animation changes:
AE526C Move 1 animation (Red Inferno): 
Original: 46 7A 00 00 //Dynamite Kick animation
Changed:  1A 85 00 00 //Giga Freeze animation

AE5278 Move 4 animation (Hurricane): 
Original: 64 8A 00 00 //Winter Blast animation
Changed:  B6 B6 00 00 //DG Dimension animation

AE528C Move 9 animation (Prominence Beam): 
Original: 3A A2 00 00 //Reverse Prog animation
Changed:  1A 85 00 00 //Giga Freeze animation

AE5294 Move 11 animation (Meltdown): 
Original: 38 AF 00 00 //All Range Beam animation
Changed:  64 8A 00 00 //Winter Blast animation



- Metal Etemon has changed:
    *Its filth type with a mech type
    *Muscle Charge with Confused Storm
    *War Cry with Full Potential
    *Spinning Shot with Delete Program
    *Horizontal Kick with Static Elec
    *Ult Poop Hell with DG Dimension


14D6F98E MetalEtemon data:
Original: 01 02 06 00 0A 2B 2C 2E 2D 2F 28 2A 29 0B 09 08 37 38 5C FF FF
Changed:  01 02 05 00 0A 2B 2C 2E 2D 2F 28 0E 1E 0B 1C 08 0C 1D 5C FF FF


1145994 Confused Storm list: 99 to 2A //Unimon NPC which is unable to use the move; now is Etemon which is the value that MetalEtemon also uses


11A16F0 Full Potential list: 96 to 2A //Vademon NPC which is unable to use the move; now is Etemon which is the value that MetalEtemon also uses


1195ECA Delete Program list: 96 to 2A //Vademon NPC which is unable to use the move; now is Etemon which is the value that MetalEtemon also uses


113A0E6 Static Elect list: A6 to 2A //Leomon NPC which is unable to use the move; now is Etemon which is the value that MetalEtemon also uses


119BCA0 DG Dimension list: 96 to 2A //Vademon NPC which is unable to use the move; now is Etemon which is the value that MetalEtemon also uses


Animation change:
B6A75C Move 8 animation (Full Potential): 
Original: 48 D9 00 00 //War Cry animation
Changed:  5A F2 00 00 //Thunder Justice animation


//new update 02/12/23
// Etemon has a 10% chance of being extra generous, make sure to have 2 slots empty


13FE4D66 Etemon text

Original:
1A 00 82 68 81 40 82 88 82 85 82 81 82 92 81 40 82 99 82 8F 82 95 81 75 82 92 82 85 81 40 82 84 82 8F 82 89 82 8E 82 87 0D 00 
82 92 82 85 82 81 82 8C 81 40 82 87 82 8F 82 8F 82 84 81 40 82 94 82 88 82 85 82 93 82 85 81 40 82 84 82 81 82 99 82 93 81 42 
0D 00 82 68 81 75 82 8C 82 8C 81 40 82 93 82 85 82 8C 82 8C 81 40 82 99 82 8F 82 95 81 40 82 93 82 8F 82 8D 82 85 82 94 82 88 
82 89 82 8E 82 87 81 40 82 87 82 8F 82 8F 82 84 81 49 0D 00 00 00 1A 00 82 68 82 94 81 75 82 93 81 40 82 94 82 88 82 85 81 40 
82 85 82 98 82 94 82 92 82 85 82 8D 82 85 82 8C 82 99 81 40 82 96 82 81 82 8C 82 95 82 81 82 82 82 8C 82 85 0D 00 01 06 82 66 
82 8F 82 8C 82 84 82 85 82 8E 81 40 82 61 82 81 82 8E 82 81 82 8E 82 81 01 01 81 42 0D 00 00 00 1A 00 82 73 82 88 82 81 82 94 
81 75 82 8C 82 8C 81 40 82 82 82 85 81 40 82 86 82 89 82 86 82 94 82 99 81 40 82 94 82 88 82 8F 82 95 82 93 81 40 82 82 82 89 
82 94 82 93 81 42 0D 00 82 6D 82 8F 81 40 82 8C 82 8F 82 81 82 8E 82 93 81 43 81 40 82 90 82 8C 82 85 82 81 82 93 82 85 81 42 
0D 00 00 00 1B FD 10 02 50 0A 2E 0B 1A 00 82 6F 82 81 82 99 0D 00 82 63 82 8F 82 8E 81 75 82 94 81 40 82 90 82 81 82 99 0D 00 
00 00 12 00 16 00 2E 0B

Changed:
1A 00 82 67 82 81 82 96 82 85 81 40 82 81 81 40 82 8C 82 8F 82 8F 82 8B 81 40 82 81 82 94 81 40 82 94 82 88 82 89 82 93 81 49 
0D 00 00 00 1A 00 82 73 82 88 82 85 81 40 82 85 82 98 82 94 82 92 82 85 82 8D 82 85 82 8C 82 99 81 40 82 96 82 81 82 8C 82 95 
82 81 82 82 82 8C 82 85 0D 00 01 06 82 66 82 8F 82 8C 82 84 82 85 82 8E 81 40 82 61 82 81 82 8E 82 81 82 8E 82 81 01 01 81 42 
0D 00 00 00 1A 00 82 73 82 88 82 81 82 94 81 75 82 8C 82 8C 81 40 82 82 82 85 81 40 82 86 82 89 82 86 82 94 82 99 81 40 82 94 
82 88 82 8F 82 95 82 93 81 40 82 82 82 89 82 94 82 93 81 42 0D 00 82 6D 82 8F 81 40 82 8C 82 8F 82 81 82 8E 82 93 81 43 81 40 
82 90 82 8C 82 85 82 81 82 93 82 85 81 42 0D 00 00 00 1B FD 10 02 E0 09 2E 0B 1A 00 82 6F 82 81 82 99 0D 00 82 63 82 8F 82 8E 
81 75 82 94 81 40 82 90 82 81 82 99 0D 00 00 00 12 00 16 00 2E 0B 24 00 67 09 19 00 08 00 67 00 10 00 F2 09 18 00 50 0A 28 00 
6D 01 1B 08 1A 00 82 60 82 8E 81 40 82 85 82 98 82 94 82 92 82 81 81 40 82 93 82 8F 82 95 82 96 82 85 82 8E 82 89 82 92 81 40 
82 86 82 8F 82 92 0D 00 82 94 82 88 82 85 81 40 82 86 82 8F 82 93 82 85 82 92 81 49 0D 00 00 00 28 00 7F 01 19 00 00 00 00 00 
18 00 50 0A 10 00 66 0A


In case of full bag:

13FE4F08

Original:
BC 0A 19 00 1B 08 1A 00 82 78 82 8F 82 95 81 40 82 86 82 8F 82 8F 82 8C 81 49 81 40 82 78 82 8F 82 95 81 40 82 88 82 81 82 96 
82 85 81 40 82 94 82 8F 82 8F 81 40 82 8D 82 95 82 83 82 88 81 49 0D 00 00 00 2A 00 50 C3 00 00 FE 00 1B FD 1A 00 82 6E 82 8F 
82 90 82 93 81 43 81 40 82 68 81 40 82 88 82 81 82 96 82 85 81 40 82 94 82 8F 82 8F 81 40 82 8D 82 95 82 83 82 88 81 49 0D 00 
00 00 1C 00 03 00 64 00 64 0D

Changed:
B0 0A 19 00 1B 08 1A 00 82 60 82 92 82 85 81 40 82 99 82 8F 82 95 81 40 82 82 82 92 82 81 82 87 82 87 82 89 82 8E 82 87 81 40 
82 86 82 8F 82 8F 82 8C 81 49 0D 00 00 00 2A 00 50 C3 00 00 FE 00 1B FD 1A 00 82 6E 82 8F 82 90 82 93 81 43 81 40 82 8D 82 99 
81 40 82 82 82 81 82 87 81 40 82 89 82 93 81 40 82 86 82 95 82 8C 82 8C 0D 00 00 00 1C 00 03 00 64 00 64 0D 19 00 08 00 67 00 
18 00 F8 0A 19 00 28 00 7F 01



//Maeson exclusive miscellaneous changes

These are changes in the code that were done by just modifying 1 byte, since the code was found by me, I'm including these.

Since the change are small, I'll just show the offsets:

14CD1CA4, 14D14A04 & 14D14E6C total lifetime of a digimon


