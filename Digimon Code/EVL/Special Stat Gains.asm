void SpecialStatsGains(short *StatsPointer,int BrainStatGains) //Used to calculate the stat gains from fresh/in-training digimon, devimon, numemon, nanimon, sukamon and evolving from sukamon

{  
  if (EvoItemFlag == 0) // checks if a evo item was used
  {
    if (DigimonType == 39) //sukamon
    {
      if (OldHP < 0)  
        OldHP = OldHP + 1;
      HPGains = (int)StatsPointer[8] - (OldHP / 2); // StatsPointer[8] is the Max HP
      if (HPGains < 0) {
        HPGains = HPGains + 1;
      }
      StatsPointer[8] = OldHp + (short)(HPGains / 2); //New Max HP

      if (OldMP < 0) {
        OldMP = OldMP + 1;
      }
      MPGains = (int)StatsPointer[9] - (OldMP / 2); // StatsPointer[9] is the Max MP
      if (MPGains < 0) {
        MPGains = MPGains + 1;
      }
      StatsPointer[9] = OldMP + (short)(MPGains / 2); // New Max MP

      if (OldOff < 0) {
        OldOff = OldOff + 1;
      }
      OffGains = (int)*StatsPointer - (OldOff / 2); // *StatsPointer is the Offense
      if (OffGains < 0) {
        OffGains = OffGains + 1;
      }
      *StatsPointer = OldOff + (short)(OffGains / 2); // New Offense

      if (OldDef < 0) {
        OldDef = OldDef + 1;
      }
      DefGains = (int)StatsPointer[1] - (OldDef / 2); // StatsPointer[1] is the Defense
      if (DefGains < 0) {
        DefGains = DefGains + 1;
      }
      StatsPointer[1] = OldDef + (short)(DefGains / 2); // New Defense

      if (OldSpeed < 0) {
        OldSpeed = OldSpeed + 1;
      }
      SpeedGains = (int)StatsPointer[2] - (OldSpeed / 2); // StatsPointer[2] is the Speed
      if (SpeedGains < 0) {
        SpeedGains = SpeedGains + 1;
      }
      StatsPointer[2] = OldSpeed + (short)(SpeedGains / 2); // New Speed

      if (OldBrains < 0) {
        OldBrains = OldBrains + 1;
      }
      BrainsGains = (int)StatsPointer[3] - (OldBrains / 2);  // StatsPointer[3] is the Brains
      if (BrainsGains < 0) {
        BrainsGains = BrainsGains + 1;
      }
      StatsPointer[3] = OldBrains + (short)(BrainsGains / 2);

      VirusBar = 0; //resets the virus bar
    }
    else {
      StatsPointer[8] = (short)((StatsPointer[8] * BrainStatGains) / 10); // HP calculation
      StatsPointer[9] = (short)((StatsPointer[9] * BrainStatGains) / 10); // MP calculation
      *StatsPointer = (short)((*StatsPointer * BrainStatGains) / 10); // Offense calculation
      StatsPointer[1] = (short)((StatsPointer[1] * BrainStatGains) / 10); // Defense calculation
      StatsPointer[2] = (short)((StatsPointer[2] * BrainStatGains) / 10); // Speed calculation
      StatsPointer[3] = (short)((StatsPointer[3] * BrainStatGains) / 10); // Brains calculation
    }

    CheckLimits(); // Checks if any stat is over 9999 for HP/MP, 999 for the others and puts them back to the right limit

    if (StatsPointer[8] < StatsPointer[10]) // checks if the current HP is bigger than the Max HP
    {
      StatsPointer[10] = StatsPointer[8];
    }
    if (StatsPointer[9] < StatsPointer[11]) // checks if the current MP is bigger than the Max MP
    {
      StatsPointer[11] = StatsPointer[9];
    }
  }
  return;
}


