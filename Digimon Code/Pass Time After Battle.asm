//This is the function in charge to handle the events related to time after a battle finishes

void PassTimeAfterBattle(int BattleResult)

{
  short sVar1;
  
  _CurrentFrame = _CurrentFrame + 400;
  sVar1 = _CurrentMinute + 20;
  if (59 < sVar1) //Check if an hour has passed
  {
    //Due to an oversight, the evolution timer does not count this hour
    sVar1  = _CurrentMinute - 40;
    _CurrentHour = _CurrentHour + 1;
    Lifetime = Lifetime - 1;
    if (Lifetime < 0) 
      Lifetime = 0;
    
    if ((ConditionFlag & 1) != 0) //sleepy
    {
      ImsomniaCounter = ImsomniaCounter + 1;
      MissedSleepHours = MissedSleepHours + 1;
    }

    if ((ConditionFlag & 0x40) != 0) //sick
      SicknessTries = SicknessTries + 1;
    
    if ((ConditionFlag & 0x20) != 0) //injured
      InjuredTimer = InjuredTimer + 1;
    
    if ((ConditionFlag & 0x40) != 0) //sick
      SicknessTimer = SicknessTimer + 1;
    
    sVar1 = _CurrentMinute;

    if (0x17 < _CurrentHour) //check if a day has passed
    {
      //Due to an oversight, the digimon will not add a day to its age
      //Due to an oversight, the daily events are not reset
      _CurrentHour = 0;
      _CurrentFrame = _CurrentMinute * 20;
      _currentDay = _currentDay + 1;
      sVar1 = _CurrentMinute;
      if (29 < _currentDay) //check if a month has passed
      {
        month = month + 1;
        _currentDay = 0;
        if (99 < month) // check if the month count should be reset
          month = 0;
        
      }
    }
  }
  _CurrentMinute = sVar1;

  if ((ConditionFlag & 4) == 0) //Is not hungry
    StarvationTimer = StarvationTimer - 20;
  else 
  {
    HungerTimer = HungerTimer - 40;
    if ((HungerTimer < 1) && (EnergyLevel < (&EnergyTresholdPtr)[DigimonType * 28])) //It was hungry, the time to feed has run out and it has less energy than it should
      CareMistake = CareMistake + 1;    
  }

  if ((ConditionFlag & 8) == 0) //Does not need the toilet
    PoopTimerBubble = PoopTimerBubble + -2;  //This is decreased by 1 every 10 minutes, since the battle takes 20 minutes, it decreases it by 2
  else 
    PoopTimerToilet = PoopTimerToilet + -400;
  
  UpdateMinuteHand(_CurrentHour, _CurrentMinute); //Update the clock

  if ((BattleResult == 1) && (((&DAT_801292e0)[(uint)DAT_ffff927c * 16] & 0x40) == 0)) //Set the Sunlight depending on the current hour if necessary and only if you won the battle
  {
    if (_CurrentHour == 16) 
      SetSunlight(0);
    
    else if (_CurrentHour == 20) 
      SetSunlight(1);
    
    else if (_CurrentHour == 6) 
      SetSunlight(2);    
  }
  return;
}

