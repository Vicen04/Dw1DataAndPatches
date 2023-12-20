//Function to select the text Penguimon will use and to render it


int LoadPenguinmonText(int currentTextLocation,uint currentTextValue) // currentTextLocation is used to determine which text should be loaded, currentTextValue is used to determine the line where it should be loaded
{
  char cVar1;
  uint uVar2;
  vector<int> textAddresses;
  vector<byte> textLocations;
  ptr pointerText;
  ptr pointerLoaction;
  ptr extraText1;
  ptr extraText2;
  int iVar3;
  
  pointerText = &ptr_penguinmonText; // It is located at 8005d3cc in the code
  textAdresses = new vector<int>;
  
  for (int i = 0; i < 61; i++)
  {
   textAdresses.push_back(*pointerText); // value in that address
   pointerLocation = pointerText + 4;   // gets the next pointer
  }

  pointerLocation = &ptr_penguinmonLocations; // It is located at 8005d4dc in the code

  extraText1 = &CircleText1; //This is just to render a Circle image
  extraText2 = &CircleTextPenguinmon; //This is just to render a Circle image but with a different displacement
  
  textLocations = new vector<byte>;
  
  for (int i = 0; i < 10; i++)
  {
   textLocations.push_back(*pointerLocation); // value in that address
   pointerLocation = pointerLocation + 1;   // gets the next pointer
  }
 
  uVar2 = currentTextValue % 3;
  
  if (((int)currentTextValue < 0) && (currentTextValue != 0)) 
    uVar2 = uVar2 - 4;
  
  if (uVar2 == 0) 
    SetTextColor(7); //SetTextColor(byte color), this is yellow
  
  else 
    SetTextColor(1); //this is white
  
  cVar1 = textLocations(currentTextLocation);
  RenderString(textAdresses[(int)cVar1 + currentTextValue], 0, currentTextValue * 13 + 1); // RenderString(stringPtr, initialXPos, yPos)

  if ((currentTextLocation == 1) && (currentTextValue == 7)) // This is just to render the circle in red
  {
    SetTextColor(10);
    RenderString(extraText1,0,0x5c);
  }
  
  if ((currentTextLocation == 2) && (currentTextValue == 3)) // circle with a red color like before
  {
    SetTextColor(10);
    RenderString(local_10,0,0x28);
  }
  
  iVar3 = (int)textLocations(currentTextLocation + 1) - (int)cVar1;
  
  if (iVar3 < 0) {
    iVar3 = iVar3 + 3;
  }
  return (iVar3 / 4);
}  



