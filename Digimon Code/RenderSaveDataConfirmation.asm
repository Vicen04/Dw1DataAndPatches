//Renders the save data box when you confirm to start or load a saved game from the memory card

void RenderSaveDataConfirmation(void)
{
  char vStart;
  POLY_FT4 *prim;
  POLY_FT4 *prim_00;
  
  prim = (POLY_FT4 *)GSGetWorkBase();

  DrawText(prim, 58, 24, 0, 0, 144, 12, 0);
  DrawText(prim + 1, 58, 46, 0, 15, 24, 12, 0);

  if ((&SaveFileStrings)[SelectedSave].fileNumber == 0) 
  {
    prim_00 = prim + 3;
    DrawText(prim + 2, 88, 46, 144, 0, 108, 12, 0);
  }
  else 
  {
    DrawText(prim + 2, 88, 46, 24, 15, 72, 12, 0);
    prim_00 = prim + 5;
    DrawText(prim + 3, 166, 46, 108, 15, 96, 12, 0);
    DrawText(prim + 4, 88, 60, 0, 24, 224, 12, 0);
  }

  DrawText(prim_00, 58, 82, 0, 240, 28, 12, 0);
  DrawText(prim_00 + 1, 58, 94, 28, 240, 28, 12, 0);
  DrawText(prim_00 + 2, 58, 116, 20, 48, 204, 12, 0);
  GSSetWorkBase(prim_00 + 3);
  DrawSDBox(48, 19, 176, 22);
  DrawSDBox(48, 41, 224, 36);
  DrawSDBox(48, 77, 56, 34);
  DrawSDBox(48, 111, 224, 22);
  return;
}


