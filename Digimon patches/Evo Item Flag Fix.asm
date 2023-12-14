// This code makes the Evo item flag restart after an evolution finishes, fixing the bug with the stats gains being disabled after using an evo item until the game is restarted

// The code shown is only the changes


Original:

void EvolutionCode(int entityPtr,short *StatsPtr,int DigimonDataPtr,int targetDigimon)
{
// code skipped
      SetDigimonAdquiredTrigger(targetDigimonTempValue & 0xffff); 

      if (currentDigimonLevel != (&DigimonLevel)[OffsetToTargetDigimonValues]) // restart the evolution timer if the digimon level is different
        DigimonEvoTimer = 0;

      return;
}

Changed:

void EvolutionCode(int entityPtr,short *StatsPtr,int DigimonDataPtr,int targetDigimon)
{
// code skipped
      SetDigimonAdquiredTrigger(targetDigimonTempValue & 0xffff); 

      if (currentDigimonLevel != (&DigimonLevel)[OffsetToTargetDigimonValues]) // restart the evolution timer if the digimon level is different
        DigimonEvoTimer = 0;

      EvoItemFlag = 0;

      return;
}



Disassembly:

	    Offset       Hex         Commands

Original:
        80063aec 02 00 43 10     beq        v0,v1,0x80063af8 // LAB_80063af8
        80063af0 00 00 00 00     _nop
        80063af4 56 00 80 a6     sh         zero,0x56(s4)

Changed:

        80063aec 02 00 43 10     beq        v0,v1,0x80063af8 // LAB_80063af8
        80063af0 26 93 80 a3     _sb        zero,DAT_ffff9326(gp) // Evo item flag
        80063af4 56 00 80 a6     sh         zero,0x56(s4)