Disassembly:

 
                             LoadPenguinmonText                             
        8005bd74 d8 fe bd 27     addiu      sp,sp,-0x128
        8005bd78 18 00 bf af     sw         ra,0x18(sp)
        8005bd7c 14 00 b1 af     sw         s1,0x14(sp)
        8005bd80 10 00 b0 af     sw         s0,0x10(sp)
        8005bd84 21 88 80 00     move       s1,a0
        8005bd88 21 80 a0 00     move       s0,a1
        8005bd8c 06 80 18 3c     lui        t8,0x8006
        8005bd90 cc d3 18 27     addiu      t8,t8,-0x2c34 //PenguinmonTextPtr        
        8005bd94 20 00 af 27     addiu      t7,sp,0x20
        8005bd98 3d 00 19 24     li         t9,0x3d
                             LAB_8005bd9c                                   
        8005bd9c 00 00 0e 8f     lw         t6,0x0(t8)            
                                                                                            
        8005bda0 04 00 18 27     addiu      t8,t8,0x4
        8005bda4 ff ff 39 27     addiu      t9,t9,-0x1
        8005bda8 00 00 ee ad     sw         t6,0x0(t7)             
                                                                                            
        8005bdac 04 00 ef 25     addiu      t7,t7,0x4
        8005bdb0 fa ff 20 1f     bgtz       t9,0x8005bd9c
        8005bdb4 00 00 00 00     _nop
        8005bdb8 44 8e 82 27     addiu      v0,gp,-0x71bc  
        8005bdbc 00 00 43 8c     lw         v1,0x0(v0)  //CircleText1
        8005bdc0 04 00 42 8c     lw         v0,0x4(v0)  //CircleTextPenguinmon
        8005bdc4 14 01 a3 af     sw         v1,0x114(sp) //extraText1
        8005bdc8 18 01 a2 af     sw         v0,0x118(sp) //extraText2
        8005bdcc 06 80 18 3c     lui        t8,0x8006
        8005bdd0 dc d4 18 27     addiu      t8,t8,-0x2b24 //PenguimonTextOffsets     
        8005bdd4 1c 01 af 27     addiu      t7,sp,0x11c
        8005bdd8 0a 00 19 24     li         t9,0xa
                             LAB_8005bddc                                     
        8005bddc 00 00 0e 83     lb         t6,0x0(t8)                   
        8005bde0 01 00 18 27     addiu      t8,t8,0x1
        8005bde4 ff ff 39 27     addiu      t9,t9,-0x1
        8005bde8 00 00 ee a1     sb         t6,0x0(t7)
        8005bdec 01 00 ef 25     addiu      t7,t7,0x1
        8005bdf0 fa ff 20 1f     bgtz       t9,0x8005bddc
        8005bdf4 00 00 00 00     _nop
        8005bdf8 03 00 02 32     andi       v0,s0,0x3
        8005bdfc 04 00 01 06     bgez       s0,0x8005be10
        8005be00 00 00 00 00     _nop
        8005be04 02 00 40 10     beq        v0,zero,0x8005be10
        8005be08 00 00 00 00     _nop
        8005be0c fc ff 42 24     addiu      v0,v0,-0x4
                             LAB_8005be10                                    
        8005be10 06 00 40 14     bne        v0,zero,0x8005be2c
        8005be14 00 00 00 00     _nop
        8005be18 07 00 04 24     li         a0,0x7
        8005be1c ca 95 02 0c     jal        0x800a5728 //SetTextColor                                     
        8005be20 00 00 00 00     _nop
        8005be24 04 00 00 10     b          0x8005be38
        8005be28 00 00 00 00     _nop
                             LAB_8005be2c                                     
        8005be2c 01 00 04 24     li         a0,0x1
        8005be30 ca 95 02 0c     jal        0x800a5728 //SetTextColor                                     
        8005be34 00 00 00 00     _nop
                             LAB_8005be38                                   
        8005be38 21 10 b1 03     addu       v0,sp,s1
        8005be3c 1c 01 42 80     lb         v0,0x11c(v0)
        8005be40 00 00 00 00     nop
        8005be44 20 10 50 00     add        v0,v0,s0
        8005be48 80 10 02 00     sll        v0,v0,0x2
        8005be4c 21 18 a2 03     addu       v1,sp,v0
        8005be50 40 10 10 00     sll        v0,s0,0x1
        8005be54 20 10 50 00     add        v0,v0,s0
        8005be58 80 10 02 00     sll        v0,v0,0x2
        8005be5c 20 10 50 00     add        v0,v0,s0
        8005be60 01 00 46 20     addi       a2,v0,0x1
        8005be64 20 00 64 8c     lw         a0,0x20(v1)
        8005be68 21 28 00 00     clear      a1
        8005be6c d2 96 02 0c     jal        0x800a5b48 //RenderString                                   
        8005be70 00 00 00 00     _nop
        8005be74 01 00 01 24     li         at,0x1
        8005be78 10 00 21 16     bne        s1,at,LAB_8005bebc
        8005be7c 00 00 00 00     _nop
        8005be80 07 00 01 24     li         at,0x7
        8005be84 0d 00 01 16     bne        s0,at,LAB_8005bebc
        8005be88 00 00 00 00     _nop
        8005be8c 0a 00 04 24     li         a0,0xa
        8005be90 ca 95 02 0c     jal        0x800a5728 //SetTextColor                                    
        8005be94 00 00 00 00     _nop
        8005be98 40 10 10 00     sll        v0,s0,0x1
        8005be9c 20 10 50 00     add        v0,v0,s0
        8005bea0 80 10 02 00     sll        v0,v0,0x2
        8005bea4 20 10 50 00     add        v0,v0,s0
        8005bea8 01 00 46 20     addi       a2,v0,0x1
        8005beac 14 01 a4 8f     lw         a0,0x114(sp) //extraText1
        8005beb0 21 28 00 00     clear      a1
        8005beb4 d2 96 02 0c     jal        0x800a5b48 //RenderString                                   
        8005beb8 00 00 00 00     _nop
                             LAB_8005bebc                                     
        8005bebc 02 00 01 24     li         at,0x2
        8005bec0 10 00 21 16     bne        s1,at,0x8005bf04
        8005bec4 00 00 00 00     _nop
        8005bec8 03 00 01 24     li         at,0x3
        8005becc 0d 00 01 16     bne        s0,at,0x8005bf04
        8005bed0 00 00 00 00     _nop
        8005bed4 0a 00 04 24     li         a0,0xa
        8005bed8 ca 95 02 0c     jal        0x800a5728 //SetTextColor                                     
        8005bedc 00 00 00 00     _nop
        8005bee0 40 10 10 00     sll        v0,s0,0x1
        8005bee4 20 10 50 00     add        v0,v0,s0
        8005bee8 80 10 02 00     sll        v0,v0,0x2
        8005beec 20 10 50 00     add        v0,v0,s0
        8005bef0 01 00 46 20     addi       a2,v0,0x1
        8005bef4 18 01 a4 8f     lw         a0,0x118(sp) //extraText2
        8005bef8 21 28 00 00     clear      a1
        8005befc d2 96 02 0c     jal        0x800a5b48 //RenderString                                  
        8005bf00 00 00 00 00     _nop
                             LAB_8005bf04                                    
        8005bf04 01 00 22 22     addi       v0,s1,0x1
        8005bf08 21 10 a2 03     addu       v0,sp,v0
        8005bf0c 1c 01 43 80     lb         v1,0x11c(v0)
        8005bf10 21 10 b1 03     addu       v0,sp,s1
        8005bf14 1c 01 42 80     lb         v0,0x11c(v0)
        8005bf18 00 00 00 00     nop
        8005bf1c 22 10 62 00     sub        v0,v1,v0
        8005bf20 83 c8 02 00     sra        t9,v0,0x2
        8005bf24 03 00 41 04     bgez       v0,0x8005bf34
        8005bf28 00 00 00 00     _nop
        8005bf2c 03 00 42 24     addiu      v0,v0,0x3
        8005bf30 83 c8 02 00     sra        t9,v0,0x2
                             LAB_8005bf34                                 
        8005bf34 00 16 19 00     sll        v0,t9,0x18
        8005bf38 03 16 02 00     sra        v0,v0,0x18
        8005bf3c 18 00 bf 8f     lw         ra,0x18(sp)
        8005bf40 14 00 b1 8f     lw         s1,0x14(sp)
        8005bf44 10 00 b0 8f     lw         s0,0x10(sp)
        8005bf48 28 01 bd 27     addiu      sp,sp,0x128
        8005bf4c 08 00 e0 03     jr         ra
        8005bf50 00 00 00 00     _nop