Disassembly:

                       RenderSaveDataConfirmation                                   
        8010dc48 d8 ff bd 27     addiu      sp,sp,-0x28
        8010dc4c 24 00 bf af     sw         ra,0x24(sp)
        8010dc50 20 00 b0 af     sw         s0,0x20(sp)
        8010dc54 0e 63 02 0c     jal        0x80098c38  //GSGetWorkBase                                  
        8010dc58 00 00 00 00     _nop
        8010dc5c 21 80 40 00     move       s0,v0
        8010dc60 21 20 00 02     move       a0,s0
        8010dc64 10 00 a0 af     sw         zero,0x10(sp)
        8010dc68 90 00 02 24     li         v0,0x90
        8010dc6c 14 00 a2 af     sw         v0,0x14(sp)
        8010dc70 0c 00 02 24     li         v0,0xc
        8010dc74 18 00 a2 af     sw         v0,0x18(sp)
        8010dc78 1c 00 a0 af     sw         zero,0x1c(sp)
        8010dc7c 28 00 90 24     addiu      s0,a0,0x28
        8010dc80 3a 00 05 24     li         a1,0x3a
        8010dc84 18 00 06 24     li         a2,0x18
        8010dc88 58 34 04 0c     jal        0x8010d160  //DrawText                                   
        8010dc8c 21 38 00 00     _clear     a3
        8010dc90 0c 00 03 24     li         v1,0xc
        8010dc94 21 20 00 02     move       a0,s0
        8010dc98 10 00 a3 af     sw         v1,0x10(sp)
        8010dc9c 18 00 02 24     li         v0,0x18
        8010dca0 14 00 a2 af     sw         v0,0x14(sp)
        8010dca4 18 00 a3 af     sw         v1,0x18(sp)
        8010dca8 1c 00 a0 af     sw         zero,0x1c(sp)
        8010dcac 28 00 90 24     addiu      s0,a0,0x28
        8010dcb0 3a 00 05 24     li         a1,0x3a
        8010dcb4 2e 00 06 24     li         a2,0x2e
        8010dcb8 58 34 04 0c     jal        0x8010d160  //DrawText                                     
        8010dcbc 21 38 00 00     _clear     a3
        8010dcc0 10 95 83 8f     lw         v1,-0x6af0(gp)  //SelectedSave
        8010dcc4 00 00 00 00     nop
        8010dcc8 00 11 03 00     sll        v0,v1,0x4
        8010dccc 20 10 43 00     add        v0,v0,v1
        8010dcd0 80 18 02 00     sll        v1,v0,0x2
        8010dcd4 1c 80 02 3c     lui        v0,0x801c
        8010dcd8 68 f7 42 24     addiu      v0,v0,-0x898
        8010dcdc 21 10 43 00     addu       v0,v0,v1
        8010dce0 00 00 42 8c     lw         v0,0x0(v0)  //SaveFileStrings                  
        8010dce4 00 00 00 00     nop
        8010dce8 28 00 40 10     beq        v0,zero,0x8010dd8c
        8010dcec 00 00 00 00     _nop
        8010dcf0 0c 00 03 24     li         v1,0xc
        8010dcf4 21 20 00 02     move       a0,s0
        8010dcf8 10 00 a3 af     sw         v1,0x10(sp)
        8010dcfc 48 00 02 24     li         v0,0x48
        8010dd00 14 00 a2 af     sw         v0,0x14(sp)
        8010dd04 18 00 a3 af     sw         v1,0x18(sp)
        8010dd08 1c 00 a0 af     sw         zero,0x1c(sp)
        8010dd0c 28 00 90 24     addiu      s0,a0,0x28
        8010dd10 58 00 05 24     li         a1,0x58
        8010dd14 2e 00 06 24     li         a2,0x2e
        8010dd18 58 34 04 0c     jal        0x8010d160  //DrawText                                     
        8010dd1c 18 00 07 24     _li        a3,0x18
        8010dd20 0c 00 03 24     li         v1,0xc
        8010dd24 21 20 00 02     move       a0,s0
        8010dd28 10 00 a3 af     sw         v1,0x10(sp)
        8010dd2c 60 00 02 24     li         v0,0x60
        8010dd30 14 00 a2 af     sw         v0,0x14(sp)
        8010dd34 18 00 a3 af     sw         v1,0x18(sp)
        8010dd38 1c 00 a0 af     sw         zero,0x1c(sp)
        8010dd3c 28 00 90 24     addiu      s0,a0,0x28
        8010dd40 a6 00 05 24     li         a1,0xa6
        8010dd44 2e 00 06 24     li         a2,0x2e
        8010dd48 58 34 04 0c     jal        0x8010d160  //DrawText                                     
        8010dd4c 6c 00 07 24     _li        a3,0x6c
        8010dd50 18 00 02 24     li         v0,0x18
        8010dd54 10 00 a2 af     sw         v0,0x10(sp)
        8010dd58 e0 00 02 24     li         v0,0xe0
        8010dd5c 14 00 a2 af     sw         v0,0x14(sp)
        8010dd60 0c 00 02 24     li         v0,0xc
        8010dd64 18 00 a2 af     sw         v0,0x18(sp)
        8010dd68 21 20 00 02     move       a0,s0
        8010dd6c 1c 00 a0 af     sw         zero,0x1c(sp)
        8010dd70 28 00 90 24     addiu      s0,a0,0x28
        8010dd74 58 00 05 24     li         a1,0x58
        8010dd78 3c 00 06 24     li         a2,0x3c
        8010dd7c 58 34 04 0c     jal        0x8010d160  //DrawText                                      
        8010dd80 21 38 00 00     _clear     a3
        8010dd84 0e 00 00 10     b          0x8010ddc0
        8010dd88 f0 00 02 24     _li        v0,0xf0
                             LAB_8010dd8c                                   
        8010dd8c 21 20 00 02     move       a0,s0
        8010dd90 10 00 a0 af     sw         zero,0x10(sp)
        8010dd94 6c 00 02 24     li         v0,0x6c
        8010dd98 14 00 a2 af     sw         v0,0x14(sp)
        8010dd9c 0c 00 02 24     li         v0,0xc
        8010dda0 18 00 a2 af     sw         v0,0x18(sp)
        8010dda4 1c 00 a0 af     sw         zero,0x1c(sp)
        8010dda8 28 00 90 24     addiu      s0,a0,0x28
        8010ddac 58 00 05 24     li         a1,0x58
        8010ddb0 2e 00 06 24     li         a2,0x2e
        8010ddb4 58 34 04 0c     jal        0x8010d160  //DrawText                                      
        8010ddb8 90 00 07 24     _li        a3,0x90
        8010ddbc f0 00 02 24     li         v0,0xf0
                             LAB_8010ddc0                                    
        8010ddc0 10 00 a2 af     sw         v0,0x10(sp)
        8010ddc4 1c 00 02 24     li         v0,0x1c
        8010ddc8 14 00 a2 af     sw         v0,0x14(sp)
        8010ddcc 0c 00 02 24     li         v0,0xc
        8010ddd0 18 00 a2 af     sw         v0,0x18(sp)
        8010ddd4 21 20 00 02     move       a0,s0
        8010ddd8 1c 00 a0 af     sw         zero,0x1c(sp)
        8010dddc 28 00 90 24     addiu      s0,a0,0x28
        8010dde0 3a 00 05 24     li         a1,0x3a
        8010dde4 52 00 06 24     li         a2,0x52
        8010dde8 58 34 04 0c     jal        0x8010d160  //DrawText                                     
        8010ddec 21 38 00 00     _clear     a3
        8010ddf0 f0 00 02 24     li         v0,0xf0
        8010ddf4 10 00 a2 af     sw         v0,0x10(sp)
        8010ddf8 1c 00 07 24     li         a3,0x1c
        8010ddfc 21 20 00 02     move       a0,s0
        8010de00 14 00 a7 af     sw         a3,0x14(sp)
        8010de04 0c 00 02 24     li         v0,0xc
        8010de08 18 00 a2 af     sw         v0,0x18(sp)
        8010de0c 1c 00 a0 af     sw         zero,0x1c(sp)
        8010de10 28 00 90 24     addiu      s0,a0,0x28
        8010de14 3a 00 05 24     li         a1,0x3a
        8010de18 58 34 04 0c     jal        0x8010d160  //DrawText                                     
        8010de1c 5e 00 06 24     _li        a2,0x5e
        8010de20 30 00 02 24     li         v0,0x30
        8010de24 10 00 a2 af     sw         v0,0x10(sp)
        8010de28 cc 00 02 24     li         v0,0xcc
        8010de2c 14 00 a2 af     sw         v0,0x14(sp)
        8010de30 0c 00 02 24     li         v0,0xc
        8010de34 18 00 a2 af     sw         v0,0x18(sp)
        8010de38 21 20 00 02     move       a0,s0
        8010de3c 1c 00 a0 af     sw         zero,0x1c(sp)
        8010de40 28 00 90 24     addiu      s0,a0,0x28
        8010de44 3a 00 05 24     li         a1,0x3a
        8010de48 74 00 06 24     li         a2,0x74
        8010de4c 58 34 04 0c     jal        0x8010d160  //DrawText                                      
        8010de50 14 00 07 24     _li        a3,0x14
        8010de54 0a 63 02 0c     jal        0x80098c28  //GSGetWorkBase(param_1)                                   
        8010de58 21 20 00 02     _move      a0,s0
        8010de5c 30 00 04 24     li         a0,0x30
        8010de60 13 00 05 24     li         a1,0x13
        8010de64 b0 00 06 24     li         a2,0xb0
        8010de68 ae 34 04 0c     jal        0x8010d2b8  //DrawSDBox                                 
        8010de6c 16 00 07 24     _li        a3,0x16
        8010de70 30 00 04 24     li         a0,0x30
        8010de74 29 00 05 24     li         a1,0x29
        8010de78 e0 00 06 24     li         a2,0xe0
        8010de7c ae 34 04 0c     jal        0x8010d2b8  //DrawSDBox                                 
        8010de80 24 00 07 24     _li        a3,0x24
        8010de84 30 00 04 24     li         a0,0x30
        8010de88 4d 00 05 24     li         a1,0x4d
        8010de8c 38 00 06 24     li         a2,0x38
        8010de90 ae 34 04 0c     jal        0x8010d2b8  //DrawSDBox                                   
        8010de94 22 00 07 24     _li        a3,0x22
        8010de98 30 00 04 24     li         a0,0x30
        8010de9c 6f 00 05 24     li         a1,0x6f
        8010dea0 e0 00 06 24     li         a2,0xe0
        8010dea4 ae 34 04 0c     jal        0x8010d2b8  //DrawSDBox                           
        8010dea8 16 00 07 24     _li        a3,0x16
        8010deac 24 00 bf 8f     lw         ra,0x24(sp)
        8010deb0 20 00 b0 8f     lw         s0,0x20(sp)
        8010deb4 08 00 e0 03     jr         ra
        8010deb8 28 00 bd 27     _addiu     sp,sp,0x28
