void RenderLoadDataBoxes(int currentBox,short cPosX,short cPosY)
{
  char cVar1;
  POLY_FT4 *prim;
  int colour;
  char vStart;
  
  cVar1 = (currentBox % 7) * 24;
  vStart = cVar1 + 15;
  prim = GSGetWorkBase();
  
  if ((((&SaveFileStrings)[currentBox].fileNumber == 0) || (DAT_80135034 == 0)) &&
     (((&SaveFileStrings)[currentBox].fileNumber != 0 || (DAT_80135034 != 0)))) 
	 colour = 1;  
  else 
    colour = 0;  
  
  DrawText(prim, cPosX + 10, cPosY + 5, 0, vStart, 24, 12, colour);
  
  if ((&SaveFileStrings)[currentBox].fileNumber != 0) 
  {
    DrawText(prim + 1, cPosX + 40, cPosY + 5, 24, vStart, 72, 12, colour);
    DrawText(prim + 2, cPosX + 118, cPosY + 5, 108, vStart, 96, 12, colour);    
    DrawText(prim + 3, cPosX + 40, cPosY + 19, 24, cVar1 + vStart, 224, 12, colour);
  }
  
  GSSetWorkBase(prim + 4);
  DrawSDBox(cPosX,cPosY, 224, 36);
  return;



Disassembly:

                             RenderLoadDataBoxes                                  
                                                                                         
        8010d868 c0 ff bd 27     addiu      sp,sp,-0x40
        8010d86c 3c 00 bf af     sw         ra,0x3c(sp)
        8010d870 38 00 b6 af     sw         s6,0x38(sp)
        8010d874 34 00 b5 af     sw         s5,0x34(sp)
        8010d878 30 00 b4 af     sw         s4,0x30(sp)
        8010d87c 2c 00 b3 af     sw         s3,0x2c(sp)
        8010d880 28 00 b2 af     sw         s2,0x28(sp)
        8010d884 24 00 b1 af     sw         s1,0x24(sp)
        8010d888 21 88 80 00     move       s1,
        8010d88c 07 00 02 24     li         v0,0x7
        8010d890 1a 00 22 02     div        s1,v0
        8010d894 20 00 b0 af     sw         s0,0x20(sp)
        8010d898 10 18 00 00     mfhi       v1
        8010d89c 40 10 03 00     sll        v0,v1,0x1
        8010d8a0 20 10 43 00     add        v0,v0,v1
        8010d8a4 c0 10 02 00     sll        v0,v0,0x3
        8010d8a8 21 98 a0 00     move       s3,a1
        8010d8ac 21 a0 c0 00     move       s4,a2
        8010d8b0 0e 63 02 0c     jal        0x80098c38  //GSSetWorkBase                                         
        8010d8b4 0c 00 52 20     _addi      s2,v0,0xc
        8010d8b8 21 80 40 00     move       s0,v0
        8010d8bc 00 11 11 00     sll        v0,s1,0x4
        8010d8c0 20 10 51 00     add        v0,v0,s1
        8010d8c4 80 18 02 00     sll        v1,v0,0x2
        8010d8c8 1c 80 02 3c     lui        v0,0x801c
        8010d8cc 68 f7 42 24     addiu      v0,v0,-0x898
        8010d8d0 21 10 43 00     addu       v0,v0,v1
        8010d8d4 00 00 42 8c     lw         v0,0x0(v0)  //SaveFileStrings                   
        8010d8d8 21 b0 60 00     move       s6,v1
        8010d8dc 05 00 40 10     beq        v0,zero,0x8010d8f4
        8010d8e0 21 18 40 00     _move      v1,v0
        8010d8e4 08 95 82 8f     lw         v0,-0x6af8(gp)  //DAT_80135034                    
        8010d8e8 00 00 00 00     nop
        8010d8ec 07 00 40 14     bne        v0,zero,0x8010d90c
        8010d8f0 00 00 00 00     _nop
                             LAB_8010d8f4                                   
        8010d8f4 07 00 60 14     bne        v1,zero,0x8010d914
        8010d8f8 00 00 00 00     _nop
        8010d8fc 08 95 82 8f     lw         v0,-0x6af8(gp)  //DAT_80135034                   
        8010d900 00 00 00 00     nop
        8010d904 03 00 40 14     bne        v0,zero,0x8010d914
        8010d908 00 00 00 00     _nop
                             LAB_8010d90c                                    
        8010d90c 02 00 00 10     b          0x8010d918
        8010d910 21 88 00 00     _clear     s1
                             LAB_8010d914                                  
        8010d914 01 00 11 24     li         s1,0x1
                             LAB_8010d918                                   
        8010d918 21 20 00 02     move       a0,s0
        8010d91c 05 00 86 22     addi       a2,s4,0x5
        8010d920 10 00 b2 af     sw         s2,0x10(sp)
        8010d924 18 00 02 24     li         v0,0x18
        8010d928 14 00 a2 af     sw         v0,0x14(sp)
        8010d92c 0c 00 02 24     li         v0,0xc
        8010d930 18 00 a2 af     sw         v0,0x18(sp)
        8010d934 1c 00 b1 af     sw         s1,0x1c(sp)
        8010d938 28 00 90 24     addiu      s0,a0,0x28
        8010d93c 0a 00 65 22     addi       a1,s3,0xa
        8010d940 21 a8 c0 00     move       s5,a2
        8010d944 58 34 04 0c     jal        0x8010d160  //DrawText                                     
        8010d948 21 38 00 00     _clear     a3
        8010d94c 1c 80 02 3c     lui        v0,0x801c
        8010d950 68 f7 42 24     addiu      v0,v0,-0x898
        8010d954 21 10 56 00     addu       v0,v0,s6
        8010d958 00 00 42 8c     lw         v0,0x0(v0)  //SaveFileStrings                    
        8010d95c 00 00 00 00     nop
        8010d960 27 00 40 10     beq        v0,zero,0x8010da00
        8010d964 00 00 00 00     _nop
        8010d968 21 20 00 02     move       a0,s0
        8010d96c 28 00 65 22     addi       a1,s3,0x28
        8010d970 10 00 b2 af     sw         s2,0x10(sp)
        8010d974 48 00 02 24     li         v0,0x48
        8010d978 14 00 a2 af     sw         v0,0x14(sp)
        8010d97c 0c 00 02 24     li         v0,0xc
        8010d980 18 00 a2 af     sw         v0,0x18(sp)
        8010d984 1c 00 b1 af     sw         s1,0x1c(sp)
        8010d988 28 00 90 24     addiu      s0,a0,0x28
        8010d98c 21 b0 a0 00     move       s6,a1
        8010d990 21 30 a0 02     move       a2,s5
        8010d994 58 34 04 0c     jal        0x8010d160  //DrawText                                   
        8010d998 18 00 07 24     _li        a3,0x18
        8010d99c 21 20 00 02     move       a0,s0
        8010d9a0 10 00 b2 af     sw         s2,0x10(sp)
        8010d9a4 60 00 02 24     li         v0,0x60
        8010d9a8 14 00 a2 af     sw         v0,0x14(sp)
        8010d9ac 0c 00 02 24     li         v0,0xc
        8010d9b0 18 00 a2 af     sw         v0,0x18(sp)
        8010d9b4 1c 00 b1 af     sw         s1,0x1c(sp)
        8010d9b8 28 00 90 24     addiu      s0,a0,0x28
        8010d9bc 76 00 65 22     addi       a1,s3,0x76
        8010d9c0 21 30 a0 02     move       a2,s5
        8010d9c4 58 34 04 0c     jal        0x8010d160  //DrawText                                   
        8010d9c8 6c 00 07 24     _li        a3,0x6c
        8010d9cc 0c 00 42 22     addi       v0,s2,0xc
        8010d9d0 10 00 a2 af     sw         v0,0x10(sp)
        8010d9d4 e0 00 02 24     li         v0,0xe0
        8010d9d8 14 00 a2 af     sw         v0,0x14(sp)
        8010d9dc 0c 00 02 24     li         v0,0xc
        8010d9e0 18 00 a2 af     sw         v0,0x18(sp)
        8010d9e4 21 20 00 02     move       a0,s0
        8010d9e8 1c 00 b1 af     sw         s1,0x1c(sp)
        8010d9ec 28 00 90 24     addiu      s0,a0,0x28
        8010d9f0 13 00 86 22     addi       a2,s4,0x13
        8010d9f4 21 28 c0 02     move       a1,s6
        8010d9f8 58 34 04 0c     jal        0x8010d160  //DrawText                                      
        8010d9fc 18 00 07 24     _li        a3,0x18
                             LAB_8010da00                                   
        8010da00 0a 63 02 0c     jal        0x80098c28  //GSSetWorkBase(param_1)                                  
        8010da04 21 20 00 02     _move      a0,s0
        8010da08 21 20 60 02     move       a0,s3
        8010da0c 21 28 80 02     move       a1,s4
        8010da10 e0 00 06 24     li         a2,0xe0
        8010da14 ae 34 04 0c     jal        0x8010d2b8  //DrawSDBox                                   
        8010da18 24 00 07 24     _li        a3,0x24
        8010da1c 3c 00 bf 8f     lw         ra,0x3c(sp)
        8010da20 38 00 b6 8f     lw         s6,0x38(sp)
        8010da24 34 00 b5 8f     lw         s5,0x34(sp)
        8010da28 30 00 b4 8f     lw         s4,0x30(sp)
        8010da2c 2c 00 b3 8f     lw         s3,0x2c(sp)
        8010da30 28 00 b2 8f     lw         s2,0x28(sp)
        8010da34 24 00 b1 8f     lw         s1,0x24(sp)
        8010da38 20 00 b0 8f     lw         s0,0x20(sp)
        8010da3c 08 00 e0 03     jr         ra
        8010da40 40 00 bd 27     _addiu     sp,sp,0x40
