//This patch does make the Tournament schedule always display the cups in the right place

Original:

void SetTournamentListText()

{
//code ignored
      if (tournamentValue == -1) //no tournament
      {
        textColour = 1;
        textValue = 8140; //Empty space, this is the cause of the error
        textPtr = textPtr + 4;

      }

// code ignored
}

Changed:

void SetTournamentListText()

{
//code ignored
      if (tournamentValue == -1) 
      {
        textColour = 0; //No colour, so it will not render
        textValue = 68; //This is a "D", the wiki explains why this fixs the error
        textPtr = textPtr + 3; 
      }

// code ignored
}


Disassembly:

        Offset       Hex         Commands

Original:

                             LAB_80080d0c                                   
        80080d0c 21 10 00 02     move       v0,s0
        80080d10 00 00 43 a0     sb         v1,0x0(v0)
        80080d14 01 00 50 24     addiu      s0,v0,0x1
        80080d18 21 10 00 02     move       v0,s0
        80080d1c 81 00 03 24     li         v1,0x81
        80080d20 00 00 43 a0     sb         v1,0x0(v0)
        80080d24 01 00 50 24     addiu      s0,v0,0x1
        80080d28 21 10 00 02     move       v0,s0
        80080d2c 40 00 03 24     li         v1,0x40
        80080d30 01 00 50 24     addiu      s0,v0,0x1
        80080d34 00 00 43 a0     sb         v1,0x0(v0)


Changed:
                             LAB_80080d0c                                      
        80080d0c 21 10 00 02     move       v0,s0
        80080d10 00 00 40 a0     sb         zero,0x0(v0)
        80080d14 01 00 50 24     addiu      s0,v0,0x1
        80080d18 21 10 00 02     move       v0,s0
        80080d1c 44 00 03 24     li         v1,0x44
        80080d20 00 00 43 a0     sb         v1,0x0(v0)
        80080d24 01 00 50 24     addiu      s0,v0,0x1
        80080d28 00 00 00 00     nop
        80080d2c 00 00 00 00     nop
        80080d30 00 00 00 00     nop
        80080d34 00 00 00 00     nop
