This is the function that makes your digimon forget moves after losing all your lives
The location of the code is based in the NTSC version

Translated to something similar to C:
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
//wrong flagBit
        flagBit = 4096;
      }
      else if ((moveID == 55) || (moveID == 57)) 
      {
//wrong flagBit
        flagBit = 0;
      }
      else {
        flagBit = moveID & 0x1f;
//should never happen
        if ((moveID < 0) && (flagBit != 0)) {
          flagBit = flagBit - 32;
        }
        flagBit = 1 << (flagBit & 0x1f) & 0xffff;
      }
//should never happen
      if (moveID < 0) {
        moveID = moveID + 31;
      }
//this is wrong, it does not work as intended and causes a glitch
      (&moveBitArray)[moveID >> 5] = (&moveBitArray)[moveId >> 5] & (flagBit ^ 0xffff);
      return;
    }
  } while( true );
}

Disassembly:

800e66e0 addiu sp, sp, -0x18
800e66e4 sw ra, local_8(sp)
800e66e8 move t0, a0
800e66ec b 0x800e6738
800e66f0 clear t1
800e66f4 lui v0, 0x8015
800e66f8 addiu v0, v0, 0x57a8
800e66fc lui at 0x8013
800e6700 addu v0, v0, t1
800e6704 lw a0, -0xcb8(at)
800e6708 lbu a1, 0x44(v0)
800e670c jal 0x800e6000
800e6710 nop
800e6714 bne t0, v0, 0x800e6734
800e6718 nop
800e671c lui v0, 0x8015
800e6720 addiu, v0, v0, 0c57ec
800e6724 li v1, 0xff
800e6728 addu v0, v0, t1,
800e672c b 0x800e6744
800e6730 sb v1, 0x0(v0)
800e6734 addi t1, t1, 0x1
800e6738 slti at, t1, 0x4
800e673c bne at,zero,0x800e66f4
800e6740 nop
800e6744 li at, 0x2c
800e6748 beq t0,at,0x800e675c
800e674c nop
800e6750 li at, 0x30
800e6754 bne t0, at, 0x800e6764
800e6758 nop
800e675c b 0x800e6764
800e6760 li a0, 0x1000
800e6764 li at, 0x37
800e6768 bew t0, at, 0x800e677c
800e676c nop
800e6770 li at, 0x39
800e6774 bne t0, at, 0x800e6764
800e6778 b 0x800e6764
800e677c clear a0
800e6780 bgez t0, 0x800e6798
800e6784 andi v1, t0, 0x1f
800e6788 beq v1, zero, 0x800e6798
800e678c nop
800e6790 addiu v1, v1, -0x20
800e6794 li v0, 0x1
800e6798 sllv v0, v0, v1
800e679c andi a0, v0, 0xffff
800e67a0 xori v0, a0, 0xffff
800e67a4 andi a0, v0, 0xffff
800e67a8 bgez t0, 0x800e67bc
800e67ac sra t9, t0, 0x5
800e67b0 addiu v0, t0, 0x1f
800e67b4 sra t9, v0, 0x5
800e67b8 lui v0, 0x8015
800e67bc sll v1, t9, 0x2
800e67c0 addiu v0, v0, 0x5800
800e67c4 addu v1, v0, v1
800e67c8 lw v0, 0x0(v1)
800e67cc nop
800e67d0 and v0, v0, a0
800e67d4 sw v0, 0x0(v1)
800e67d8 lw ra local_8(sp)
800e67dc nop
800e67e0 jr ra
800e67e4 addiu sp, sp, 0x18

Hex, starts at 14D1DA98:

e8 ff bd 27 10 00 bf af 21 40 80 00 12 00 00 10 
21 48 00 00 15 80 02 3c a8 57 42 24 13 80 01 3c 
21 10 49 00 48 f3 24 8c 44 00 45 90 00 98 03 0c 
00 00 00 00 07 00 02 15 00 00 00 00 15 80 02 3c 
ec 57 42 24 ff 00 03 24 21 10 49 00 05 00 00 10 
00 00 43 a0 01 00 29 21 04 00 21 29 ed ff 20 14 
00 00 00 00 2c 00 01 24 04 00 01 11 00 00 00 00 
30 00 01 24 03 00 01 15 00 00 00 00 11 00 00 10 
00 10 04 24 37 00 01 24 04 00 01 11 00 00 00 00 
39 00 01 24 03 00 01 15 00 00 00 00 09 00 00 10 
21 20 00 00 04 00 01 05 1f 00 03 31 02 00 60 10 
00 00 00 00 e0 ff 63 24 01 00 02 24 04 10 62 00 
ff ff 44 30 ff ff 82 38 ff ff 44 30 03 00 01 05 
43 c9 08 00 1f 00 02 25 43 c9 02 00 15 80 02 3c 
80 18 19 00 00 58 42 24 21 18 43 00 00 00 62 8c 
00 00 00 00 24 10 44 00 00 00 62 ac 10 00 bf 8f 
00 00 00 00 08 00 e0 03 18 00 bd 27

