void CheckLimits() // Used to make sure the stats don't go past the limit after the stats gains are applied
{
  if (9999 < MaxDigimonHP) {
    MaxDigimonHP = 9999;
  }
  if (9999 < MaxDigimonMP) {
    MaxDigimonMP = 9999;
  }
  if (999 < DigimonOff) {
    DigimonOff = 999;
  }
  if (999 < DigimonDef) {
    DigimonDef = 999;
  }
  if (999 < DigimonSpeed) {
    DigimonSpeed = 999;
  }
  if (999 < DigimonBrains) {
    DigimonBrains = 999;
  }
  return;
}

Disassembly:

        Offset       Hex         Commands
                            
        80063e30 15 80 03 3c     lui        v1,0x8015
        80063e34 e0 57 63 24     addiu      v1,v1,0x57e0
        80063e38 10 00 62 84     lh         v0,0x10(v1) // MaxDigimonHP               
        80063e3c 00 00 00 00     nop
        80063e40 10 27 41 28     slti       at,v0,0x2710
        80063e44 03 00 20 14     bne        at,zero,0x80063e54 //LAB_80063e54
        80063e48 00 00 00 00     _nop
        80063e4c 0f 27 02 24     li         v0,0x270f
        80063e50 10 00 62 a4     sh         v0,0x10(v1) // MaxDigimonHP                 
                             LAB_80063e54                                    
        80063e54 12 00 62 84     lh         v0,0x12(v1) // MaxDigimonMP      
        80063e58 00 00 00 00     nop
        80063e5c 10 27 41 28     slti       at,v0,0x2710
        80063e60 03 00 20 14     bne        at,zero,0x80063e70 // LAB_80063e70
        80063e64 00 00 00 00     _nop
        80063e68 0f 27 02 24     li         v0,0x270f
        80063e6c 12 00 62 a4     sh         v0,0x12(v1) // MaxDigimonMP                      
                             LAB_80063e70                                  
        80063e70 00 00 62 84     lh         v0,0x0(v1) // DigimonOff            
        80063e74 00 00 00 00     nop
        80063e78 e8 03 41 28     slti       at,v0,0x3e8
        80063e7c 03 00 20 14     bne        at,zero,0x80063e8c // LAB_80063e8c
        80063e80 00 00 00 00     _nop
        80063e84 e7 03 02 24     li         v0,0x3e7
        80063e88 00 00 62 a4     sh         v0,0x0(v1) // DigimonOff   
                             LAB_80063e8c                                   
        80063e8c 02 00 62 84     lh         v0,0x2(v1) // DigimonDef        
        80063e90 00 00 00 00     nop
        80063e94 e8 03 41 28     slti       at,v0,0x3e8
        80063e98 03 00 20 14     bne        at,zero,0x80063ea8 // LAB_80063ea8
        80063e9c 00 00 00 00     _nop
        80063ea0 e7 03 02 24     li         v0,0x3e7
        80063ea4 02 00 62 a4     sh         v0,0x2(v1) // DigimonDef       
                             LAB_80063ea8                            
        80063ea8 04 00 62 84     lh         v0,0x4(v1) // DigimonSpeed    
        80063eac 00 00 00 00     nop
        80063eb0 e8 03 41 28     slti       at,v0,0x3e8
        80063eb4 03 00 20 14     bne        at,zero,0x80063ec4 // LAB_80063ec4
        80063eb8 00 00 00 00     _nop
        80063ebc e7 03 02 24     li         v0,0x3e7
        80063ec0 04 00 62 a4     sh         v0,0x4(v1) // DigimonSpeed    
                             LAB_80063ec4                                 
        80063ec4 06 00 62 84     lh         v0,0x6(v1) // DigimonBrains     
        80063ec8 00 00 00 00     nop
        80063ecc e8 03 41 28     slti       at,v0,0x3e8
        80063ed0 03 00 20 14     bne        at,zero,0x80063ee0 // LAB_80063ee0
        80063ed4 00 00 00 00     _nop
        80063ed8 e7 03 02 24     li         v0,0x3e7
        80063edc 06 00 62 a4     sh         v0,0x6(v1) // DigimonBrains 
                             LAB_80063ee0                        
        80063ee0 08 00 e0 03     jr         ra
        80063ee4 00 00 00 00     _nop
