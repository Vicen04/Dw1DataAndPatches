//This is the function that just fills with empty spaces the individual boxes from a list of choices that you can find in some places (such as birdramon's travel or giromon's jukebox)

//This function is the cause of the famous Giromon's jukebox bug

Ptr* SetChoiceScrollListTextBox(Ptr* currentTextOffset,int CharLimit,int CharAmount)

{
  undefined *puVar1;
  int iVar2;
  
  for (iVar2 = CharLimit - (CharAmount / 2);
       iVar2 != 0; 
       iVar2 = iVar2 + -1) //If by any chance the Character amount is more than the double of the Character limit, this function will break and it will start writing 0x40 into memory until it loops back to 0 due to an overflow
  {
    *currentTextOffset = 0x81; // Writes in the first byte, the first part of a character
    puVar1 = currentTextOffset + 1;
    currentTextOffset = currentTextOffset + 2;
    *puVar1 = 0x40; // Writes in the second byte, the second part of a character, this character is represented as an empty space in the text
  }
  return currentTextOffset;
}

Disassembly:


         Offset       Hex             Commands

                   SetChoiceScrollListTextBox
        800ff51c 43 10 06 00     sra        v0,a2,0x1
        800ff520 0a 00 00 10     b          0x800ff54c
        800ff524 22 30 a2 00     _sub       a2,a1,v0
                             LAB_800ff528                                   
        800ff528 21 10 80 00     move       v0,a0
        800ff52c 81 00 03 24     li         v1,0x81
        800ff530 00 00 43 a0     sb         v1,0x0(v0)
        800ff534 01 00 44 24     addiu      a0,v0,0x1
        800ff538 21 10 80 00     move       v0,a0
        800ff53c 40 00 03 24     li         v1,0x40
        800ff540 01 00 44 24     addiu      a0,v0,0x1
        800ff544 00 00 43 a0     sb         v1,0x0(v0)
        800ff548 ff ff c6 20     addi       a2,a2,-0x1
                             LAB_800ff54c                                   
        800ff54c f6 ff c0 14     bne        a2,zero,0x800ff528
        800ff550 00 00 00 00     _nop
        800ff554 08 00 e0 03     jr         ra
        800ff558 21 10 80 00     _move      v0,a0
