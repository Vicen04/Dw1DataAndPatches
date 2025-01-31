void Scold/PraiseMechanics(int interactionValue) // checks if the scold or the praise has been used and adds happiness and/or discipline
{
  short addedDiscipline;
  short addedHappiness;
  short discipline;
  short happiness;
  
  happiness = DigimonHappiness;
  discipline = DigimonDiscipline;
  if (interactionValue == 4) // scold value
  { 
    if (ScoldFlag == '\x01') // has done something that requires a scold
	{ 
      addedDiscipline = 8;
      ScoldFlag = '\x02'; //Next item will not be rejected
	  //note that here the "addedHappiness" is never set, so it uses a value carried from a different function
      //This is equivalent to "addedHappiness = 3", the happiness added by a positive scold is actually unintended
    }
    else 
	{
      addedDiscipline = 2;
      addedHappiness = -10;
    }
    FavouriteItemRejected = 0; 
    ConditionFlag = ConditionFlag & 0xffffffef;
    if (ButteflyFlag == 0)
	{
      UnsetButterfly(ButterflyInstance);
      ButteflyFlag = -1;
    }
  }
  else //praise has been used, the interaction value here is "11"
  {  
    addedDiscipline = -5;
    addedHappiness = (discipline / 10 + 2);
  }
  if (((interactionValue == 4) && (NanimonEvoFlag = 0) && (DigimonLevel == 3)) && 
        ((discipline == 0) && (Happiness == -100))) //nanimon evo check
    NanimonEvoFlag = 1;
  
  DigimonDiscipline = DigimonDiscipline + addedDiscipline;
  DigimonHappiness = DigimonHappiness + addedHappiness;
  return;
}

