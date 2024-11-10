RenderTechBrainsLearnText 
{
   if (TechtextLenght == 0) 
    TechtextLenght = TechtextLenght + 1;

  renderSpriteFromVRAM(3,-78, 24, TechtextLenght * 2 * 24, 12, 0, 120, 4, 0); //twice the original width to render everything

  renderSpriteFromVRAM(0, -0x4e, 36, 96, 12, 0, 132, 4, 0); //twice the original width to render everything, render in the next line

//removed one of the function calls since it was not necessary anymore
}


Disassembly:

 
                             RenderTechBrainsLearn  
							 
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
        8008aa20 66 98 82 93     lbu        v0,-0x679a(gp) //TechtextLenght                 
        8008aa24 00 00 00 00     nop
        8008aa28 03 00 41 04     bgez       v0,0x8008aa38
        8008aa2c 43 c8 02 00     _sra       t9,v0,0x1
        8008aa30 01 00 42 24     addiu      v0,v0,0x1
        8008aa34 43 c8 02 00     sra        t9,v0,0x1
                             LAB_8008aa38                                   
        8008aa38 40 10 19 00     sll        v0,t9,0x1
        8008aa3c 20 10 59 00     add        v0,v0,t9
        8008aa40 c0 38 02 00     sll        a3,v0,0x3
        8008aa44 03 00 04 24     li         a0,0x3
        8008aa48 b2 ff 05 24     li         a1,-0x4e
        8008aa4c d4 96 03 0c     jal        0x800e5b50  //renderSpriteFromVRAM                         
        8008aa50 18 00 06 24     _li        a2,0x18
        8008aa54 21 10 00 00     li         v0, 0xc
        8008aa58 10 00 a2 af     sw         v0,0x10(sp)
        8008aa5c 14 00 a2 af     sw         zero,0x14(sp)
        8008aa60 84 00 02 24     li         v0,0x84
        8008aa64 18 00 a2 af     sw         v0,0x18(sp)
        8008aa68 04 00 02 24     li         v0,0x4
        8008aa6c 1c 00 a2 af     sw         v0,0x1c(sp)
        8008aa70 20 00 a0 af     sw         zero,0x20(sp)
        8008aa74 21 20 00 00     clear      a0
        8008aa78 b2 ff 05 24     li         a1,-0x4e
        8008aa7c 24 00 06 24     li         a2,0x24
        8008aa80 d4 96 03 0c     jal        0x800e5b50  //renderSpriteFromVRAM                          
        8008aa84 60 00 07 24     _li        a3,0x60
        8008aa88 28 00 bf 8f     lw         ra,0x28(sp)
        8008aa8c 00 00 00 00     nop
        8008aa90 08 00 e0 03     jr         ra
        8008aa94 30 00 bd 27     _addiu     sp,sp,0x30
