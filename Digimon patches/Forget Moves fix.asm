This is a file that shows the changes made to the "forget moves" function from the game to fix the error caused after the game chooses which moves should be forgotten.
The error was caused by the game having the wrong calculations, it changed a lot more data than the intended, which in turn, deletes more moves than intended
This fix fully changes the old function with brand new code to be similar to the "learn  move" function, but making the game forget rather than learn a move.

Original:

void ForgetMovesAfterAllLivesLost(uint moveID)

{
  char moveValue;
  uint flagBit;
  int count;
  
  count = 0;
  do {
    moveValue = GetTechFromMove(DigimonPtr,(uint)(byte)(&DigimonCurrentTechs)[count]);
    if (param_1 == moveValue) 
      {
          (&DigimonCurrentTechs)[count] = -1;
      }
    count++;
    if (count > 3) {
      if ((moveID == 44) || (moveID == 48))
      {
        flagBit = 4096;
      }
      else if ((moveID == 55) || (moveID == 57)) 
      {
        flagBit = 0;
      }
      else {
        flagBit = moveID & 0x1f;
        if ((moveID < 0) && (flagBit != 0)) {
          flagBit = flagBit - 32;
        }
        flagBit = 1 << (flagBit & 0x1f) & 0xffff;
      }
      if (moveID < 0) {
        moveID = moveID + 31;
      }
      (&moveBitArray)[moveID >> 5] = (&moveBitArray)[moveId >> 5] & (flagBit ^ 0xffff);
      return;
    }
  } while( true );
}

Changed:
// similar to the learn moves code
void ForgetMovesAfterAllLivesLost(uint moveID)

{
  uint moveBit;
  int moveBitFlag;
  
  if ((moveID == 44) || (moveID == 48)) {
    flagBit = flagBit ^ 0x11000;
  }
  else if ((MoveID == 55) || (MoveID == 57)) {
    flagBit = flagBit ^ 0x2800000;
  }
  else {
    moveBit = moveID & 0x1f;
    if ((moveID < 0) && (moveBit != 0)) {
      moveBit = moveBit - 32;
    }
    moveBitFlag = moveID >> 5;
    if (moveID < 0) {
      moveBitFlag = (moveID + 31) >> 5;
    }
    (&moveBitArray)[moveBitFlag] = (&moveBitArray)[moveBitFlag] ^ 1 << (moveBit & 0x1f);
  }
  return;
}

Disassembly:

