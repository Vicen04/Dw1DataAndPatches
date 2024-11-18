//makes feeding your digimon something else not override the digipine, hawk radish and supercarrot training boost

void SetTrainingBoost(short trainingBoostFlag,short trainingBoostValue,short trainingBoostTime)
{  
  if (trainingBoostFlag != 0) 
 {
    TrainingBoostTimer = trainingBoostTime * 1200;
    TrainingBoostFlag = trainingBoostFlag;
    TrainingBoostValue = trainingBoostValue;
  }
  return;
}


Disassembly:

                             SetTrainingBoost                               
        800c59b0 09 00 80 10     beq        a0,zero,LAB_800c59d8
        800c59b4 14 80 01 3c     _lui       at,0x8014
        800c59b8 00 11 06 00     sll        v0,a2,0x4
        800c59bc ac 84 24 a4     sh         a0,-0x7b54(at)//TrainingBoostFlag 
        800c59c0 22 18 46 00     sub        v1,v0,a2
        800c59c4 80 10 03 00     sll        v0,v1,0x2
        800c59c8 ae 84 25 a4     sh         a1,-0x7b52(at)//TrainingBoostValue
        800c59cc 20 10 62 00     add        v0,v1,v0
        800c59d0 00 11 02 00     sll        v0,v0,0x4
        800c59d4 b0 84 22 a4     sh         v0,-0x7b50(at)//TrainingBoostTimer
                             LAB_800c59d8                                  
        800c59d8 08 00 e0 03     jr         ra
        800c59dc 00 00 00 00     _nop