Disassembly:

        Offset       Hex             Commands
  
        800a6230 d8 ff bd 27     addiu      sp,sp,-0x28
        800a6234 24 00 bf af     sw         ra,0x24(sp)
        800a6238 20 00 b4 af     sw         s4,0x20(sp)
        800a623c 1c 00 b3 af     sw         s3,0x1c(sp)
        800a6240 18 00 b2 af     sw         s2,0x18(sp)
        800a6244 14 80 01 3c     lui        at,0x8014
        800a6248 88 84 32 84     lh         s2,-0x7b78(at)                    
        800a624c 14 00 b1 af     sw         s1,0x14(sp)
        800a6250 14 80 01 3c     lui        at,0x8014
        800a6254 8a 84 34 84     lh         s4,-0x7b76(at)                     
        800a6258 10 00 b0 af     sw         s0,0x10(sp)
        800a625c 21 98 80 00     move       s3,a0
        800a6260 04 00 01 24     li         at,0x4
        800a6264 23 00 61 16     bne        s3,at,0x800a62f4
        800a6268 21 18 40 02     _move      v1,s2
        800a626c 2d 91 82 83     lb         v0,-0x6ed3(gp)
        800a6270 01 00 01 24     li         at,0x1
        800a6274 07 00 41 14     bne        v0,at,0x800a6294
        800a6278 00 00 00 00     _nop
        800a627c 08 00 02 24     li         v0,0x8
        800a6280 00 84 02 00     sll        s0,v0,0x10
        800a6284 02 00 02 24     li         v0,0x2
        800a6288 03 84 10 00     sra        s0,s0,0x10
        800a628c 07 00 00 10     b          0x800a62ac
        800a6290 2d 91 82 a3     _sb        v0,-0x6ed3(gp)
                             LAB_800a6294                                     
        800a6294 02 00 02 24     li         v0,0x2
        800a6298 00 84 02 00     sll        s0,v0,0x10
        800a629c f6 ff 02 24     li         v0,-0xa
        800a62a0 00 8c 02 00     sll        s1,v0,0x10
        800a62a4 03 84 10 00     sra        s0,s0,0x10
        800a62a8 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800a62ac                                     
        800a62ac 14 80 01 3c     lui        at,0x8014
        800a62b0 a4 84 20 a4     sh         zero,-0x7b5c(at)                  
        800a62b4 14 80 01 3c     lui        at,0x8014
        800a62b8 60 84 23 8c     lw         v1,-0x7ba0(at)                     
        800a62bc ef ff 02 24     li         v0,-0x11
        800a62c0 24 10 62 00     and        v0,v1,v0
        800a62c4 14 80 01 3c     lui        at,0x8014
        800a62c8 60 84 22 ac     sw         v0,-0x7ba0(at)                    
        800a62cc 28 91 82 8f     lw         v0,-0x6ed8(gp)
        800a62d0 00 00 00 00     nop
        800a62d4 14 00 40 14     bne        v0,zero,0x800a6328
        800a62d8 00 00 00 00     _nop
        800a62dc 34 91 84 8f     lw         a0,-0x6ecc(gp)
        800a62e0 ed a7 03 0c     jal        0x800e9fb4  //UnsetButterfly                                
        800a62e4 00 00 00 00     _nop
        800a62e8 ff ff 02 24     li         v0,-0x1
        800a62ec 0e 00 00 10     b          0x800a6328 
        800a62f0 28 91 82 af     _sw        v0,-0x6ed8(gp)
                             LAB_800a62f4                                     
        800a62f4 fb ff 02 24     li         v0,-0x5
        800a62f8 00 84 02 00     sll        s0,v0,0x10
        800a62fc 66 66 02 3c     lui        v0,0x6666
        800a6300 67 66 42 34     ori        v0,v0,0x6667
        800a6304 18 00 43 00     mult       v0,v1
        800a6308 03 84 10 00     sra        s0,s0,0x10
        800a630c 10 10 00 00     mfhi       v0
        800a6310 c2 1f 03 00     srl        v1,v1,0x1f
        800a6314 83 10 02 00     sra        v0,v0,0x2
        800a6318 21 10 43 00     addu       v0,v0,v1
        800a631c 02 00 42 20     addi       v0,v0,0x2
        800a6320 00 8c 02 00     sll        s1,v0,0x10
        800a6324 03 8c 11 00     sra        s1,s1,0x10
                             LAB_800a6328                              
        800a6328 14 80 01 3c     lui        at,0x8014
        800a632c 88 84 22 84     lh         v0,-0x7b78(at)                    
        800a6330 00 00 00 00     nop
        800a6334 20 10 50 00     add        v0,v0,s0
        800a6338 14 80 01 3c     lui        at,0x8014
        800a633c 88 84 22 a4     sh         v0,-0x7b78(at)                   
        800a6340 14 80 01 3c     lui        at,0x8014
        800a6344 8a 84 22 84     lh         v0,-0x7b76(at)                    
        800a6348 00 00 00 00     nop
        800a634c 20 10 51 00     add        v0,v0,s1   
        800a6350 14 80 01 3c     lui        at,0x8014
        800a6354 8a 84 22 a4     sh         v0,-0x7b76(at)                     
        800a6358 04 00 01 24     li         at,0x4
        800a635c 17 00 61 16     bne        s3,at,0x800a63bc
        800a6360 00 00 00 00     _nop
        800a6364 15 80 01 3c     lui        at,0x8015
        800a6368 a8 57 23 8c     lw         v1,0x57a8(at)                       
        800a636c 38 91 80 af     sw         zero,-0x6ec8(gp)
        800a6370 40 10 03 00     sll        v0,v1,0x1
        800a6374 20 10 43 00     add        v0,v0,v1
        800a6378 80 10 02 00     sll        v0,v0,0x2
        800a637c 20 10 43 00     add        v0,v0,v1
        800a6380 80 18 02 00     sll        v1,v0,0x2
        800a6384 13 80 02 3c     lui        v0,0x8013
        800a6388 d1 ce 42 24     addiu      v0,v0,-0x312f
        800a638c 21 10 43 00     addu       v0,v0,v1
        800a6390 00 00 42 90     lbu        v0,0x0(v0)
        800a6394 03 00 01 24     li         at,0x3
        800a6398 08 00 41 14     bne        v0,at,0x800a63bc
        800a639c 00 00 00 00     _nop
        800a63a0 06 00 40 16     bne        s2,zero,0x800a63bc
        800a63a4 00 00 00 00     _nop
        800a63a8 9c ff 01 24     li         at,-0x64
        800a63ac 03 00 81 16     bne        s4,at,0x800a63bc
        800a63b0 00 00 00 00     _nop
        800a63b4 01 00 02 24     li         v0,0x1
        800a63b8 38 91 82 af     sw         v0,-0x6ec8(gp)
                             LAB_800a63bc   
        800a63bc 24 00 bf 8f     lw         ra,0x24(sp)
        800a63c0 20 00 b4 8f     lw         s4,0x20(sp)
        800a63c4 1c 00 b3 8f     lw         s3,0x1c(sp)
        800a63c8 18 00 b2 8f     lw         s2,0x18(sp)
        800a63cc 14 00 b1 8f     lw         s1,0x14(sp)
        800a63d0 10 00 b0 8f     lw         s0,0x10(sp)
        800a63d4 08 00 e0 03     jr         ra
        800a63d8 28 00 bd 27     _addiu     sp,sp,0x28