Original:

        Offset      Hex               Commands

        800e66e0 e8 ff bd 27     addiu      sp,sp,-0x18
        800e66e4 10 00 bf af     sw         ra,0x10(sp)
        800e66e8 21 40 80 00     move       t0,a0
        800e66ec 12 00 00 10     b          LAB_800e6738
        800e66f0 21 48 00 00     _clear     t1
                             LAB_800e66f4                                    
        800e66f4 15 80 02 3c     lui        v0,0x8015
        800e66f8 a8 57 42 24     addiu      v0,v0,0x57a8
        800e66fc 13 80 01 3c     lui        at,0x8013
        800e6700 21 10 49 00     addu       v0,v0,t1
        800e6704 48 f3 24 8c     lw         a0,-0xcb8(at)
        800e6708 44 00 45 90     lbu        a1,0x44(v0)                     
        800e670c 00 98 03 0c     jal        getTechFromMove            GetTechFromMove(a0 (*MapDataOffset), a1(MoveID))                        
        800e6710 00 00 00 00     _nop
        800e6714 07 00 02 15     bne        t0,v0,LAB_800e6734
        800e6718 00 00 00 00     _nop
        800e671c 15 80 02 3c     lui        v0,0x8015
        800e6720 ec 57 42 24     addiu      v0,v0,0x57ec
        800e6724 ff 00 03 24     li         v1,0xff
        800e6728 21 10 49 00     addu       v0,v0,t1
        800e672c 05 00 00 10     b          LAB_800e6744
        800e6730 00 00 43 a0     _sb        v1,0x0(v0)                         
                             LAB_800e6734                                    
        800e6734 01 00 29 21     addi       t1,t1,0x1
                             LAB_800e6738                                    
        800e6738 04 00 21 29     slti       at,t1,0x4
        800e673c ed ff 20 14     bne        at,zero,LAB_800e66f4
        800e6740 00 00 00 00     _nop
                             LAB_800e6744                                   
        800e6744 2c 00 01 24     li         at,0x2c
        800e6748 04 00 01 11     beq        t0,at,LAB_800e675c
        800e674c 00 00 00 00     _nop
        800e6750 30 00 01 24     li         at,0x30
        800e6754 03 00 01 15     bne        t0,at,LAB_800e6764
        800e6758 00 00 00 00     _nop
                             LAB_800e675c                                  
        800e675c 11 00 00 10     b          LAB_800e67a4
        800e6760 00 10 04 24     _li        a0,0x1000
                             LAB_800e6764                                  
        800e6764 37 00 01 24     li         at,0x37
        800e6768 04 00 01 11     beq        t0,at,LAB_800e677c
        800e676c 00 00 00 00     _nop
        800e6770 39 00 01 24     li         at,0x39
        800e6774 03 00 01 15     bne        t0,at,LAB_800e6784
        800e6778 00 00 00 00     _nop
                             LAB_800e677c                                    
        800e677c 09 00 00 10     b          LAB_800e67a4
        800e6780 21 20 00 00     _clear     a0
                             LAB_800e6784                                   
        800e6784 04 00 01 05     bgez       t0,LAB_800e6798
        800e6788 1f 00 03 31     _andi      v1,t0,0x1f
        800e678c 02 00 60 10     beq        v1,zero,LAB_800e6798
        800e6790 00 00 00 00     _nop
        800e6794 e0 ff 63 24     addiu      v1,v1,-0x20
                             LAB_800e6798                                     
        800e6798 01 00 02 24     li         v0,0x1
        800e679c 04 10 62 00     sllv       v0,v0,v1
        800e67a0 ff ff 44 30     andi       a0,v0,0xffff
                             LAB_800e67a4                                      
        800e67a4 ff ff 82 38     xori       v0,a0,0xffff
        800e67a8 ff ff 44 30     andi       a0,v0,0xffff
        800e67ac 03 00 01 05     bgez       t0,LAB_800e67bc
        800e67b0 43 c9 08 00     _sra       t9,t0,0x5
        800e67b4 1f 00 02 25     addiu      v0,t0,0x1f
        800e67b8 43 c9 02 00     sra        t9,v0,0x5
                             LAB_800e67bc                                    
        800e67bc 15 80 02 3c     lui        v0,0x8015
        800e67c0 80 18 19 00     sll        v1,t9,0x2
        800e67c4 00 58 42 24     addiu      v0,v0,0x5800
        800e67c8 21 18 43 00     addu       v1,v0,v1
        800e67cc 00 00 62 8c     lw         v0,0x0(v1)                       
        800e67d0 00 00 00 00     nop
        800e67d4 24 10 44 00     and        v0,v0,a0
        800e67d8 00 00 62 ac     sw         v0,0x0(v1)                      
        800e67dc 10 00 bf 8f     lw         ra,0x10(sp)
        800e67e0 00 00 00 00     nop
        800e67e4 08 00 e0 03     jr         ra
        800e67e8 18 00 bd 27     _addiu     sp,sp,0x18


