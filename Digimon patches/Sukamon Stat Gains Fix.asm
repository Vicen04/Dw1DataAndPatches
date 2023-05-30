// This is a patch that allows Sukamon to have Stat Gains when it evolves naturally
// It can still trigger the old stats calculation if by some chance you manage to evolve to Sukamon from one of its natural evolutions and then evolve naturally again to the same digimon

// The code shown is just the changed code

Original:
void EvolutionCode(int entityPtr,short *StatsPtr,int DigimonDataPtr,int targetDigimon)
{
//code skipped

  if ((targetDigimon == 11) // Numemon || (targetDigimon == 39) // Sukamon || (targetDigimon == 53)) // Nanimon ||(targetDigimon == 6) // Devimon 
  || (CurrentDigimon == 39) // current digimon is Sukamon || (targetDigimonLevel < 3) // less level than a rookie)
  {
    SpecialStatGains(StatsPtr,(&StatGainsBrainsPtr + OffsetSum));
    targetDigimonTempValue = (&DataFromStatGains)[targetDigimon * 7];
  }

//code skipped
}



Changed: 
void EvolutionCode(int entityPtr,short *StatsPtr,int DigimonDataPtr,int targetDigimon)
{
//code skipped

  if ((targetDigimon == 11) // Numemon || (targetDigimon == 39) // Sukamon  || 
  ((CurrentDigimon == 39) // current digimon is Sukamon   && (ReadPSStat(5) == targetDigimon)) // ReadPSStat(5) is used to get the digimon sukamon was before
  || (targetDigimonLevel < 3) // less level than a rookie)
  {
    SpecialStatGains(StatsPtr,(&StatGainsBrainsPtr + OffsetSum));
    targetDigimonTempValue = (&DataFromStatGains)[targetDigimon * 7];
  }

//code skipped
}


Disassembly:

        Offset       Hex         Commands
 
Original:

        80063408 10 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        8006340c 03 1c 03 00     _sra       v1,v1,0x10
        80063410 27 00 01 24     li         at,0x27
        80063414 0d 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        80063418 00 00 00 00     _nop
        8006341c 35 00 01 24     li         at,0x35
        80063420 0a 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        80063424 00 00 00 00     _nop
        80063428 06 00 01 24     li         at,0x6
        8006342c 07 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        80063430 00 00 00 00     _nop
        80063434 27 00 01 24     li         at,0x27
        80063438 04 00 41 10     beq        v0,at,0x8006344c // LAB_8006344c
        8006343c 00 00 00 00     _nop
        80063440 03 00 61 28     slti       at,v1,0x3
        80063444 0b 00 20 10     beq        at,zero,0x80063474 // LAB_80063474
        80063448 00 00 00 00     _nop


Changed:

        80063408 10 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        8006340c 03 1c 03 00     _sra       v1,v1,0x10
        80063410 27 00 01 24     li         at,0x27
        80063414 0d 00 e1 10     beq        a3,at,0x8006344c // LAB_8006344c
        80063418 00 00 00 00     _nop
        8006341c 03 00 61 28     slti       at,v1,0x3
        80063420 0a 00 20 14     bne        at,zero,0x8006344c // LAB_8006344c
        80063424 00 00 00 00     _nop
        80063428 21 08 02 00     move       at,v0
        8006342c b8 18 04 0c     jal        0x801062e0      // ReadPStat                                
        80063430 05 00 04 24     _li        a0,0x5
        80063434 0f 00 e2 14     bne        a3,v0,0x80063474 // LAB_80063474
        80063438 00 00 00 00     _nop
        8006343c 21 10 01 00     move       v0,at
        80063440 27 00 01 24     li         at,0x27
        80063444 0b 00 22 14     bne        at,v0,0x80063474  // LAB_80063474
        80063448 00 00 00 00     _nop
