//This is a patch that adds code missing from the battle time update function

//Originally if an hour pass due to a battle, the evolution timer would not go up
//And if a day passed due to a battle, the daily events would not be reset (meat farm, etc...) and the age would not go up

//For the original code, check the file "Pass time after battle" inside the folder "Digimon code"

Changed:

void PassTimeCombat(int RemainingEnemies)
{
  
  _CurrentFrame = _CurrentFrame + 400;
  _CurrentMinuteMemory = _CurrentMinuteMemory + 20;
  if (59 < _CurrentMinuteMemory) 
  {
    EvoTimer = EvoTimer + 1; //Added 
    _CurrentMinuteMemory = _CurrentMinuteMemory - 40;
    _CurrentHourMemory = _CurrentHourMemory + 1;
    Lifetime = Lifetime - 1;
    if (Lifetime < 0) 
      Lifetime = 0;    
    if ((ConditionFlag & 1) != 0) 
    {
      ImsomniaCounter = ImsomniaCounter + 1;
      MissedSleepHours = MissedSleepHours + 1;
    }
    if ((ConditionFlag & 0x40) != 0)
      SicknessTries = SicknessTries + 1;
    
    if ((ConditionFlag & 0x20) != 0) 
      InjuredTimer = InjuredTimer + 1;
    
    if ((ConditionFlag & 0x40) != 0)
      SicknessTimer = SicknessTimer + 1;
    
    if (23 < _CurrentHourMemory)
    {
      _CurrentHourMemory = 0;
      _CurrentFrame = _CurrentMinuteMemory * 20;
      FUN_800FC374(); //Added, Reset daily events
      age = age + 1; //Added 
      _currentDay = _currentDay + 1;
      if (29 < _currentDay) 
      {
        month = month + 1;
        _currentDay = 0;
        if (99 < month)
          month = 0;
      }
    }
  }
//Code ignored
} 



Disassembly:

        Offset      Hex               Commands