Disassembly:
              
        Offset       Hex         Command       
             
        80063b28 e8 ff bd 27     addiu      sp,sp,-0x18
        80063b2c 14 00 bf af     sw         ra,0x14(sp)
        80063b30 10 00 b0 af     sw         s0,0x10(sp)
        80063b34 26 93 82 83     lb         v0,-0x6cda(gp)
        80063b38 00 00 00 00     nop
        80063b3c b8 00 40 14     bne        v0,zero,0x80063e20  // LAB_80063e20
        80063b40 21 80 80 00     _move      s0,a0
        80063b44 15 80 01 3c     lui        at,0x8015
        80063b48 a8 57 22 8c     lw         v0,0x57a8(at)   //Digimon Type offset                      
        80063b4c 27 00 01 24     li         at,0x27
        80063b50 4b 00 41 10     beq        v0,at,0x80063c80 // LAB_80063c80
        80063b54 00 00 00 00     _nop
        80063b58 10 00 02 86     lh         v0,0x10(s0)
        80063b5c 21 18 a0 00     move       v1,a1
        80063b60 18 00 45 00     mult       v0,a1
        80063b64 66 66 02 3c     lui        v0,0x6666
        80063b68 12 20 00 00     mflo       a0
        80063b6c 67 66 42 34     ori        v0,v0,0x6667
        80063b70 00 00 00 00     nop
        80063b74 18 00 44 00     mult       v0,a0
        80063b78 c2 2f 04 00     srl        a1,a0,0x1f
        80063b7c 10 20 00 00     mfhi       a0
        80063b80 83 20 04 00     sra        a0,a0,0x2
        80063b84 21 20 85 00     addu       a0,a0,a1
        80063b88 10 00 04 a6     sh         a0,0x10(s0)
        80063b8c 12 00 04 86     lh         a0,0x12(s0)
        80063b90 00 00 00 00     nop
        80063b94 18 00 83 00     mult       a0,v1
        80063b98 12 28 00 00     mflo       a1
        80063b9c 21 20 40 00     move       a0,v0
        80063ba0 00 00 00 00     nop
        80063ba4 18 00 85 00     mult       a0,a1
        80063ba8 10 20 00 00     mfhi       a0
        80063bac c2 2f 05 00     srl        a1,a1,0x1f
        80063bb0 83 20 04 00     sra        a0,a0,0x2
        80063bb4 21 20 85 00     addu       a0,a0,a1
        80063bb8 12 00 04 a6     sh         a0,0x12(s0)
        80063bbc 00 00 04 86     lh         a0,0x0(s0)
        80063bc0 00 00 00 00     nop
        80063bc4 18 00 83 00     mult       a0,v1
        80063bc8 12 28 00 00     mflo       a1
        80063bcc 21 20 40 00     move       a0,v0
        80063bd0 00 00 00 00     nop
        80063bd4 18 00 85 00     mult       a0,a1
        80063bd8 10 20 00 00     mfhi       a0
        80063bdc c2 2f 05 00     srl        a1,a1,0x1f
        80063be0 83 20 04 00     sra        a0,a0,0x2
        80063be4 21 20 85 00     addu       a0,a0,a1
        80063be8 00 00 04 a6     sh         a0,0x0(s0)
        80063bec 02 00 04 86     lh         a0,0x2(s0)
        80063bf0 00 00 00 00     nop
        80063bf4 18 00 83 00     mult       a0,v1
        80063bf8 12 28 00 00     mflo       a1
        80063bfc 21 20 40 00     move       a0,v0
        80063c00 00 00 00 00     nop
        80063c04 18 00 85 00     mult       a0,a1
        80063c08 10 20 00 00     mfhi       a0
        80063c0c c2 2f 05 00     srl        a1,a1,0x1f
        80063c10 83 20 04 00     sra        a0,a0,0x2
        80063c14 21 20 85 00     addu       a0,a0,a1
        80063c18 02 00 04 a6     sh         a0,0x2(s0)
        80063c1c 04 00 04 86     lh         a0,0x4(s0)
        80063c20 00 00 00 00     nop
        80063c24 18 00 83 00     mult       a0,v1
        80063c28 12 28 00 00     mflo       a1
        80063c2c 21 20 40 00     move       a0,v0
        80063c30 00 00 00 00     nop
        80063c34 18 00 85 00     mult       a0,a1
        80063c38 10 20 00 00     mfhi       a0
        80063c3c c2 2f 05 00     srl        a1,a1,0x1f
        80063c40 83 20 04 00     sra        a0,a0,0x2
        80063c44 21 20 85 00     addu       a0,a0,a1
        80063c48 04 00 04 a6     sh         a0,0x4(s0)
        80063c4c 06 00 04 86     lh         a0,0x6(s0)
        80063c50 00 00 00 00     nop
        80063c54 18 00 83 00     mult       a0,v1
        80063c58 12 18 00 00     mflo       v1
        80063c5c 00 00 00 00     nop
        80063c60 00 00 00 00     nop
        80063c64 18 00 43 00     mult       v0,v1
        80063c68 10 10 00 00     mfhi       v0
        80063c6c c2 1f 03 00     srl        v1,v1,0x1f
        80063c70 83 10 02 00     sra        v0,v0,0x2
        80063c74 21 10 43 00     addu       v0,v0,v1
        80063c78 59 00 00 10     b          0x80063de0 //LAB_80063de0
        80063c7c 06 00 02 a6     _sh        v0,0x6(s0)
                             LAB_80063c80                                     
        80063c80 14 80 01 3c     lui        at,0x8014
        80063c84 d6 84 24 84     lh         a0,-0x7b2a(at) //OldHp                       
        80063c88 10 00 03 86     lh         v1,0x10(s0)
        80063c8c 03 00 81 04     bgez       a0,0x80063c9c // LAB_80063c9c
        80063c90 43 c8 04 00     _sra       t9,a0,0x1
        80063c94 01 00 82 24     addiu      v0,a0,0x1
        80063c98 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063c9c                                  
        80063c9c 22 10 79 00     sub        v0,v1,t9
        80063ca0 03 00 41 04     bgez       v0,0x80063cb0  // LAB_80063cb0
        80063ca4 43 c8 02 00     _sra       t9,v0,0x1
        80063ca8 01 00 42 24     addiu      v0,v0,0x1
        80063cac 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063cb0                                  
        80063cb0 20 10 99 00     add        v0,a0,t9
        80063cb4 10 00 02 a6     sh         v0,0x10(s0)
        80063cb8 14 80 01 3c     lui        at,0x8014
        80063cbc d8 84 24 84     lh         a0,-0x7b28(at) //OldMP                      
        80063cc0 12 00 03 86     lh         v1,0x12(s0)
        80063cc4 03 00 81 04     bgez       a0,0x80063cd4 // LAB_80063cd4 
        80063cc8 43 c8 04 00     _sra       t9,a0,0x1
        80063ccc 01 00 82 24     addiu      v0,a0,0x1
        80063cd0 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063cd4                                  
        80063cd4 22 10 79 00     sub        v0,v1,t9
        80063cd8 03 00 41 04     bgez       v0,0x80063ce8 // LAB_80063ce8
        80063cdc 43 c8 02 00     _sra       t9,v0,0x1
        80063ce0 01 00 42 24     addiu      v0,v0,0x1
        80063ce4 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063ce8                                  
        80063ce8 20 10 99 00     add        v0,a0,t9
        80063cec 12 00 02 a6     sh         v0,0x12(s0)
        80063cf0 14 80 01 3c     lui        at,0x8014
        80063cf4 da 84 23 84     lh         v1,-0x7b26(at) //OldOff                  
        80063cf8 00 00 00 00     nop
        80063cfc 03 00 61 04     bgez       v1,LAB_80063d0c
        80063d00 43 c8 03 00     _sra       t9,v1,0x1
        80063d04 01 00 62 24     addiu      v0,v1,0x1
        80063d08 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063d0c                                   
        80063d0c 00 00 02 86     lh         v0,0x0(s0)
        80063d10 00 00 00 00     nop
        80063d14 22 10 59 00     sub        v0,v0,t9
        80063d18 03 00 41 04     bgez       v0,LAB_80063d28
        80063d1c 43 c8 02 00     _sra       t9,v0,0x1
        80063d20 01 00 42 24     addiu      v0,v0,0x1
        80063d24 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063d28                                  
        80063d28 20 10 79 00     add        v0,v1,t9
        80063d2c 00 00 02 a6     sh         v0,0x0(s0)
        80063d30 14 80 01 3c     lui        at,0x8014
        80063d34 dc 84 24 84     lh         a0,-0x7b24(at) //OldDef                     
        80063d38 02 00 03 86     lh         v1,0x2(s0)
        80063d3c 03 00 81 04     bgez       a0,0x80063d4c  // LAB_80063d4c
        80063d40 43 c8 04 00     _sra       t9,a0,0x1
        80063d44 01 00 82 24     addiu      v0,a0,0x1
        80063d48 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063d4c                                   
        80063d4c 22 10 79 00     sub        v0,v1,t9
        80063d50 03 00 41 04     bgez       v0,0x80063d60 // LAB_80063d60
        80063d54 43 c8 02 00     _sra       t9,v0,0x1
        80063d58 01 00 42 24     addiu      v0,v0,0x1
        80063d5c 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063d60                                    
        80063d60 20 10 99 00     add        v0,a0,t9
        80063d64 02 00 02 a6     sh         v0,0x2(s0)
        80063d68 14 80 01 3c     lui        at,0x8014
        80063d6c de 84 24 84     lh         s0,-0x7b22(at) //OldSpeed                 
        80063d70 04 00 03 86     lh         v1,0x4(s0)
        80063d74 03 00 81 04     bgez       a0,LAB_80063d84
        80063d78 43 c8 04 00     _sra       t9,a0,0x1
        80063d7c 01 00 82 24     addiu      v0,a0,0x1
        80063d80 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063d84                                   
        80063d84 22 10 79 00     sub        v0,v1,t9
        80063d88 03 00 41 04     bgez       v0,0x80063d98 // LAB_80063d98
        80063d8c 43 c8 02 00     _sra       t9,v0,0x1
        80063d90 01 00 42 24     addiu      v0,v0,0x1
        80063d94 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063d98                                    
        80063d98 20 10 99 00     add        v0,a0,t9
        80063d9c 04 00 02 a6     sh         v0,0x4(s0)
        80063da0 14 80 01 3c     lui        at,0x8014
        80063da4 e0 84 24 84     lh         a0,-0x7b20(at) //OldBrains                
        80063da8 06 00 03 86     lh         v1,0x6(s0)
        80063dac 03 00 81 04     bgez       a0,0x80063dbc // LAB_80063dbc
        80063db0 43 c8 04 00     _sra       t9,a0,0x1
        80063db4 01 00 82 24     addiu      v0,a0,0x1
        80063db8 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063dbc                                   
        80063dbc 22 10 79 00     sub        v0,v1,t9
        80063dc0 03 00 41 04     bgez       v0,0x80063dd0 // LAB_80063dd0
        80063dc4 43 c8 02 00     _sra       t9,v0,0x1
        80063dc8 01 00 42 24     addiu      v0,v0,0x1
        80063dcc 43 c8 02 00     sra        t9,v0,0x1
                             LAB_80063dd0                                     
        80063dd0 20 10 99 00     add        v0,a0,t9
        80063dd4 06 00 02 a6     sh         v0,0x6(s0)
        80063dd8 14 80 01 3c     lui        at,0x8014
        80063ddc 7e 84 20 a4     sh         zero,-0x7b82(at) //VirusBar                      
                             LAB_80063de0                                    
        80063de0 8c 8f 01 0c     jal        0x80063e30          //CheckLimits()                            
        80063de4 00 00 00 00     _nop
        80063de8 10 00 02 86     lh         v0,0x10(s0)
        80063dec 14 00 03 86     lh         v1,0x14(s0)
        80063df0 00 00 00 00     nop
        80063df4 2a 08 43 00     slt        at,v0,v1
        80063df8 02 00 20 10     beq        at,zero,0x80063e04 // LAB_80063e04
        80063dfc 21 20 40 00     _move      a0,v0
        80063e00 14 00 04 a6     sh         a0,0x14(s0)
                             LAB_80063e04                                   
        80063e04 12 00 02 86     lh         v0,0x12(s0)
        80063e08 16 00 03 86     lh         v1,0x16(s0)
        80063e0c 00 00 00 00     nop
        80063e10 2a 08 43 00     slt        at,v0,v1
        80063e14 02 00 20 10     beq        at,zero,0x80063e20 // LAB_80063e20
        80063e18 21 20 40 00     _move      a0,v0
        80063e1c 16 00 04 a6     sh         a0,0x16(s0)
                             LAB_80063e20                                    
        80063e20 14 00 bf 8f     lw         ra,0x14(sp)
        80063e24 10 00 b0 8f     lw         s0,0x10(sp)
        80063e28 08 00 e0 03     jr         ra
        80063e2c 18 00 bd 27     _addiu     sp,sp,0x18
