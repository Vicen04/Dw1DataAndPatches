//Function that loads and shows the text after you have learned a technique during brains training


RenderTechBrainsLearnText 
{
  if (TechtextLenght == 0) 
    TechtextLenght = TechtextLenght + 1;

  renderSpriteFromVRAM(3,-78, 24, TechtextLenght * 2 * 12, 12, 0, 120, 4, 0); // The text is too short here
  
  if (TechtextLenght == 0) 
    TechtextLenght = TechtextLenght + 1;
	
  renderSpriteFromVRAM(0, TechtextLenght * 2 * 12 - 78, 24, 12, 0, 132, 4, 0); // Tries to render the "was mastered" on the same line as the learned tech, but with a low widht
  
  renderSpriteFromVRAM(0, -0x4e, 36, 48, 0, 12, 132, 4, 0); // Another attempt to "was mastered", probably to follow the earlier render...
}

// renderSpriteFromVRAM(int colorId, short posX, short posY, short uvWidht, short uvHeight, char uvX, char vY, int offset, int hasShadow)



Disassembly:

                            
                        RenderTechBrainsLearnText 
  
	     Offset       Hex         Commands  
        8008a9f8 d0 ff bd 27     addiu      sp,sp,-0x30
        8008a9fc 28 00 bf af     sw         ra,0x28(sp)
        8008aa00 0c 00 02 24     li         v0,0xc
        8008aa04 10 00 a2 af     sw         v0,0x10(sp)
        8008aa08 14 00 a0 af     sw         zero,0x14(sp)
        8008aa0c 78 00 02 24     li         v0,0x78
        8008aa10 18 00 a2 af     sw         v0,0x18(sp)
        8008aa14 04 00 02 24     li         v0,0x4
        8008aa18 1c 00 a2 af     sw         v0,0x1c(sp)
        8008aa1c 20 00 a0 af     sw         zero,0x20(sp)
        8008aa20 66 98 82 93     lbu        v0,-0x679a(gp)
        8008aa24 00 00 00 00     nop
        8008aa28 03 00 41 04     bgez       v0,0x8008aa38
        8008aa2c 43 c8 02 00     _sra       t9,v0,0x1
        8008aa30 01 00 42 24     addiu      v0,v0,0x1
        8008aa34 43 c8 02 00     sra        t9,v0,0x1
                             LAB_8008aa38                                     
        8008aa38 40 10 19 00     sll        v0,t9,0x1
        8008aa3c 20 10 59 00     add        v0,v0,t9
        8008aa40 80 38 02 00     sll        a3,v0,0x2
        8008aa44 03 00 04 24     li         a0,0x3
        8008aa48 b2 ff 05 24     li         a1,-0x4e
        8008aa4c d4 96 03 0c     jal        0x800e5b50   //RenderSpriteFromVRAM
        8008aa50 18 00 06 24     _li        a2,0x18
        8008aa54 0c 00 02 24     li         v0,0xc
        8008aa58 10 00 a2 af     sw         v0,0x10(sp)
        8008aa5c 14 00 a0 af     sw         zero,0x14(sp)
        8008aa60 84 00 02 24     li         v0,0x84
        8008aa64 18 00 a2 af     sw         v0,0x18(sp)
        8008aa68 04 00 02 24     li         v0,0x4
        8008aa6c 1c 00 a2 af     sw         v0,0x1c(sp)
        8008aa70 20 00 a0 af     sw         zero,0x20(sp)
        8008aa74 66 98 82 93     lbu        v0,-0x679a(gp)
        8008aa78 00 00 00 00     nop
        8008aa7c 03 00 41 04     bgez       v0,0x8008aa8c
        8008aa80 43 c8 02 00     _sra       t9,v0,0x1
        8008aa84 01 00 42 24     addiu      v0,v0,0x1
        8008aa88 43 c8 02 00     sra        t9,v0,0x1
                             LAB_8008aa8c                                  
        8008aa8c 40 10 19 00     sll        v0,t9,0x1
        8008aa90 20 10 59 00     add        v0,v0,t9
        8008aa94 80 10 02 00     sll        v0,v0,0x2
        8008aa98 b2 ff 45 20     addi       a1,v0,-0x4e
        8008aa9c 21 20 00 00     clear      a0
        8008aaa0 18 00 06 24     li         a2,0x18
        8008aaa4 d4 96 03 0c     jal        0x800e5b50  //RenderSpriteFromVRAM
        8008aaa8 0c 00 07 24     _li        a3,0xc
        8008aaac 0c 00 02 24     li         v0,0xc
        8008aab0 10 00 a2 af     sw         v0,0x10(sp)
        8008aab4 14 00 a2 af     sw         v0,0x14(sp)
        8008aab8 84 00 02 24     li         v0,0x84
        8008aabc 18 00 a2 af     sw         v0,0x18(sp)
        8008aac0 04 00 02 24     li         v0,0x4
        8008aac4 1c 00 a2 af     sw         v0,0x1c(sp)
        8008aac8 20 00 a0 af     sw         zero,0x20(sp)
        8008aacc 21 20 00 00     clear      a0
        8008aad0 b2 ff 05 24     li         a1,-0x4e
        8008aad4 24 00 06 24     li         a2,0x24
        8008aad8 d4 96 03 0c     jal        0x800e5b50   //RenderSpriteFromVRAM
        8008aadc 30 00 07 24     _li        a3,0x30
        8008aae0 28 00 bf 8f     lw         ra,0x28(sp)
        8008aae4 00 00 00 00     nop
        8008aae8 08 00 e0 03     jr         ra
        8008aaec 30 00 bd 27     _addiu     sp,sp,0x30
