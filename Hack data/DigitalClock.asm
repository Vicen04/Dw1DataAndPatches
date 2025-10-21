//How the render clock function was changed

//There is also a change in the sprite, but I won't include it

void RenderClock(void)

{
  uint animation;
  uint texPage;
  uint clut;
  int daytime;
  
  if ((hours < 6) || (16 < hours)) 
    daytime = 1;  
  else 
    daytime = 0;
  
  animation = currentFrame & 0xf;

  if (currentFrame < 0 && ((currentFrame & 0xf) != 0))) 
    animation = animation - 16;
  
  if (animation < 0) 
    animation = animation + 3;
  
  animation = (animation / 4) & 0xff;

  if (TimeTicking == 0) 
    ClockPosX = ClockPosX + -50;  
  else 
  {
    RenderString(&NumbersRender, 0, 240);  //Renders the numbers in the VRAM
    ClockPosX = ClockPosX + 50;
  }

  if (-135 < ClockPosX) 
    ClockPosX = -135;
  
  if (ClockPosX < -220) 
    ClockPosX = -220;
  
  texPage = GetTexturePage(0, 0, 896, 448);
  clut = GetClutIndex(256, 497);
  CreateRectPolyFT4(ClockPosX + -6, -93, 16, 16, 192, 224, texPage, clut, 9, 0);

  texPage = GetTexturePage(0, 0, 896, 448);
  clut = GetClutIndex(256, 497);
  CreateRectPolyFT4(ClockPosX + 37, -83, 16, 16, 192, 240, texPage, clut, 9, 0);

  texPage = GetTexturePage(0, 0, 896, 448);
  clut = GetClutIndex(256, 498);
  CreateRectPolyFT4(ClockPosX + 8, -100, 32, 16, ClockmonUvX[animation + daytime * 4], ClockmonUvY[animation + daytime * 4], texPage, clut, 9, 0);

  texPage = GetTexturePage(0, 0, 896, 448);
  clut = GetClutIndex(256, 496);
  CreateRectPolyFT4(ClockPosX, -88, 48, 37, 210, 216, texPage, clut, 10, 0);

  renderNumber(0, ClockPosX + 7, -73, 2, hours, 1);
  renderNumber(0, ClockPosX + 26, -73, 2, minutes, 1);

  if (minutes < 10) 
    renderNumber(0, ClockPosX + 26, -73, 1, 0, 1);
  
  RenderOverworldData(TimeTicking);
  return;
}



                             RenderClockBars                                  
        800dd2a4 90 93 82 87     lh         v0,-0x6c70(gp)  //hours                            
        800dd2a8 c8 ff bd 27     addiu      sp,sp,-0x38
        800dd2ac 34 00 bf af     sw         ra,0x34(sp)
        800dd2b0 30 00 b2 af     sw         s2,0x30(sp)
        800dd2b4 2c 00 b1 af     sw         s1,0x2c(sp)
        800dd2b8 28 00 b0 af     sw         s0,0x28(sp)
        800dd2bc 06 00 41 28     slti       at,v0,0x6
        800dd2c0 06 00 20 14     bne        at,zero,0x800dd2dc
        800dd2c4 21 18 40 00     _move      v1,v0
        800dd2c8 11 00 61 28     slti       at,v1,0x11
        800dd2cc 03 00 20 10     beq        at,zero,0x800dd2dc
        800dd2d0 00 00 00 00     _nop
        800dd2d4 02 00 00 10     b          0x800dd2e0
        800dd2d8 21 88 00 00     _clear     s1
                             LAB_800dd2dc                                   
        800dd2dc 01 00 11 24     li         s1,0x1
                             LAB_800dd2e0                                    
        800dd2e0 dc 93 83 97     lhu        v1,-0x6c24(gp)  //currentFrame                   
        800dd2e4 00 00 00 00     nop
        800dd2e8 04 00 61 04     bgez       v1,0x800dd2fc
        800dd2ec 0f 00 62 30     _andi      v0,v1,0xf
        800dd2f0 02 00 40 10     beq        v0,zero,0x800dd2fc
        800dd2f4 00 00 00 00     _nop
        800dd2f8 f0 ff 42 24     addiu      v0,v0,-0x10
                             LAB_800dd2fc                                   
        800dd2fc 03 00 41 04     bgez       v0,0x800dd30c
        800dd300 83 c8 02 00     _sra       t9,v0,0x2
        800dd304 03 00 42 24     addiu      v0,v0,0x3
        800dd308 83 c8 02 00     sra        t9,v0,0x2
                             LAB_800dd30c                                    
        800dd30c f4 92 82 83     lb         v0,-0x6d0c(gp)  //TimeTicking                     
        800dd310 00 00 00 00     nop
        800dd314 06 00 40 14     bne        v0,zero,0x800dd330
        800dd318 ff 00 32 33     _andi      s2,t9,0xff
        800dd31c f2 92 82 87     lh         v0,-0x6d0e(gp)  //ClockPosX                      
        800dd320 00 00 00 00     nop
        800dd324 ce ff 42 20     addi       v0,v0,-0x32
        800dd328 0a 00 00 10     b          0x800dd354
        800dd32c f2 92 82 a7     _sh        v0,-0x6d0e(gp)  //ClockPosX                     
                             LAB_800dd330                                  
        800dd330 12 80 04 3c     lui        a0,0x8012
        800dd334 54 4c 84 24     addiu      a0,a0,0x4c54   //NumbersRender         
        800dd338 21 28 00 00     clear      a1
        800dd33c c9 33 04 0c     jal        RenderString                                    
        800dd340 f0 00 06 24     _li        a2,0xf0
        800dd344 f2 92 82 87     lh         v0,-0x6d0e(gp)  //ClockPosX                       
        800dd348 00 00 00 00     nop
        800dd34c 32 00 42 20     addi       v0,v0,0x32
        800dd350 f2 92 82 a7     sh         v0,-0x6d0e(gp)  //ClockPosX                       
                             LAB_800dd354                                  
        800dd354 f2 92 82 87     lh         v0,-0x6d0e(gp)  //ClockPosX                       
        800dd358 00 00 00 00     nop
        800dd35c 7a ff 41 28     slti       at,v0,-0x86
        800dd360 03 00 20 14     bne        at,zero,0x800dd370
        800dd364 00 00 00 00     _nop
        800dd368 79 ff 02 24     li         v0,-0x87
        800dd36c f2 92 82 a7     sh         v0,-0x6d0e(gp)  //ClockPosX                      
                             LAB_800dd370                                   
        800dd370 f2 92 82 87     lh         v0,-0x6d0e(gp)  //ClockPosX                      
        800dd374 00 00 00 00     nop
        800dd378 24 ff 41 28     slti       at,v0,-0xdc
        800dd37c 03 00 20 10     beq        at,zero,0x800dd38c
        800dd380 00 00 00 00     _nop
        800dd384 24 ff 02 24     li         v0,-0xdc
        800dd388 f2 92 82 a7     sh         v0,-0x6d0e(gp)  //ClockPosX                       
                             LAB_800dd38c                                   
        800dd38c 21 20 00 00     clear      a0
        800dd390 21 28 00 00     clear      a1
        800dd394 80 03 06 24     li         a2,0x380
        800dd398 a0 4a 02 0c     jal        0x80092a80  //GetTexturePage                                   
        800dd39c c0 01 07 24     _li        a3,0x1c0
        800dd3a0 21 80 40 00     move       s0,v0
        800dd3a4 00 01 04 24     li         a0,0x100
        800dd3a8 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                     
        800dd3ac f1 01 05 24     _li        a1,0x1f1
        800dd3b0 c0 00 03 24     li         v1,0xc0
        800dd3b4 10 00 a3 af     sw         v1,0x10(sp)
        800dd3b8 e0 00 03 24     li         v1,0xe0
        800dd3bc 14 00 a3 af     sw         v1,0x14(sp)
        800dd3c0 18 00 b0 af     sw         s0,0x18(sp)
        800dd3c4 1c 00 a2 af     sw         v0,0x1c(sp)
        800dd3c8 09 00 02 24     li         v0,0x9
        800dd3cc 20 00 a2 af     sw         v0,0x20(sp)
        800dd3d0 24 00 a0 af     sw         zero,0x24(sp)
        800dd3d4 f2 92 82 87     lh         v0,-0x6d0e(gp)  //ClockPosX                        
        800dd3d8 10 00 07 24     li         a3,0x10
        800dd3dc fa ff 44 20     addi       a0,v0,-0x6
        800dd3e0 a3 ff 05 24     li         a1,-0x5d
        800dd3e4 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                               
        800dd3e8 21 30 e0 00     _move      a2,a3
        800dd3ec 21 20 00 00     clear      a0
        800dd3f0 21 28 00 00     clear      a1
        800dd3f4 80 03 06 24     li         a2,0x380
        800dd3f8 a0 4a 02 0c     jal        0x80092a80  //GetTexturePage                                         
        800dd3fc c0 01 07 24     _li        a3,0x1c0
        800dd400 21 80 40 00     move       s0,v0
        800dd404 00 01 04 24     li         a0,0x100
        800dd408 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                    
        800dd40c f1 01 05 24     _li        a1,0x1f1
        800dd410 c0 00 03 24     li         v1,0xc0
        800dd414 10 00 a3 af     sw         v1,0x10(sp)
        800dd418 f0 00 03 24     li         v1,0xf0
        800dd41c 14 00 a3 af     sw         v1,0x14(sp)
        800dd420 18 00 b0 af     sw         s0,0x18(sp)
        800dd424 1c 00 a2 af     sw         v0,0x1c(sp)
        800dd428 09 00 02 24     li         v0,0x9
        800dd42c 20 00 a2 af     sw         v0,0x20(sp)
        800dd430 24 00 a0 af     sw         zero,0x24(sp)
        800dd434 f2 92 82 87     lh         v0,-0x6d0e(gp)  //ClockPosX                       
        800dd438 10 00 07 24     li         a3,0x10
        800dd43c 25 00 44 20     addi       a0,v0,0x25
        800dd440 a3 ff 05 24     li         a1,-0x5d
        800dd444 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                               
        800dd448 21 30 e0 00     _move      a2,a3
        800dd44c 80 80 11 00     sll        s0,s1,0x2
        800dd450 21 20 00 00     clear      a0
        800dd454 21 28 00 00     clear      a1
        800dd458 80 03 06 24     li         a2,0x380
        800dd45c a0 4a 02 0c     jal        0x80092a80  //GetTexturePage                                         
        800dd460 c0 01 07 24     _li        a3,0x1c0
        800dd464 21 88 40 00     move       s1,v0
        800dd468 00 01 04 24     li         a0,0x100
        800dd46c af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                     
        800dd470 f2 01 05 24     _li        a1,0x1f2
        800dd474 b0 88 83 27     addiu      v1,gp,-0x7750
        800dd478 21 18 70 00     addu       v1,v1,s0
        800dd47c 21 18 43 02     addu       v1,s2,v1
        800dd480 00 00 63 90     lbu        v1,0x0(v1)  //ClockmonUvX                     
        800dd484 9c ff 05 24     li         a1,-0x64
        800dd488 10 00 a3 af     sw         v1,0x10(sp)
        800dd48c b8 88 83 27     addiu      v1,gp,-0x7748
        800dd490 21 18 70 00     addu       v1,v1,s0
        800dd494 21 18 43 02     addu       v1,s2,v1
        800dd498 00 00 63 90     lbu        v1,0x0(v1)  //ClockmonUvY                         
        800dd49c 20 00 06 24     li         a2,0x20
        800dd4a0 14 00 a3 af     sw         v1,0x14(sp)
        800dd4a4 18 00 b1 af     sw         s1,0x18(sp)
        800dd4a8 1c 00 a2 af     sw         v0,0x1c(sp)
        800dd4ac 09 00 02 24     li         v0,0x9
        800dd4b0 20 00 a2 af     sw         v0,0x20(sp)
        800dd4b4 24 00 a0 af     sw         zero,0x24(sp)
        800dd4b8 f2 92 82 87     lh         v0,-0x6d0e(gp)  //ClockPosX                       
        800dd4bc 10 00 07 24     li         a3,0x10
        800dd4c0 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                               
        800dd4c4 08 00 44 20     _addi      a0,v0,0x8
        800dd4c8 21 20 00 00     clear      a0
        800dd4cc 21 28 00 00     clear      a1
        800dd4d0 80 03 06 24     li         a2,0x380
        800dd4d4 a0 4a 02 0c     jal        0x80092a80  //GetTexturePage                                        
        800dd4d8 c0 01 07 24     _li        a3,0x1c0
        800dd4dc 21 80 40 00     move       s0,v0
        800dd4e0 00 01 04 24     li         a0,0x100
        800dd4e4 af 4a 02 0c     jal        0x80092abc  //GetClutIndex                                   
        800dd4e8 f0 01 05 24     _li        a1,0x1f0
        800dd4ec d0 00 03 24     li         v1,0xd0
        800dd4f0 10 00 a3 af     sw         v1,0x10(sp)
        800dd4f4 d8 00 03 24     li         v1,0xd8
        800dd4f8 14 00 a3 af     sw         v1,0x14(sp)
        800dd4fc 18 00 b0 af     sw         s0,0x18(sp)
        800dd500 1c 00 a2 af     sw         v0,0x1c(sp)
        800dd504 0a 00 02 24     li         v0,0xa
        800dd508 20 00 a2 af     sw         v0,0x20(sp)
        800dd50c 24 00 a0 af     sw         zero,0x24(sp)
        800dd510 f2 92 84 87     lh         a0,-0x6d0e(gp)  //ClockPosX                       
        800dd514 a8 ff 05 24     li         a1,-0x58
        800dd518 30 00 06 24     li         a2,0x30
        800dd51c 4a e2 02 0c     jal        0x800b8928  //CreateRectPolyFT4                              
        800dd520 27 00 07 24     _li        a3,0x27
        800dd524 13 80 01 3c     lui        at,0x8013
        800dd528 bc 4e 22 84     lh         v0,offset hours(at)                            
        800dd52c f2 92 85 87     lh         a1,-0x6d0e(gp)  //ClockPosX                       
        800dd530 10 00 a2 af     sw         v0,0x10(sp)
        800dd534 07 00 a5 24     addiu      a1,a1,0x7
        800dd538 01 00 02 24     li         v0,0x1
        800dd53c 14 00 a2 af     sw         v0,0x14(sp)
        800dd540 21 20 00 00     clear      a0
        800dd544 b7 ff 06 24     li         a2,-0x49
        800dd548 11 95 03 0c     jal        0x800e5444  //renderNumber                                     
        800dd54c 02 00 07 24     _li        a3,0x2
        800dd550 01 00 04 24     li         a0,0x1
        800dd554 13 80 01 3c     lui        at,0x8013
        800dd558 be 4e 22 84     lh         v0,0x4ebe(at)   //minutes                                
        800dd55c f2 92 85 87     lh         a1,-0x6d0e(gp)  //ClockPosX                       
        800dd560 10 00 a2 af     sw         v0,0x10(sp)
        800dd564 1a 00 a5 24     addiu      a1,a1,0x1a
        800dd568 14 00 a4 af     sw         a0,0x14(sp)
        800dd56c 21 20 00 00     clear      a0
        800dd570 b7 ff 06 24     li         a2,-0x49
        800dd574 11 95 03 0c     jal        0x800e5444  //renderNumber                                   
        800dd578 02 00 07 24     _li        a3,0x2
        800dd57c 13 80 01 3c     lui        at,0x8013
        800dd580 be 4e 22 84     lh         v0,0x4ebe(at)   //minutes                        
        800dd584 00 00 00 00     nop
        800dd588 0a 00 41 28     slti       at,v0,0xa
        800dd58c 0b 00 20 10     beq        at,zero,0x800dd5bc
        800dd590 01 00 02 24     _li        v0,0x1
        800dd594 23 00 05 24     li         a1,0x23
        800dd598 13 80 01 3c     lui        at,0x8013
        800dd59c f2 92 85 87     lh         a1,-0x6d0e(gp)  //ClockPosX                      
        800dd5a0 10 00 a0 af     sw         zero,0x10(sp)
        800dd5a4 1a 00 a5 24     addiu      a1,a1,0x1a
        800dd5a8 14 00 a2 af     sw         v0,0x14(sp)
        800dd5ac 21 20 00 00     clear      a0
        800dd5b0 b7 ff 06 24     li         a2,-0x49
        800dd5b4 11 95 03 0c     jal        0x800e5444  //renderNumber                                   
        800dd5b8 01 00 07 24     _li        a3,0x1
                             LAB_800dd5bc                                   
        800dd5bc f4 92 84 83     lb         a0,-0x6d0c(gp)  //TimeTicking                     
        800dd5c0 db a4 02 0c     jal        0x800a936c  //RenderOverworldData                          
        800dd5c4 00 00 00 00     _nop
        800dd5c8 34 00 bf 8f     lw         ra,0x34(sp)
        800dd5cc 30 00 b2 8f     lw         s2,0x30(sp)
        800dd5d0 2c 00 b1 8f     lw         s1,0x2c(sp)
        800dd5d4 28 00 b0 8f     lw         s0,0x28(sp)
        800dd5d8 08 00 e0 03     jr         ra
        800dd5dc 38 00 bd 27     _addiu     sp,sp,0x38