Changed:
                   
        800e66e0 e8 ff bd 27     addiu      sp,sp,-0x18
        800e66e4 10 00 bf af     sw         ra,0x10(sp)
        800e66e8 00 00 00 00     nop
        800e66ec 2c 00 01 24     li         at,0x2c
        800e66f0 04 00 81 10     beq        a0,at,LAB_800e6704
        800e66f4 00 00 00 00     _nop
        800e66f8 30 00 01 24     li         at,0x30
        800e66fc 0b 00 81 14     bne        a0,at,LAB_800e672c
        800e6700 00 00 00 00     _nop
                             LAB_800e6704                                    
        800e6704 15 80 01 3c     lui        at,0x8015
        800e6708 04 58 22 8c     lw         v0,0x5804(at)                          
        800e670c 00 00 00 00     nop
        800e6710 21 18 00 00     clear      v1
        800e6714 01 00 03 3c     lui        v1,0x1
        800e6718 00 10 63 24     addiu      v1,v1,0x1000
        800e671c 26 10 62 00     xor        v0,v1,v0
        800e6720 15 80 01 3c     lui        at,0x8015
        800e6724 2e 00 00 10     b          LAB_800e67e0
        800e6728 04 58 22 ac     _sw        v0,0x5804(at)                          
                             LAB_800e672c                                     
        800e672c 37 00 01 24     li         at,0x37
        800e6730 04 00 81 10     beq        a0,at,LAB_800e6744
        800e6734 00 00 00 00     _nop
        800e6738 39 00 01 24     li         at,0x39
        800e673c 0a 00 81 14     bne        a0,at,LAB_800e6768
        800e6740 00 00 00 00     _nop
                             LAB_800e6744                                     
        800e6744 15 80 01 3c     lui        at,0x8015
        800e6748 04 58 22 8c     lw         v0,0x5804(at)                          
        800e674c 00 00 00 00     nop
        800e6750 21 18 00 00     clear      v1
        800e6754 80 02 03 3c     lui        v1,0x280
        800e6758 26 10 62 00     xor        v0,v1,v0
        800e675c 15 80 01 3c     lui        at,0x8015
        800e6760 1f 00 00 10     b          LAB_800e67e0
        800e6764 04 58 22 ac     _sw        v0,0x5804(at)                        
                             LAB_800e6768                                  
        800e6768 04 00 81 04     bgez       a0,LAB_800e677c
        800e676c 1f 00 83 30     _andi      v1,a0,0x1f
        800e6770 02 00 60 10     beq        v1,zero,LAB_800e677c
        800e6774 00 00 00 00     _nop
        800e6778 e0 ff 63 24     addiu      v1,v1,-0x20
                             LAB_800e677c                                    
        800e677c 01 00 02 24     li         v0,0x1
        800e6780 04 28 62 00     sllv       a1,v0,v1
        800e6784 00 00 00 00     nop
        800e6788 03 00 81 04     bgez       a0,LAB_800e6798
        800e678c 43 c9 04 00     _sra       t9,a0,0x5
        800e6790 1f 00 82 24     addiu      v0,a0,0x1f
        800e6794 43 c9 02 00     sra        t9,v0,0x5
                             LAB_800e6798                                    
        800e6798 15 80 02 3c     lui        v0,0x8015
        800e679c 80 18 19 00     sll        v1,t9,0x2
        800e67a0 00 58 42 24     addiu      v0,v0,0x5800
        800e67a4 21 18 43 00     addu       v1,v0,v1
        800e67a8 00 00 62 8c     lw         v0,0x0(v1)                      
        800e67ac 00 00 00 00     nop
        800e67b0 26 10 45 00     xor        v0,v0,a1
        800e67b4 00 00 62 ac     sw         v0,0x0(v1)                     
        800e67b8 10 00 bf 8f     lw         ra,0x10(sp)
        800e67bc 00 00 00 00     nop
        800e67c0 00 00 00 00     nop
        800e67c4 00 00 00 00     nop
        800e67c8 00 00 00 00     nop
        800e67cc 00 00 00 00     nop
        800e67d0 00 00 00 00     nop
        800e67d4 00 00 00 00     nop
        800e67d8 00 00 00 00     nop
        800e67dc 00 00 00 00     nop
                             LAB_800e67e0                                    
        800e67e0 00 00 00 00     nop
        800e67e4 08 00 e0 03     jr         ra
        800e67e8 18 00 bd 27     _addiu     sp,sp,0x18