Disassembly:

         Offset      Hex              Commands    

        800abccc 92 93 82 87     lh         v0,-0x6c6e(gp) //Current minute
        800abcd0 e8 ff bd 27     addiu      sp,sp,-0x18
        800abcd4 14 00 42 20     addi       v0,v0,0x14
        800abcd8 92 93 82 a7     sh         v0,-0x6c6e(gp) //Current minute
        800abcdc dc 93 82 97     lhu        v0,-0x6c24(gp) //Current frame
        800abce0 14 00 bf af     sw         ra,0x14(sp)
        800abce4 92 93 83 87     lh         v1,-0x6c6e(gp) //Current minute
        800abce8 10 00 b0 af     sw         s0,0x10(sp)
        800abcec 90 01 42 24     addiu      v0,v0,0x190
        800abcf0 21 80 80 00     move       s0,a0
        800abcf4 3c 00 61 28     slti       at,v1,0x3c
        800abcf8 65 00 20 14     bne        at,zero,0x800abe90
        800abcfc dc 93 82 a7     _sh        v0,-0x6c24(gp) //Current frame
        800abd00 90 93 82 87     lh         v0,-0x6c70(gp) //Current hour
        800abd04 14 80 01 3c     lui        at,0x8014
        800abd08 01 00 42 20     addi       v0,v0,0x1
        800abd0c 90 93 82 a7     sh         v0,-0x6c70(gp) //Current hour
        800abd10 c4 ff 62 20     addi       v0,v1,-0x3c
        800abd14 92 93 82 a7     sh         v0,-0x6c6e(gp) //Current minute
        800abd18 a8 84 22 84     lh         v0,-0x7b58(at)//Lifetime                   
        800abd1c 00 00 00 00     nop
        800abd20 ff ff 42 20     addi       v0,v0,-0x1
        800abd24 14 80 01 3c     lui        at,0x8014
        800abd28 a8 84 22 a4     sh         v0,-0x7b58(at)//Lifetime                    
        800abd2c 14 80 01 3c     lui        at,0x8014
        800abd30 a8 84 22 84     lh         v0,-0x7b58(at)//Lifetime                    
        800abd34 00 00 00 00     nop
        800abd38 03 00 41 04     bgez       v0,0x800abd48
        800abd3c 00 00 00 00     _nop
        800abd40 14 80 01 3c     lui        at,0x8014
        800abd44 a8 84 20 a4     sh         zero,-0x7b58(at)//Lifetime                   
                             LAB_800abd48                                  
        800abd48 14 80 01 3c     lui        at,0x8014
        800abd4c 60 84 22 8c     lw         v0,-0x7ba0(at)//ConditionFlag                   
        800abd50 00 00 00 00     nop
        800abd54 01 00 42 30     andi       v0,v0,0x1
        800abd58 0d 00 40 10     beq        v0,zero,0x800abd90
        800abd5c 00 00 00 00     _nop
        800abd60 14 80 01 3c     lui        at,0x8014
        800abd64 72 84 22 84     lh         v0,-0x7b8e(at)//Imsomnia counter                    
        800abd68 00 00 00 00     nop
        800abd6c 01 00 42 20     addi       v0,v0,0x1
        800abd70 14 80 01 3c     lui        at,0x8014
        800abd74 72 84 22 a4     sh         v0,-0x7b8e(at)//Imsomnia counter                   
        800abd78 14 80 01 3c     lui        at,0x8014
        800abd7c 74 84 22 84     lh         v0,-0x7b8c(at)//Missed sleep hours                   
        800abd80 00 00 00 00     nop
        800abd84 01 00 42 20     addi       v0,v0,0x1
        800abd88 14 80 01 3c     lui        at,0x8014
        800abd8c 74 84 22 a4     sh         v0,-0x7b8c(at)//Missed sleep hours                   
                             LAB_800abd90                                    
        800abd90 14 80 01 3c     lui        at,0x8014
        800abd94 60 84 22 8c     lw         v0,-0x7ba0(at)//ConditionFlag                   
        800abd98 00 00 00 00     nop
        800abd9c 40 00 42 30     andi       v0,v0,0x40
        800abda0 07 00 40 10     beq        v0,zero,0x800abdc0
        800abda4 00 00 00 00     _nop
        800abda8 14 80 01 3c     lui        at,0x8014
        800abdac 92 84 22 84     lh         v0,-0x7b6e(at)//Sickness tries                  
        800abdb0 00 00 00 00     nop
        800abdb4 01 00 42 20     addi       v0,v0,0x1
        800abdb8 14 80 01 3c     lui        at,0x8014
        800abdbc 92 84 22 a4     sh         v0,-0x7b6e(at)//Sickness tries                   
                             LAB_800abdc0                                  
        800abdc0 14 80 01 3c     lui        at,0x8014
        800abdc4 60 84 22 8c     lw         v0,-0x7ba0(at)//ConditionFlag                   
        800abdc8 00 00 00 00     nop
        800abdcc 20 00 42 30     andi       v0,v0,0x20
        800abdd0 07 00 40 10     beq        v0,zero,0x800abdf0
        800abdd4 00 00 00 00     _nop
        800abdd8 14 80 01 3c     lui        at,0x8014
        800abddc 98 84 22 84     lh         v0,-0x7b68(at)//Injured timer                 
        800abde0 00 00 00 00     nop
        800abde4 01 00 42 20     addi       v0,v0,0x1
        800abde8 14 80 01 3c     lui        at,0x8014
        800abdec 98 84 22 a4     sh         v0,-0x7b68(at)//Injured timer                
                             LAB_800abdf0                                  
        800abdf0 14 80 01 3c     lui        at,0x8014
        800abdf4 60 84 22 8c     lw         v0,-0x7ba0(at)//ConditionFlag                  
        800abdf8 00 00 00 00     nop
        800abdfc 40 00 42 30     andi       v0,v0,0x40
        800abe00 07 00 40 10     beq        v0,zero,0x800abe20
        800abe04 00 00 00 00     _nop
        800abe08 14 80 01 3c     lui        at,0x8014
        800abe0c 96 84 22 84     lh         v0,-0x7b6a(at)//Sickness timer                 
        800abe10 00 00 00 00     nop
        800abe14 01 00 42 20     addi       v0,v0,0x1
        800abe18 14 80 01 3c     lui        at,0x8014
        800abe1c 96 84 22 a4     sh         v0,-0x7b6a(at)//Sickness timer                 
                             LAB_800abe20                                  
        800abe20 90 93 82 87     lh         v0,-0x6c70(gp) //Current day
        800abe24 00 00 00 00     nop
        800abe28 18 00 41 28     slti       at,v0,0x18
        800abe2c 18 00 20 14     bne        at,zero,0x800abe90
        800abe30 00 00 00 00     _nop
        800abe34 d8 93 82 87     lh         v0,-0x6c28(gp) //Current day
        800abe38 92 93 83 87     lh         v1,-0x6c6e(gp) //Current minute
        800abe3c 01 00 42 20     addi       v0,v0,0x1
        800abe40 d8 93 82 a7     sh         v0,-0x6c28(gp) //Current day
        800abe44 80 10 03 00     sll        v0,v1,0x2
        800abe48 20 10 43 00     add        v0,v0,v1
        800abe4c 80 10 02 00     sll        v0,v0,0x2
        800abe50 dc 93 82 a7     sh         v0,-0x6c24(gp) //Current frame
        800abe54 d8 93 82 87     lh         v0,-0x6c28(gp) //Current day
        800abe58 00 00 00 00     nop
        800abe5c 1e 00 41 28     slti       at,v0,0x1e
        800abe60 0b 00 20 14     bne        at,zero,0x800abe90
        800abe64 90 93 80 a7     _sh        zero,-0x6c70(gp) //Current day
        800abe68 d6 93 82 93     lbu        v0,-0x6c2a(gp) //Current month
        800abe6c 00 00 00 00     nop
        800abe70 01 00 42 24     addiu      v0,v0,0x1
        800abe74 d6 93 82 a3     sb         v0,-0x6c2a(gp)//Current month
        800abe78 d6 93 82 93     lbu        v0,-0x6c2a(gp)//Current month
        800abe7c 00 00 00 00     nop
        800abe80 64 00 41 2c     sltiu      at,v0,0x64
        800abe84 02 00 20 14     bne        at,zero,0x800abe90
        800abe88 d8 93 80 a7     _sh        zero,-0x6c28(gp) //Current day
        800abe8c d6 93 80 a3     sb         zero,-0x6c2a(gp) //Current month
                                 LAB_800abe90                                 
        800abe90 14 80 01 3c     lui        at,0x8014
        800abe94 60 84 22 8c     lw         v0,-0x7ba0(at)//ConditionFlag        
        800abe98 00 00 00 00     nop
        800abe9c 04 00 42 30     andi       v0,v0,0x4
        800abea0 23 00 40 10     beq        v0,zero,0x800abf30
        800abea4 00 00 00 00     _nop
        800abea8 14 80 01 3c     lui        at,0x8014
        800abeac a0 84 22 84     lh         v0,-0x7b60(at)//HungerTimer         
        800abeb0 00 00 00 00     nop
        800abeb4 d8 ff 42 20     addi       v0,v0,-0x28
        800abeb8 14 80 01 3c     lui        at,0x8014
        800abebc a0 84 22 a4     sh         v0,-0x7b60(at)//HungerTimer         
        800abec0 14 80 01 3c     lui        at,0x8014
        800abec4 a0 84 22 84     lh         v0,-0x7b60(at)//HungerTimer        
        800abec8 00 00 00 00     nop
        800abecc 1e 00 40 1c     bgtz       v0,0x800abf48
        800abed0 00 00 00 00     _nop
        800abed4 14 80 01 3c     lui        at,0x8014
        800abed8 9c 84 24 84     lh         a0,-0x7b64(at)//EnergyLevel       
        800abedc 15 80 01 3c     lui        at,0x8015
        800abee0 a8 57 23 8c     lw         v1,0x57a8(at) //Digimon type                      
        800abee4 00 00 00 00     nop
        800abee8 c0 10 03 00     sll        v0,v1,0x3
        800abeec 22 10 43 00     sub        v0,v0,v1
        800abef0 80 18 02 00     sll        v1,v0,0x2
        800abef4 12 80 02 3c     lui        v0,0x8012
        800abef8 c5 25 42 24     addiu      v0,v0,0x25c5
        800abefc 21 10 43 00     addu       v0,v0,v1
        800abf00 00 00 42 90     lbu        v0,0x0(v0)//EnergyTresholdPtr
        800abf04 00 00 00 00     nop
        800abf08 2a 08 82 00     slt        at,a0,v0
        800abf0c 0e 00 20 10     beq        at,zero,0x800abf48
        800abf10 00 00 00 00     _nop
        800abf14 14 80 01 3c     lui        at,0x8014
        800abf18 b2 84 22 84     lh         v0,-0x7b4e(at)//CareMistake       
        800abf1c 00 00 00 00     nop
        800abf20 01 00 42 20     addi       v0,v0,0x1
        800abf24 14 80 01 3c     lui        at,0x8014
        800abf28 07 00 00 10     b          0x800abf48
        800abf2c b2 84 22 a4     _sh        v0,-0x7b4e(at)//CareMistake          
                             LAB_800abf30                                   
        800abf30 14 80 01 3c     lui        at,0x8014
        800abf34 9e 84 22 84     lh         v0,-0x7b62(at)//StarvationTimer      
        800abf38 00 00 00 00     nop
        800abf3c ec ff 42 20     addi       v0,v0,-0x14
        800abf40 14 80 01 3c     lui        at,0x8014
        800abf44 9e 84 22 a4     sh         v0,-0x7b62(at)//StarvationTimer      
                             LAB_800abf48                                 
        800abf48 14 80 01 3c     lui        at,0x8014
        800abf4c 60 84 22 8c     lw         v0,-0x7ba0(at)//ConditionFlag       
        800abf50 00 00 00 00     nop
        800abf54 08 00 42 30     andi       v0,v0,0x8
        800abf58 08 00 40 10     beq        v0,zero,0x800abf7c
        800abf5c 00 00 00 00     _nop
        800abf60 14 80 01 3c     lui        at,0x8014
        800abf64 80 84 22 84     lh         v0,-0x7b80(at)//PoopTimerToilet      
        800abf68 00 00 00 00     nop
        800abf6c 70 fe 42 20     addi       v0,v0,-0x190
        800abf70 14 80 01 3c     lui        at,0x8014
        800abf74 07 00 00 10     b          0x800abf94
        800abf78 80 84 22 a4     _sh        v0,-0x7b80(at)//PoopTimerToilet      
                             LAB_800abf7c                                  
        800abf7c 14 80 01 3c     lui        at,0x8014
        800abf80 78 84 22 84     lh         v0,-0x7b88(at)//PoopTimerBubble     
        800abf84 00 00 00 00     nop
        800abf88 fe ff 42 20     addi       v0,v0,-0x2
        800abf8c 14 80 01 3c     lui        at,0x8014
        800abf90 78 84 22 a4     sh         v0,-0x7b88(at)//PoopTimerBubble     
                             LAB_800abf94                                 
        800abf94 90 93 84 87     lh         a0,-0x6c70(gp) //Current hour
        800abf98 92 93 85 87     lh         a1,-0x6c6e(gp) //Current minute
        800abf9c e0 75 03 0c     jal        0x800dd780 //UpdateMinuteHand                             
        800abfa0 00 00 00 00     _nop
        800abfa4 01 00 01 24     li         at,0x1
        800abfa8 22 00 01 16     bne        s0,at,0x800ac034
        800abfac 00 00 00 00     _nop
        800abfb0 7c 92 82 93     lbu        v0,-0x6d84(gp) //DAT_ffff927c
        800abfb4 00 00 00 00     nop
        800abfb8 00 19 02 00     sll        v1,v0,0x4
        800abfbc 13 80 02 3c     lui        v0,0x8013
        800abfc0 e0 92 42 24     addiu      v0,v0,-0x6d20
        800abfc4 21 10 43 00     addu       v0,v0,v1
        800abfc8 00 00 42 80     lb         v0,0x0(v0) //DAT_801292e0   
        800abfcc 00 00 00 00     nop
        800abfd0 40 00 42 30     andi       v0,v0,0x40
        800abfd4 00 16 02 00     sll        v0,v0,0x18
        800abfd8 03 16 02 00     sra        v0,v0,0x18
        800abfdc 15 00 40 14     bne        v0,zero,0x800ac034
        800abfe0 00 00 00 00     _nop
        800abfe4 90 93 82 87     lh         v0,-0x6c70(gp) //Current hour
        800abfe8 10 00 01 24     li         at,0x10
        800abfec 05 00 41 14     bne        v0,at,0x800ac004
        800abff0 21 18 40 00     _move      v1,v0
        800abff4 41 5c 03 0c     jal        0x800d7104 //SetSunlight                             
        800abff8 21 20 00 00     _clear     a0
        800abffc 0e 00 00 10     b          0x800ac038
        800ac000 14 00 bf 8f     _lw        ra,0x14(sp)
                             LAB_800ac004                                
        800ac004 14 00 01 24     li         at,0x14
        800ac008 05 00 61 14     bne        v1,at,0x800ac020
        800ac00c 00 00 00 00     _nop
        800ac010 41 5c 03 0c     jal        0x800d7104 //SetSunlight                                     
        800ac014 01 00 04 24     _li        a0,0x1
        800ac018 07 00 00 10     b          0x800ac038
        800ac01c 14 00 bf 8f     _lw        ra,0x14(sp)
                             LAB_800ac020                                  
        800ac020 06 00 01 24     li         at,0x6
        800ac024 03 00 61 14     bne        v1,at,0x800ac034
        800ac028 00 00 00 00     _nop
        800ac02c 41 5c 03 0c     jal        0x800d7104 //SetSunlight                                   
        800ac030 02 00 04 24     _li        a0,0x2
                             LAB_800ac034                                  
        800ac034 14 00 bf 8f     lw         ra,0x14(sp)
                             LAB_800ac038                                  
        800ac038 10 00 b0 8f     lw         s0,0x10(sp)
        800ac03c 08 00 e0 03     jr         ra
        800ac040 18 00 bd 27     _addiu     sp,sp,0x18