Changed:
                            
        800abccc 92 93 85 87     lh         a1,-0x6c6e(gp) //Current minute 
        800abcd0 e8 ff bd 27     addiu      sp,sp,-0x18
        800abcd4 14 00 a5 20     addi       a1,a1,0x14
        800abcd8 92 93 85 a7     sh         a1,-0x6c6e(gp) //Current minute
        800abcdc dc 93 82 97     lhu        v0,-0x6c24(gp) //Current frame
        800abce0 14 00 bf af     sw         ra,0x14(sp)
        800abce4 10 00 b0 af     sw         s0,0x10(sp)
        800abce8 90 01 42 24     addiu      v0,v0,0x190
        800abcec 21 80 04 00     move       s0,a0
        800abcf0 3c 00 a1 28     slti       at,a1,0x3c
        800abcf4 66 00 20 14     bne        at,zero,0x800abe90
        800abcf8 dc 93 82 a7     _sh        v0,-0x6c24(gp)//Current frame
        800abcfc 14 80 01 3c     lui        at,0x8014
        800abd00 b6 84 22 84     lh         v0,-0x7b4a(at)//EvoTimer     
        800abd04 14 80 01 3c     lui        at,0x8014
        800abd08 01 00 42 20     addi       v0,v0,0x1
        800abd0c b6 84 22 a4     sh         v0,-0x7b4a(at)//EvoTimer        
        800abd10 c4 ff a5 20     addi       a1,a1,-0x3c
        800abd14 92 93 85 a7     sh         a1,-0x6c6e(gp)//Current minute
        800abd18 14 80 01 3c     lui        at,0x8014
        800abd1c a8 84 22 84     lh         v0,-0x7b58(at)//Lifetime
        800abd20 90 93 83 87     lh         v1,-0x6c70(gp)//Current hour
        800abd24 14 80 01 3c     lui        at,0x8014
        800abd28 01 00 63 24     addiu      v1,v1,0x1
        800abd2c 90 93 83 a7     sh         v1,-0x6c70(gp)//CurrentHour
        800abd30 ff ff 42 24     addiu      v0,v0,-0x1
        800abd34 03 00 41 04     bgez       v0,0x800abd44
        800abd38 a8 84 22 a4     _sh        v0,-0x7b58(at)//Lifetime   
        800abd3c 14 80 01 3c     lui        at,0x8014
        800abd40 a8 84 20 a4     sh         zero,-0x7b58(at)//Lifetime     
                             LAB_800abd44                      
        800abd44 14 80 01 3c     lui        at,0x8014
        800abd48 60 84 22 8c     lw         v0,-0x7ba0(at)//ConditionFlag  
        800abd4c 00 00 00 00     nop
        800abd50 01 00 42 30     andi       v0,v0,0x1
        800abd54 0d 00 40 10     beq        v0,zero,0x800abd8c
        800abd58 00 00 00 00     _nop
        800abd5c 14 80 01 3c     lui        at,0x8014
        800abd60 72 84 22 84     lh         v0,-0x7b8e(at)//ImsomniaCounter 
        800abd64 00 00 00 00     nop
        800abd68 01 00 42 20     addi       v0,v0,0x1
        800abd6c 14 80 01 3c     lui        at,0x8014
        800abd70 72 84 22 a4     sh         v0,-0x7b8e(at)//ImsomniaCounter  
        800abd74 14 80 01 3c     lui        at,0x8014
        800abd78 74 84 22 84     lh         v0,-0x7b8c(at)//MissedSleepHours  
        800abd7c 00 00 00 00     nop
        800abd80 01 00 42 20     addi       v0,v0,0x1
        800abd84 14 80 01 3c     lui        at,0x8014
        800abd88 74 84 22 a4     sh         v0,-0x7b8c(at)//MissedSleepHours  
                             LAB_800abd8c                                
        800abd8c 14 80 01 3c     lui        at,0x8014
        800abd90 60 84 22 8c     lw         v0,-0x7ba0(at)// Condition flag
        800abd94 00 00 00 00     nop
        800abd98 40 00 42 30     andi       v0,v0,0x40
        800abd9c 07 00 40 10     beq        v0,zero,0x800abdbc
        800abda0 00 00 00 00     _nop
        800abda4 14 80 01 3c     lui        at,0x8014
        800abda8 92 84 22 84     lh         v0,-0x7b6e(at) //Sickness tries
        800abdac 00 00 00 00     nop
        800abdb0 01 00 42 20     addi       v0,v0,0x1
        800abdb4 14 80 01 3c     lui        at,0x8014
        800abdb8 92 84 22 a4     sh         v0,-0x7b6e(at)// Sickness tries
                             LAB_800abdbc                                  
        800abdbc 14 80 01 3c     lui        at,0x8014
        800abdc0 60 84 22 8c     lw         v0,-0x7ba0(at)//ConditionFlag  
        800abdc4 00 00 00 00     nop
        800abdc8 20 00 42 30     andi       v0,v0,0x20
        800abdcc 07 00 40 10     beq        v0,zero,0x800abdec
        800abdd0 00 00 00 00     _nop
        800abdd4 14 80 01 3c     lui        at,0x8014
        800abdd8 98 84 22 84     lh         v0,-0x7b68(at)//InjuredTimer  
        800abddc 00 00 00 00     nop
        800abde0 01 00 42 20     addi       v0,v0,0x1
        800abde4 14 80 01 3c     lui        at,0x8014
        800abde8 98 84 22 a4     sh         v0,-0x7b68(at)//InjuredTimer   
                             LAB_800abdec                                   
        800abdec 14 80 01 3c     lui        at,0x8014
        800abdf0 60 84 22 8c     lw         v0,-0x7ba0(at)//ConditionFlag 
        800abdf4 00 00 00 00     nop
        800abdf8 40 00 42 30     andi       v0,v0,0x40
        800abdfc 07 00 40 10     beq        v0,zero,0x800abe1c
        800abe00 00 00 00 00     _nop
        800abe04 14 80 01 3c     lui        at,0x8014
        800abe08 96 84 22 84     lh         v0,-0x7b6a(at)//SicknessTimer 
        800abe0c 00 00 00 00     nop
        800abe10 01 00 42 20     addi       v0,v0,0x1
        800abe14 14 80 01 3c     lui        at,0x8014
        800abe18 96 84 22 a4     sh         v0,-0x7b6a(at)//SicknessTimer  
                             LAB_800abe1c                                   
        800abe1c 18 00 61 28     slti       at,v1,0x18
        800abe20 1b 00 20 14     bne        at,zero,0x800abe90
        800abe24 00 00 00 00     _nop
        800abe28 90 93 80 a7     sh         zero,-0x6c70(gp)//Current hour
        800abe2c 80 10 05 00     sll        v0,a1,0x2
        800abe30 20 10 45 00     add        v0,v0,a1
        800abe34 80 10 02 00     sll        v0,v0,0x2
        800abe38 dc 93 82 a7     sh         v0,-0x6c24(gp) //Current frame
        800abe3c dd f0 03 0c     jal        0x800fc374   //The reset daily events function                              
        800abe40 00 00 00 00     _nop
        800abe44 14 80 01 3c     lui        at,0x8014
        800abe48 aa 84 23 84     lh         v1,-0x7b56(at)//age                  
        800abe4c d8 93 82 87     lh         v0,-0x6c28(gp)//Current day
        800abe50 14 80 01 3c     lui        at,0x8014
        800abe54 01 00 63 24     addiu      v1,v1,0x1
        800abe58 aa 84 23 a4     sh         v1,-0x7b56(at)//age                          
        800abe5c 01 00 42 24     addiu      v0,v0,0x1
        800abe60 1e 00 41 28     slti       at,v0,0x1e
        800abe64 0a 00 20 14     bne        at,zero,0x800abe90
        800abe68 d8 93 82 a7     _sh        v0,-0x6c28(gp)//Current day
        800abe6c d6 93 82 93     lbu        v0,-0x6c2a(gp)//Current month
        800abe70 00 00 00 00     nop
        800abe74 01 00 42 24     addiu      v0,v0,0x1
        800abe78 d6 93 82 a3     sb         v0,-0x6c2a(gp)//Current month
        800abe7c 00 00 00 00     nop
        800abe80 64 00 41 2c     sltiu      at,v0,0x64
        800abe84 02 00 20 14     bne        at,zero,0x800abe90
        800abe88 d8 93 80 a7     _sh        zero,-0x6c28(gp)
        800abe8c d6 93 80 a3     sb         zero,-0x6c2a(gp)//Current month
