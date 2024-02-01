//This patch allows you to learn more than 1 tech in battle (if applicable)

Orignal:

void LearnBattleMoves(void)

{
//code ignored

  if (MoveLearnCount != 0) 
  {
    rand = ReturnRandom(MoveLearnCount);
    MoveSelected = MovesLearnArray[rand];
    LearnMove((MoveSelected);
    RenderLearnedMoveText(MoveSelected);
  }
}


Changed:

void LearnBattleMoves(void)
{
//code ignored

  while(MoveLearnCount != 0; MoveLearnCount--)
 {
    MoveSelected = MovesLearnArray[MoveLearnCount];
    LearnMove((MoveSelected);
    RenderLearnedMoveText(MoveSelected);
  }
}






Disassembly:


Original:

                             LAB_800ed558                                   
        800ed558 0d 00 40 12     beq        s2,zero,0x800ed590
        800ed55c 00 00 00 00     _nop
        800ed560 b5 8d 02 0c     jal        0x800a36d4  //ReturnRandom                                   
        800ed564 21 20 40 02     _move      a0,s2
        800ed568 21 10 a2 03     addu       v0,sp,v0
        800ed56c 2c 00 42 90     lbu        v0,0x2c(v0)
        800ed570 00 00 00 00     nop
        800ed574 00 84 02 00     sll        s0,v0,0x10
        800ed578 03 84 10 00     sra        s0,s0,0x10
        800ed57c 21 88 00 02     move       s1,s0
        800ed580 c5 97 03 0c     jal        0x800e5f14  //LearnMove                                        
        800ed584 21 20 00 02     _move      a0,s0
        800ed588 5c 8b 01 0c     jal        0x80062d70  //RenderMoveLearnedText                                  
        800ed58c 21 20 20 02     _move      a0,s1


Changed:
                             LAB_800ed558                                   
        800ed558 0d 00 40 12     beq        s2,zero,0x800ed590
        800ed55c 00 00 00 00     _nop
                             LAB_800ed560                                   
        800ed560 ff ff 52 26     addiu      s2,s2,-0x1
        800ed564 21 10 b2 03     addu       v0,sp,s2
        800ed568 2c 00 42 90     lbu        v0,0x2c(v0)
        800ed56c 00 00 00 00     nop
        800ed570 00 84 02 00     sll        s0,v0,0x10
        800ed574 03 84 10 00     sra        s0,s0,0x10
        800ed578 c5 97 03 0c     jal        0x800e5f14  //LearnMove                                       
        800ed57c 21 20 00 02     _move      a0,s0
        800ed580 5c 8b 01 0c     jal        0x80062d70  //RenderMoveLearnedText                           
        800ed584 21 20 00 02     _move      a0,s0
        800ed588 f5 ff 40 16     bne        s2,zero,0x800ed560
        800ed58c 00 00 00 00     _nop

