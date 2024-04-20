//This is how the new function works, the original can be seen in SydMontague's Github


void BattleGains&ItemDrops(int ItemDropArray)

{
//code ignored
    if (enemyStat < 100) 
      currentStatGain = enemyCount;
    
    else 
    {
	 //normal version
       statGainResult = enemyStat * enemyCount / 100;       //This gives you 1% of the enemy stat multiplied by the amount of enemies
	  
	 //super version 	
	   statGainResult = enemyStat * enemyCount / 50;       //This gives you 2% of the enemy stat multiplied by the amount of enemies
	
	 //hyper version
	   statGainResult = enemyStat * enemyCount / 10;       //This gives you 10% of the enemy stat multiplied by the amount of enemies
	   
      currentStatGain = statGainResult;
    }
//code ignored
}



Disassembly:

                       BattleGains&ItemDrops

         Offset       Hex         Commands

//the changes start here

        800ecfc4 00 00 00 00     nop
        800ecfc8 00 00 00 00     nop
		
		//normal version
        800ecfcc 64 00 02 24     li         v0,0x64
		
		//super version
		800ecfcc 32 00 02 24     li         v0,0x32
		
		//hyper version
		800ecfcc 0a 00 02 24     li         v0,0xa
		
        800ecfd0 2a 08 82 00     slt        at,a0,v0
        800ecfd4 24 00 20 14     bne        at,zero,0x800ed068
        800ecfd8 00 00 00 00     _nop
        800ecfdc 21 18 00 01     move       v1,t0
        800ecfe0 18 00 83 00     mult       a0,v1
        800ecfe4 12 18 00 00     mflo       v1
        800ecfe8 00 00 00 00     nop
        800ecfec 1a 00 62 00     div        v1,v0
        800ecff0 12 18 00 00     mflo       v1
        800ecff4 00 00 00 00     nop
        800ecff8 00 00 00 00     nop
        800ecffc 00 00 00 00     nop
        800ed000 00 00 00 00     _nop
        800ed004 00 00 00 00     nop                                                             
        800ed008 14 80 02 3c     lui        v0,0x8014
        800ed00c 68 d4 42 24     addiu      v0,v0,-0x2b98
        800ed010 21 10 51 00     addu       v0,v0,s1
        800ed014 19 00 00 10     b          0x800ed07c
        800ed018 00 00 43 a4     _sh        v1,0x0(v0) //currentStatGain 

//this is not read anymore       
                 
        800ed01c ff ff a5 20     addi       a1,a1,-0x1
        800ed020 a4 89 83 27     addiu      v1,gp,-0x765c
        800ed024 21 18 65 00     addu       v1,v1,a1
        800ed028 00 00 63 80     lb         v1,0x0(v1)
        800ed02c 00 00 00 00     nop
        800ed030 18 00 83 00     mult       a0,v1
        800ed034 12 20 00 00     mflo       a0
        800ed038 80 18 04 00     sll        v1,a0,0x2
        800ed03c 20 20 64 00     add        a0,v1,a0
        800ed040 80 18 04 00     sll        v1,a0,0x2
        800ed044 20 18 83 00     add        v1,a0,v1
        800ed048 80 18 03 00     sll        v1,v1,0x2
        800ed04c 1a 00 62 00     div        v1,v0
        800ed050 12 90 00 00     mflo       s2
        800ed054 b5 8d 02 0c     jal        0x800a36d4 //ReturnRandom                                     
        800ed058 64 00 04 24     _li        a0,0x64
        800ed05c 2a 08 52 00     slt        at,v0,s2
        800ed060 06 00 20 10     beq        at,zero,0x800ed07c
        800ed064 00 00 00 00     _nop

//jumps here from 800ecfd4       
                   
        800ed068 14 80 02 3c     lui        v0,0x8014
        800ed06c 68 d4 42 24     addiu      v0,v0,-0x2b98
        800ed070 21 18 00 01     move       v1,t0
        800ed074 21 10 51 00     addu       v0,v0,s1
        800ed078 00 00 43 a4     sh         v1,0x0(v0) //currentStatGain 