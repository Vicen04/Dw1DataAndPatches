// This function checks if a digimon will reject an item being feed


HandleItemRejection(void)

{
  int RandomValue;
  undefined4 returnValue;
  uint itemLevel;
  int iVar1;
  int iVar2;
  uint currentDigimonLevel;
  int DigimonType;
  uint Flag;
  uint itemFeedValue;
  
  DigimonType = DigimonTypeData;
  Flag = ConditionFlag;
  FavouriteItemRejected = 0;
  RandomValue = ReturnRandom(10);
  itemFeedValue = ItemBeingFeed;
  if ((((ItemBeingFeed < 115) || (ItemBeingFeed == 121)) || (ItemBeingFeed == 122)) ||
     (124 < (int)ItemBeingFeed?)) { // check if item  is an item that can be eaten
    if ((ItemBeingFeed? < 33) || (37 < ItemBeingFeed?)) { // Another check for items that can be eaten
      if (((70 < ItemBeingFeed) && (ItemBeingFeed < 115)) ||
         ((124 < ItemBeingFeed && (ItemBeingFeed < 128)))) { // evo items
        if (ItemBeingFeed == 126) { // panjamon evo item
          if (CurrentDigimonLevel != 3) {
            return 1;
          }
        }
        else if ((ItemBeingFeed == 125) || (ItemBeingFeed == 127)) { // gigadramon/metal etemon evo items
          if (CurrentDigimonLevel != 4) {
            return 1;
          }
        }
        else {
          currentDigimonLevel = CurrentDigimonLevel;
          itemLevel = CurrentItemLevel;
          if (((currentDigimonLevel == itemLevel) || (currentDigimonLevel + 2 == itemLevel)) ||
             (currentDigimonLevel - 1 == itemLevel)) {
            return 1;
          }
        }
      }
      if (DAT_ffff912d == 2) {
        DAT_ffff912d = 0;
        returnValue = 0;
      }
      else {
        if ((-1 < ItemBeingFeed?) && (ItemBeingFeed? < 38)) {
          iVar1 = ReturnRandom(100);
          iVar2 = ReturnRandom(10);
          if (iVar1 < 110 - (Discipline + iVar2 + 10)) {
            DAT_ffff912d = 1;
            return 1;
          }
        }
        if (((hungryFlag == 0) && (itemFeedValue == digimonFavouriteFood &&
           (RandomValue < 2)) {
          FavouriteItemRejected = 2;
        }
        returnValue = 0;
        if (FavouriteItemRejected != 0) {
          if (hungryFlag != 0) {
            DAT_ffff912d = '\x01';
          }
          returnValue = 1;
        }
      }
    }
    else {
      returnValue = 1;
    }
  }
  else {
    returnValue = 1;
  }
  return returnValue;
}

Disassembly:
        
        Offset      Hex              Commands    
                     
        800a72b0 d8 ff bd 27     addiu      sp,sp,-0x28
        800a72b4 24 00 bf af     sw         ra,0x24(sp)
        800a72b8 14 80 01 3c     lui        at,0x8014
        800a72bc 20 00 b4 af     sw         s4,0x20(sp)
        800a72c0 a4 84 20 a4     sh         zero,-0x7b5c(at)                  
        800a72c4 1c 00 b3 af     sw         s3,0x1c(sp)
        800a72c8 15 80 01 3c     lui        at,0x8015
        800a72cc a8 57 33 8c     lw         s3,0x57a8(at)                      
        800a72d0 18 00 b2 af     sw         s2,0x18(sp)
        800a72d4 14 80 01 3c     lui        at,0x8014
        800a72d8 14 00 b1 af     sw         s1,0x14(sp)
        800a72dc 60 84 34 8c     lw         s4,-0x7ba0(at)                     
        800a72e0 10 00 b0 af     sw         s0,0x10(sp)
        800a72e4 b5 8d 02 0c     jal        RandomValue(a0)                                   
        800a72e8 0a 00 04 24     _li        a0,0xa
        800a72ec 14 80 01 3c     lui        at,0x8014
        800a72f0 d8 d4 30 8c     lw         s0,-0x2b28(at)                    
        800a72f4 00 94 02 00     sll        s2,v0,0x10
        800a72f8 73 00 01 2a     slti       at,s0,0x73
        800a72fc 0c 00 20 14     bne        at,zero,LAB_800a7330
        800a7300 03 94 12 00     _sra       s2,s2,0x10
        800a7304 79 00 01 24     li         at,0x79
        800a7308 09 00 01 12     beq        s0,at,LAB_800a7330
        800a730c 00 00 00 00     _nop
        800a7310 7a 00 01 24     li         at,0x7a
        800a7314 06 00 01 12     beq        s0,at,LAB_800a7330
        800a7318 00 00 00 00     _nop
        800a731c 7d 00 01 2a     slti       at,s0,0x7d
        800a7320 03 00 20 10     beq        at,zero,LAB_800a7330
        800a7324 00 00 00 00     _nop
        800a7328 a4 00 00 10     b          LAB_800a75bc
        800a732c 01 00 02 24     _li        v0,0x1
                             LAB_800a7330                                    
        800a7330 21 00 01 2a     slti       at,s0,0x21
        800a7334 06 00 20 14     bne        at,zero,LAB_800a7350
        800a7338 00 00 00 00     _nop
        800a733c 26 00 01 2a     slti       at,s0,0x26
        800a7340 03 00 20 10     beq        at,zero,LAB_800a7350
        800a7344 00 00 00 00     _nop
        800a7348 9c 00 00 10     b          LAB_800a75bc
        800a734c 01 00 02 24     _li        v0,0x1
                             LAB_800a7350                                     
        800a7350 47 00 01 2a     slti       at,s0,0x47
        800a7354 04 00 20 14     bne        at,zero,LAB_800a7368
        800a7358 00 00 00 00     _nop
        800a735c 73 00 01 2a     slti       at,s0,0x73
        800a7360 07 00 20 14     bne        at,zero,LAB_800a7380
        800a7364 00 00 00 00     _nop
                             LAB_800a7368                                     
        800a7368 7d 00 01 2a     slti       at,s0,0x7d
        800a736c 52 00 20 14     bne        at,zero,LAB_800a74b8
        800a7370 00 00 00 00     _nop
        800a7374 80 00 01 2a     slti       at,s0,0x80
        800a7378 4f 00 20 10     beq        at,zero,LAB_800a74b8
        800a737c 00 00 00 00     _nop
                             LAB_800a7380                                      
        800a7380 7e 00 01 24     li         at,0x7e
        800a7384 12 00 01 16     bne        s0,at,LAB_800a73d0
        800a7388 00 00 00 00     _nop
        800a738c 15 80 01 3c     lui        at,0x8015
        800a7390 a8 57 23 8c     lw         v1,0x57a8(at)                      
        800a7394 00 00 00 00     nop
        800a7398 40 10 03 00     sll        v0,v1,0x1
        800a739c 20 10 43 00     add        v0,v0,v1
        800a73a0 80 10 02 00     sll        v0,v0,0x2
        800a73a4 20 10 43 00     add        v0,v0,v1
        800a73a8 80 18 02 00     sll        v1,v0,0x2
        800a73ac 13 80 02 3c     lui        v0,0x8013
        800a73b0 d1 ce 42 24     addiu      v0,v0,-0x312f
        800a73b4 21 10 43 00     addu       v0,v0,v1
        800a73b8 00 00 42 90     lbu        v0,0x0(v0)
        800a73bc 03 00 01 24     li         at,0x3
        800a73c0 3d 00 41 10     beq        v0,at,LAB_800a74b8
        800a73c4 00 00 00 00     _nop
        800a73c8 7c 00 00 10     b          LAB_800a75bc
        800a73cc 01 00 02 24     _li        v0,0x1
                             LAB_800a73d0                                     
        800a73d0 7d 00 01 24     li         at,0x7d
        800a73d4 04 00 01 12     beq        s0,at,LAB_800a73e8
        800a73d8 00 00 00 00     _nop
        800a73dc 7f 00 01 24     li         at,0x7f
        800a73e0 12 00 01 16     bne        s0,at,LAB_800a742c
        800a73e4 00 00 00 00     _nop
                             LAB_800a73e8                                      
        800a73e8 15 80 01 3c     lui        at,0x8015
        800a73ec a8 57 23 8c     lw         v1,0x57a8(at)                       
        800a73f0 00 00 00 00     nop
        800a73f4 40 10 03 00     sll        v0,v1,0x1
        800a73f8 20 10 43 00     add        v0,v0,v1
        800a73fc 80 10 02 00     sll        v0,v0,0x2
        800a7400 20 10 43 00     add        v0,v0,v1
        800a7404 80 18 02 00     sll        v1,v0,0x2
        800a7408 13 80 02 3c     lui        v0,0x8013
        800a740c d1 ce 42 24     addiu      v0,v0,-0x312f
        800a7410 21 10 43 00     addu       v0,v0,v1
        800a7414 00 00 42 90     lbu        v0,0x0(v0)
        800a7418 04 00 01 24     li         at,0x4
        800a741c 26 00 41 10     beq        v0,at,LAB_800a74b8
        800a7420 00 00 00 00     _nop
        800a7424 65 00 00 10     b          LAB_800a75bc
        800a7428 01 00 02 24     _li        v0,0x1
                             LAB_800a742c                                    
        800a742c 15 80 01 3c     lui        at,0x8015
        800a7430 a8 57 23 8c     lw         v1,0x57a8(at)                      
        800a7434 13 80 05 3c     lui        a1,0x8013
        800a7438 40 10 03 00     sll        v0,v1,0x1
        800a743c 20 10 43 00     add        v0,v0,v1
        800a7440 80 10 02 00     sll        v0,v0,0x2
        800a7444 20 10 43 00     add        v0,v0,v1
        800a7448 80 10 02 00     sll        v0,v0,0x2
        800a744c d1 ce a5 24     addiu      a1,a1,-0x312f
        800a7450 21 10 a2 00     addu       v0,a1,v0
        800a7454 00 00 44 90     lbu        a0,0x0(v0)
        800a7458 12 80 02 3c     lui        v0,0x8012
        800a745c 15 7c 42 24     addiu      v0,v0,0x7c15
        800a7460 21 10 50 00     addu       v0,v0,s0
        800a7464 00 00 43 90     lbu        v1,0x0(v0)                        
        800a7468 21 30 80 00     move       a2,a0
        800a746c 40 10 03 00     sll        v0,v1,0x1
        800a7470 20 10 43 00     add        v0,v0,v1
        800a7474 80 10 02 00     sll        v0,v0,0x2
        800a7478 20 10 43 00     add        v0,v0,v1
        800a747c 80 10 02 00     sll        v0,v0,0x2
        800a7480 21 10 a2 00     addu       v0,a1,v0
        800a7484 00 00 42 90     lbu        v0,0x0(v0)
        800a7488 00 00 00 00     nop
        800a748c 08 00 82 10     beq        a0,v0,LAB_800a74b0
        800a7490 21 18 40 00     _move      v1,v0
        800a7494 21 28 c0 00     move       a1,a2
        800a7498 02 00 c2 20     addi       v0,a2,0x2
        800a749c 04 00 43 10     beq        v0,v1,LAB_800a74b0
        800a74a0 21 20 60 00     _move      a0,v1
        800a74a4 ff ff a2 20     addi       v0,a1,-0x1
        800a74a8 03 00 44 14     bne        v0,a0,LAB_800a74b8
        800a74ac 00 00 00 00     _nop
                             LAB_800a74b0                                  
        800a74b0 42 00 00 10     b          LAB_800a75bc
        800a74b4 01 00 02 24     _li        v0,0x1
                             LAB_800a74b8                              
        800a74b8 2d 91 82 83     lb         v0,-0x6ed3(gp)
        800a74bc 02 00 01 24     li         at,0x2
        800a74c0 04 00 41 14     bne        v0,at,LAB_800a74d4
        800a74c4 00 00 00 00     _nop
        800a74c8 2d 91 80 a3     sb         zero,-0x6ed3(gp)
        800a74cc 3b 00 00 10     b          LAB_800a75bc
        800a74d0 21 10 00 00     _clear     v0
                             LAB_800a74d4                                    
        800a74d4 2a 08 00 02     slt        at,s0,zero
        800a74d8 16 00 20 14     bne        at,zero,LAB_800a7534
        800a74dc 00 00 00 00     _nop
        800a74e0 26 00 01 2a     slti       at,s0,0x26
        800a74e4 13 00 20 10     beq        at,zero,LAB_800a7534
        800a74e8 00 00 00 00     _nop
        800a74ec b5 8d 02 0c     jal        RandomValue(a0)                               
        800a74f0 64 00 04 24     _li        a0,0x64
        800a74f4 00 8c 02 00     sll        s1,v0,0x10
        800a74f8 03 8c 11 00     sra        s1,s1,0x10
        800a74fc b5 8d 02 0c     jal        RandomValue                           
        800a7500 0a 00 04 24     _li        a0,0xa
        800a7504 14 80 01 3c     lui        at,0x8014
        800a7508 88 84 23 84     lh         v1,-0x7b78(at)                    
        800a750c 0a 00 42 20     addi       v0,v0,0xa
        800a7510 20 18 62 00     add        v1,v1,v0
        800a7514 6e 00 02 24     li         v0,0x6e
        800a7518 22 10 43 00     sub        v0,v0,v1
        800a751c 2a 08 22 02     slt        at,s1,v0
        800a7520 04 00 20 10     beq        at,zero,LAB_800a7534
        800a7524 00 00 00 00     _nop
        800a7528 01 00 02 24     li         v0,0x1
        800a752c 23 00 00 10     b          LAB_800a75bc
        800a7530 2d 91 82 a3     _sb        v0,-0x6ed3(gp)
                             LAB_800a7534                    
        800a7534 04 00 82 32     andi       v0,s4,0x4
        800a7538 11 00 40 14     bne        v0,zero,LAB_800a7580
        800a753c 00 00 00 00     _nop
        800a7540 c0 10 13 00     sll        v0,s3,0x3
        800a7544 22 10 53 00     sub        v0,v0,s3
        800a7548 80 18 02 00     sll        v1,v0,0x2
        800a754c 12 80 02 3c     lui        v0,0x8012
        800a7550 cd 25 42 24     addiu      v0,v0,0x25cd
        800a7554 21 10 43 00     addu       v0,v0,v1
        800a7558 00 00 42 90     lbu        v0,0x0(v0)=>DAT_801225cd
        800a755c 00 00 00 00     nop
        800a7560 07 00 02 16     bne        s0,v0,LAB_800a7580
        800a7564 00 00 00 00     _nop
        800a7568 02 00 41 2a     slti       at,s2,0x2
        800a756c 04 00 20 10     beq        at,zero,LAB_800a7580
        800a7570 00 00 00 00     _nop
        800a7574 02 00 02 24     li         v0,0x2
        800a7578 14 80 01 3c     lui        at,0x8014
        800a757c a4 84 22 a4     sh         v0,-0x7b5c(at)                     
                             LAB_800a7580                                 
        800a7580 14 80 01 3c     lui        at,0x8014
        800a7584 a4 84 22 84     lh         v0,-0x7b5c(at)                     
        800a7588 00 00 00 00     nop
        800a758c 0b 00 40 10     beq        v0,zero,LAB_800a75bc
        800a7590 21 10 00 00     _clear     v0
        800a7594 14 80 01 3c     lui        at,0x8014
        800a7598 60 84 22 8c     lw         v0,-0x7ba0(at)                    
        800a759c 00 00 00 00     nop
        800a75a0 04 00 42 30     andi       v0,v0,0x4
        800a75a4 03 00 40 10     beq        v0,zero,LAB_800a75b4
        800a75a8 00 00 00 00     _nop
        800a75ac 01 00 02 24     li         v0,0x1
        800a75b0 2d 91 82 a3     sb         v0,-0x6ed3(gp)
                             LAB_800a75b4                                    
        800a75b4 01 00 00 10     b          LAB_800a75bc
        800a75b8 01 00 02 24     _li        v0,0x1
                             LAB_800a75bc                                   
        800a75bc 24 00 bf 8f     lw         ra,0x24(sp)
        800a75c0 20 00 b4 8f     lw         s4,0x20(sp)
        800a75c4 1c 00 b3 8f     lw         s3,0x1c(sp)
        800a75c8 18 00 b2 8f     lw         s2,0x18(sp)
        800a75cc 14 00 b1 8f     lw         s1,0x14(sp)
        800a75d0 10 00 b0 8f     lw         s0,0x10(sp)
        800a75d4 08 00 e0 03     jr         ra
        800a75d8 28 00 bd 27     _addiu     sp,sp,0x28
      