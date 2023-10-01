//This is a small change that fixes the sleep regeneration overflow when a digimon sleeps at the same time it is supposed to wake up and it has more than 9000 health

Original:
void SleepRegen(void)
{
CurrentHour = (int)_CurrentHourMemory;
  if (CurrentHour < WakeUpHour) { //This does not take into account if the time is 0, so if by any chance that happens, it will go to the other option, making it 24 hours of sleep
    HoursSleep = WakeUpHour - CurrentHour;
  }
  else {
    HoursSleep = 24 - CurrentHour;
  }
//code ignored
}

Changed:
void SleepRegen(void)
{
CurrentHour = (int)_CurrentHourMemory;
  if (CurrentHour - WakeUpHour < 1) { //This does take into account if the time is 0, so it will stay in this option
    HoursSleep = WakeUpHour - CurrentHour;
  }
  else {
    HoursSleep = 24 - CurrentHour;
  }
//code ignored
}

Disassembly:

        Offset       Hex         Commands

Original:
        800a6d30 2a 08 62 00     slt        at,v1,v0  // v1 = CurrentHour; v0 = WakeUpHour
        800a6d34 05 00 20 10     beq        at,zero,0x800a6d4c

Changed:
        800a6d30 22 08 62 00     sub        at,v1,v0 // v1 = CurrentHour; v0 = WakeUpHour
        800a6d34 05 00 20 1c     bgtz       at,0x800a6d4c



