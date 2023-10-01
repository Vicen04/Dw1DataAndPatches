//Fresh digimon and In-Training digimon were able to eat Evolution items, this can make the player lose them since the evolution would never trigger.
//This patch just makes them reject the item as intended.

Original:
int HandleItemRejection(void)
{
// code ignored
          if (((currentDigimonLevel == itemLevel) || 
		     (currentDigimonLevel + 2 == itemLevel)) ||
             (currentDigimonLevel - 1 == itemLevel)) //Reject the item if any of these if true 
            return 1;

//code ignored
}

Changed:
int HandleItemRejection(void)
{
// code ignored
          if (((currentDigimonLevel + 1 != itemLevel) //Reject the item if the digimon is not 1 level below the item target
            return 1;
//code ignored
}


Disassembly:

        Offset       Hex         Commands

Original:
        800a748c 08 00 82 10     beq        a0,v0,0x800a74b0
        800a7490 21 18 40 00     _move      v1,v0
        800a7494 21 28 c0 00     move       a1,a2
        800a7498 02 00 c2 20     addi       v0,a2,0x2
        800a749c 04 00 43 10     beq        v0,v1,0x800a74b0
        800a74a0 21 20 60 00     _move      a0,v1
        800a74a4 ff ff a2 20     addi       v0,a1,-0x1
        800a74a8 03 00 44 14     bne        v0,a0,0x800a74b8
        800a74ac 00 00 00 00     _nop

Changed:
        800a748c 08 00 82 10     beq        a0,v0,0x800a74b0
        800a7490 21 18 40 00     _move      v1,v0
        800a7494 21 28 c0 00     move       a1,a2
        800a7498 01 00 c2 20     addi       v0,a2,0x1
        800a749c 06 00 43 10     beq        v0,v1,0x800a74b8
        800a74a0 00 00 00 00     _nop
        800a74a4 00 00 00 00     nop
        800a74a8 00 00 00 00     nop
        800a74ac 00 00 00 00     nop
