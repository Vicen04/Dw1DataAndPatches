void SetTextBoxShop/Bank(int *param_1,int param_2,int param_3)
{
//code ignored
  if (ShoppingType == 1)
    textPtr = IntToText(textPtr,ItemValue & 0x7f,2,0);  //IntToText(textPtr*, Value, maxDigitCount, padding?), returns a pointer
  
  if (((ShoppingType != 0) && (ShoppingType != 7)) && (ShoppingType != 1)) 
  {
    *textPtr = '\x1c';
    textPtr[1] = '\0';
    textPtr = IntToText(textPtr + 2,ItemValue & 0x7f,2,0);
  }

//code ignored
}


void SetTextBoxShop/Bank(int *param_1,int param_2,int param_3)
{
//code ignored
  if ((ShoppingType == 1) || (ShoppingType == 2)) 
    textPtr = IntToText(textPtr,ItemValue & 0x7f,2,0);
  
  else if (ShoppingType != 0) && (ShoppingType != 7) 
   {  
    *textPtr = '\x1c';
    textPtr[1] = '\0';
    textPtr = IntToText(textPtr + 2,ItemValue & 0x7f,2,0);
  }
//code ignored
}




Disassembly:

                          SetTextBoxShop/Bank

Original:

                             LAB_800fe940                                   
        800fe940 e5 94 82 93     lbu        v0,-0x6b1b(gp) //ShoppingType
        800fe944 01 00 01 24     li         at,0x1
        800fe948 07 00 41 14     bne        v0,at,0x800fe968
        800fe94c 00 00 00 00     _nop
        800fe950 7f 00 45 32     andi       a1,s2,0x7f
        800fe954 21 20 00 02     move       a0,s0
        800fe958 02 00 06 24     li         a2,0x2
        800fe95c 19 08 04 0c     jal        0x80102064   //IntToText                                 
        800fe960 21 38 00 00     _clear     a3
        800fe964 21 80 40 00     move       s0,v0
                             LAB_800fe968                                 
        800fe968 e5 94 82 93     lbu        v0,-0x6b1b(gp) //ShoppingType
        800fe96c 00 00 00 00     nop
        800fe970 14 00 40 10     beq        v0,zero,0x800fe9c4
        800fe974 21 18 40 00     _move      v1,v0
        800fe978 07 00 01 24     li         at,0x7
        800fe97c 11 00 61 10     beq        v1,at,0x800fe9c4
        800fe980 00 00 00 00     _nop
        800fe984 01 00 01 24     li         at,0x1
        800fe988 0e 00 61 10     beq        v1,at,0x800fe9c4
        800fe98c 00 00 00 00     _nop




Changed:

                             LAB_800fe940                                  
        800fe940 e5 94 82 93     lbu        v0,-0x6b1b(gp) //ShoppingType
        800fe944 01 00 01 24     li         at,0x1
        800fe948 03 00 41 10     beq        v0,at,0x800fe958
        800fe94c 02 00 01 24     _li        at,0x2
        800fe950 08 00 41 14     bne        v0,at,0x800fe974
        800fe954 00 00 00 00     _nop
                             LAB_800fe958                                  
        800fe958 7f 00 45 32     andi       a1,s2,0x7f
        800fe95c 21 20 00 02     move       a0,s0
        800fe960 02 00 06 24     li         a2,0x2
        800fe964 19 08 04 0c     jal        0x80102064  //IntToText                                 
        800fe968 21 38 00 00     _clear     a3
        800fe96c 15 00 00 10     b          0x800fe9c4
        800fe970 21 80 02 00     _move      s0,v0
                             LAB_800fe974                                  
        800fe974 e5 94 82 93     lbu        v0,-0x6b1b(gp) //ShoppingType
        800fe978 00 00 00 00     nop
        800fe97c 11 00 40 10     beq        v0,zero,0x800fe9c4
        800fe980 21 18 40 00     _move      v1,v0
        800fe984 07 00 01 24     li         at,0x7
        800fe988 0e 00 61 10     beq        v1,at,0x800fe9c4
        800fe98c 00 00 00 00     _nop


