using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Xml.Linq;
using Godot;

public class digimonNecessaryData
{
  public List<byte> Attacks { get; set; }
  public byte Level { get; set; }
  public bool HasFinisher { get; set; }

  public int finisherLocation { get; set; }
  public byte Type1 { get; set; }
  public int dataWeight { get; set; }

  public digimonNecessaryData(uint currentOffset, Stream bin)
  {
    finisherLocation = -1;
    Attacks = new List<byte>();
    HasFinisher = false;

    bin.Position = currentOffset + 0x1D;
    Level = (byte)bin.ReadByte();

    bin.Position = currentOffset + 0x1E;
    Type1 = (byte)bin.ReadByte();

    bin.Position = currentOffset + 0x23;

    if (currentOffset != 0x14D6F430)
      for (int j = 0; j < 16; j++)
      {
        int attackID = bin.ReadByte();
        if (attackID == 0xFF)
          break;
        else if (attackID > 56 && attackID < 113)
        {
          HasFinisher = true;
          finisherLocation = j;
        }
        Attacks.Add((byte)attackID);
      }
    else
    {
      for (int j = 0; j < 6; j++)
      {
        int attackID = bin.ReadByte();
        Attacks.Add((byte)attackID);
      }
      Attacks.AddRange([0xE, 0xF, 0x46]);
      HasFinisher = true;
      finisherLocation = 8;
    }
  }
  
}

public class TechNecessaryData
{
  public short Power { get; set; }
  public byte Type { get; set; }

  public TechNecessaryData(uint currentOffset, BinaryReader reader, Stream bin)
  {
      bin.Position = currentOffset;
      Power = reader.ReadInt16();
      bin.Position = currentOffset + 5;
      Type = (byte)bin.ReadByte();
      
  }
}

public class digimonMapData
{
  public int offset { get; set; }
  public short[] Attacks { get; set; }
  public short[] AttackChances { get; set; }

}

public class MapData
{
  public List<digimonMapData> digimonsMapData { get; set; }
  public int[] scriptOffsets { get; set; }
  public byte digimonID { get; set; }
}

public class BossMapData
{
  public int scriptOffset1{ get; set; }
  public int scriptOffset2{ get; set; }
  public int mapOffset { get; set; }
}

public class digimonStatsData
{
  public short currentHP { get; set; }
  public short currentMP { get; set; }
  public short maxHP { get; set; }
  public short maxMP { get; set; }
  public short offense { get; set; }
  public short defense { get; set; }
  public short speed { get; set; }
  public short brains { get; set; }

  public digimonStatsData(short cHP, short cMP, short mHP, short mMP, short off, short def, short spd, short brn)
  {
    currentHP = cHP;
    currentMP = cMP;
    maxHP = mHP;
    maxMP = mMP;
    offense = off;
    defense = def;
    speed = spd;
    brains = brn;
  }
}

public class DigimonRandomizer
{

  bool isHardcore, tHardcore;
  Stream bin;

  List<digimonNecessaryData> digimonData;
  List<TechNecessaryData> techData;

  List<int> NPCOffsets =
  new List<int>(){0x1420C5C, 0x1420CC2, 0x1420D22, 0x1420D8E, 0x1420DFA, 0x1420E66, 0x1420ECC, 0x1420F38, 0x14D3A16, 0x14D3ACA, 0x14D3B24, 0x14D3B7E, 0x157DD56, 0x157DDB6,
                  0x157DE76, 0x157DED0, 0x16433F4, 0x164344E, 0x16434AE, 0x1643508, 0x18D6626, 0x18D66EC, 0x199D106, 0x199D16C, 0x199D1D8, 0x199D232, 0x199D298, 0x199D304,
                  0x1BA6246, 0x1BA62AC, 0x1BA6306, 0x1BA636C, 0x1C018B2, 0x1C0190C, 0x1C01978, 0x1C019D2, 0x1CAB7EA, 0x1CAB85C, 0x1CAB8C8, 0x1CAB922, 0x1CABAC4, 0x1CABB30,
                  0x1D68A54, 0x1D68B08, 0x1D68B62, 0x1D68BBC, 0x1E0A2C4, 0x1E0A378, 0x23FA8A4, 0x23FA8FE, 0x23FA958, 0x2449580, 0x24495DA, 0x2497646, 0x24976A6, 0x249770C,
                  0x249776C, 0x24977CC, 0x2497832, 0x254AA4E, 0x254AAA8, 0x254AB02, 0x254AB62, 0x26198D6, 0x2619936, 0x2619990, 0x2619B20, 0x26D58FE, 0x26D595E, 0x26D59B8,
                  0x26D5A18, 0x279B38A, 0x279B3F0, 0x279B450, 0x279B4AA, 0x279B510, 0x279B570, 0x2860346, 0x28603A0, 0x2860406, 0x286046C, 0x28604C6, 0x286052C, 0x29924EE,
                  0x2992548, 0x29925AE, 0x2992608, 0x2992662, 0x29926C8, 0x2B7EC76, 0x2B7ECD0, 0x2B7ED30, 0x2B7ED8A, 0x2B7EDEA, 0x2B7EE50, 0x2D4BF48, 0x2D4BFA2, 0x2D4C008,
                  0x2D4C062, 0x2D4C0BC, 0x2D4C122, 0x2DF66A4, 0x2DF66FE, 0x2DF6758, 0x2F3084E, 0x2F308A8, 0x2F30902, 0x2F3095C, 0x2F309B6, 0x2FBE59E, 0x2FBE5FE, 0x2FBE66A,
                  0x2FBE6CA, 0x2FBE724, 0x4FD240E, 0x4FD246E, 0x4FD2522, 0x4FD257C, 0x506015E, 0x50601B8, 0x5060212, 0x50602C6, 0x50EDEAE, 0x50EDF0E, 0x50EDF68, 0x56448FE,
                  0x5644948, 0x56449A2, 0x56449FC, 0x56D263E, 0x56D2698, 0x56D26F2, 0x56D274C, 0x576038E, 0x57603E8, 0x5760442, 0x576049C, 0x5EBE83E, 0x5EBE898, 0x5EBE8F2,
                  0x5EBE94C, 0x5EBE9A6, 0x5F02DCE, 0x5F02E28, 0x5F02E82, 0x5F90B1E, 0x5F90B78, 0x5F90BD2, 0x5F90C2C, 0x601E86E, 0x601E8C8, 0x601E928, 0x60AC5BE, 0x60AC678,
                  0x60AC618, 0x60AC738, 0x60AC6D2, 0x60AC798, 0x615FE1E, 0x615FE78, 0x6160002, 0x6160062, 0x61600C2, 0x616011C, 0x6160176, 0x61601D6, 0x61EF122, 0x61EF182,
                  0x61EF1E2, 0x62A221E, 0x62A2284, 0x62A22F0, 0x62A235C, 0x6407EFA, 0x6407F66, 0x6407FD8, 0x640803E, 0x64080B0, 0x640811C, 0x640818E, 0x64081F4, 0x64BADB6,
                  0x64BAE22, 0x64BAE94, 0x64BAEFA, 0x64BB09C, 0x64BB108, 0x64BB17A, 0x64BB1E0, 0x6580D9C, 0x6580E02, 0x6633BA6, 0x6633C06, 0x6633C72, 0x6633CDE, 0x6633D3E,
                  0x6633DAA, 0x67AC99A, 0x6843EA6, 0x6844030, 0x684408A, 0x6B4270E, 0x6B4276E, 0x6B427DA, 0x6B42964, 0x6B429BE, 0x6B42A1E, 0x6B42A8A, 0x6B42AE4, 0x6C08FBA,
                  0x6C0901A, 0x6C0907A, 0x6C090E6, 0x6C09146, 0x6C091B2, 0x6CCE592, 0x6CCE5F8, 0x6CCE658, 0x6CCE6BE, 0x6D9CA06, 0x6D9CA7E, 0x6D9CAF6, 0x6D9CB6E, 0x6D9CBE6,
                  0x6D9CC5E, 0x6E0EF4E, 0x6E0EFBA, 0x6E0F01A, 0x6E0F07A, 0x6E0F0E6, 0x6ED3D9C, 0x7070BC6, 0x7070C2C, 0x7070C86, 0x7070CE6, 0x7070D4C, 0x7070DA6, 0x70FE5D6,
                  0x70FE630, 0x70FE696, 0x70FE6F0, 0x70FE74A, 0x70FE7B0, 0x7195A22, 0x7195A7C, 0x7195AD6, 0x723F662, 0x723F6C2, 0x723F728, 0x723F788, 0x7541886, 0x75418E6,
                  0x7541946, 0x75A623E, 0x75A629E, 0x75A62FE, 0x76B0F7A, 0x76B0FD4, 0x77489B2, 0x7748A0C, 0x7AFA3C2,
                  0x7AFA41C, 0x7AFA476, 0x7C6E44E, 0x7D0A3AE, 0x7DF0324, 0x7DF037E, 0x7DF03DE, 0x7DF044A, 0x7EA7F7A, 0x7EA7FDA, 0x7EA8040, 0x7EA80B2, 0x7EA8112, 0x7EA8178,
                  0x7F6E396, 0x7F6E3F6, 0x7F6E462, 0x7F6E4BC, 0x7F6E51C, 0x7F6E588, 0x800E866, 0x800E8CC, 0x800E92C, 0x800E992, 0x80D4BDC, 0x80D4D6C, 0x80D4DD8, 0x80D4E44,
                  0x80D4EA4, 0x80D4F10, 0x819AB92, 0x819ABF2, 0x819AC52, 0x819ACB2, 0x819AD12, 0x819AD72, 0x831290E, 0x8312968, 0x83129C2, 0x8312A1C, 0x8312A76, 0x8312AD6,
                  0x86BCF7E, 0x86BCFD8, 0x86BD038, 0x86BD092, 0x87700E6, 0x8770146, 0x87701A6, 0x8770206, 0x8836646, 0x88366B2, 0x883671E, 0x883678A, 0x88E959C, 0x88E9602,
                  0x899CB36, 0x899CB9C, 0x899CC02, 0x899CC68, 0x899CCCE, 0x899CD34, 0x8B16304, 0x8B16370, 0x8B163CA, 0x8B16436, 0x8B846D2, 0x8B84738, 0x8B846D4, 0x8B8479E,
                  0x8B847F8, 0x8B8485E, 0x8D464D2, 0x8D46538, 0x8D46598, 0x8D465F2, 0x8D46658, 0x8D466B8, 0x8F3E146, 0x8F3E1B2, 0x8F3E34E, 0x8F3E3BA, 0x9004E22,
                  0x9004E8E, 0x9004EF4, 0x9004F60, 0x90B824E, 0x90B82B4, 0x90B831A, 0x90B8386, 0x90B83EC, 0x90B8452, 0x919D74A, 0x919D7AA, 0x919D80A, 0x919D864, 0x919D8C4,
                  0x919D924, 0x92475CE, 0x924762E, 0x9247694, 0x92476F4, 0x9303D68, 0x9303DCE, 0x9303E2E, 0x9303E94, 0x93BFFF6, 0x93C0056, 0x93C00BC, 0x93C011C, 0x94F2E2A,
                  0x94F2EA2, 0x94F303E, 0x963383E, 0x9633898, 0x96338F2, 0x96CA9A2, 0x96CAA14, 0x96CAA7A, 0x96CAAE6, 0x9774054, 0x97740AE, 0x9774108, 0x97C191E, 0x97C1984,
                  0x991EF12, 0x991EF6C, 0x991F0FC, 0x991F162, 0x991F1C8, 0x991F222, 0x991F282, 0x991F2E8, 0x9C9B44E, 0x9C9B4AE, 0x9C9B69E, 0x9C9B6FE, 0x9EEBC16, 0x9EEBC76,
                  0x9EEBCD0, 0x9EEBD30, 0x9EEBD90, 0x9EEBDEA, 0x9FAF9BC, 0x9FAFA22, 0x9FAFA88, 0x9FAFAE8, 0x9FAFB4E, 0x9FAFCDE, 0xA073802, 0xA073992, 0xA0739F2, 0xA073A52,
                  0xA073AB8, 0xA073B18, 0xA073B7E, 0xA1376B2, 0xA137712, 0xA137772, 0xA1377D2, 0xA137832, 0xA346156, 0xA3461B6, 0xA346216, 0xA346276, 0xA4F0BA8, 0xA5B5E3A,
                  0xA5B5E94, 0xA5B5EEE, 0xA7EEAD0, 0xA7EEB36, 0xA7EEB96, 0xA7EEBFC, 0xA86D5FE, 0xA86D658, 0xA86D6B2, 0xA86D70C, 0xA86D896, 0xA86D8F0, 0xA86D94A, 0xA86D9A4},
  bossOffsets =
  new List<int>(){0x14D3A70, 0x157DE1C, 0x164339A, 0x17C609E, 0x18D6692, 0x1C01852, 0x2133FCA, 0x2134024, 0x213407E, 0x21340D8, 0x2134132, 0x254ABC2, 0x26D5A72, 0x2929254,
                  0x2A3286A, 0x2AF131C, 0x2E3C072, 0x2EA2E6A, 0x2EA2EC4, 0x2EA2F1E, 0x3085C02, 0x3085C5C, 0x314D97A, 0x314D9D4, 0x3214FBA, 0x3215014, 0x32DC392, 0x32DC3EC,
                  0x33A37DA, 0x33A3834, 0x346AF4A, 0x346AFA4, 0x35328BA, 0x3532914, 0x35F9D02, 0x35F9D5C, 0x36C1472, 0x36C14CC, 0x3789012, 0x378919C, 0x3850EBA, 0x3850F14,
                  0x3918E2A, 0x3918E84, 0x4F4D9AE, 0x4F4DA0E, 0x517BBF6, 0x558834A, 0x59E5B6A, 0x5EBEA00, 0x60AC7FE, 0x61EF0C2, 0x8EEBF2E, 0x68440E4, 0x684413E, 0x6844198,
                  0x69CECCA, 0x6FA261C, 0x7195B30, 0x7195B8A, 0x7195BE4, 0x7351FCA, 0x76268AE, 0x7626908, 0x7626962, 0x76269BC, 0x76B102E, 0x76B1088, 0x76B10E2, 0x76B113C,
                  0x780E4D2, 0x7D6A690, 0x800E7FA, 0x831290E, 0x8836592, 0x88365EC, 0x8B162AA, 0x8E50416, 0x8EEBE7A, 0x9303D0E, 0x9303EF4, 0x9858822, 0x9E286E0, 0x9E28740,
                  0xA07377E, 0xA5B5F48, 0xA5B5FA2, 0xA5B5FFC, 0xA668D76, 0xA7EEA76,
                  0x63429FA, 0x6342A54, 0x6342AAE, 0x57EE0C6, 0x57EE120, 0x5860516, 0x5860570, 0x59201CE, 0x5920228 }; //extra maps

  Dictionary<string, MapData> NPCMapData;
  List<BossMapData> bossMapData;

  public DigimonRandomizer(bool hardcore, bool trueHardcore, ref Stream binOG, BinaryReader reader)
  {
    isHardcore = hardcore;
    tHardcore = trueHardcore;
    bin = binOG;

    uint dataOffset = 0x14D6E9DC, currentOffset = dataOffset;
    uint[] jumpOffsets = { 0x14D6EB28, 0x14D6F458, 0x14D6FD88, 0x14D706B8, 0x14D70FE8, 0x79999999 };
    int jumpValue = 0;

    digimonData = new List<digimonNecessaryData>();

    for (int i = 0; i < 176; i++)
    {
      digimonData.Add(new digimonNecessaryData(currentOffset, bin));
       currentOffset = currentOffset + 0x34;
      if (currentOffset + 0x1C > jumpOffsets[jumpValue])
      {
        currentOffset = currentOffset + 0x130;
        jumpValue++;
      }
    }

    techData = new List<TechNecessaryData>();

    dataOffset = 0x14D66DF8;
    currentOffset = dataOffset;

    for (int i = 0; i < 57; i++)
    {
      techData.Add(new TechNecessaryData(currentOffset, reader, bin));
      currentOffset = currentOffset + 0x10;
    }

    NPCMapData = new Dictionary<string, MapData>();
    bossMapData = new List<BossMapData>();

    if (hardcore)
    {
      bin.Position = 0x6580D42;
      if (bin.ReadByte() != 0xA)
        return;

      bossOffsets.AddRange([0x1D68AAE, 0x2E3C0CC, 0x4F4DA6E, 0x5A8FA32, 0x5B399C6, 0x6580D42, 0x66F9FCA, 0x66FA024, 0x66FA07E, 0x6C08F5A, 0x6ED3D42, 0x76B1196, 0x7F6E32A,
                            0x80D4B82, 0x8A6330E, 0x8EEBED4, 0x94851F6, 0x9AA152A, 0x9AA1584, 0x9AA15DE, 0x9B3AAC6, 0x9B3AB20, 0x9B3AB7A, 0x9BBF4FE]);
      if (trueHardcore)
      {
        bin.Position = 0x723F7EE;
        if (bin.ReadByte() != 0x2F)
          return;

        bossOffsets.AddRange([0x1EC08C6, 0x1F404D2, 0x20C7328, 0x23A3066, 0x723F7EE, 0x740DEC6, 0x740DF20, 0x7D6A636, 0x825FCA2, 0x825FCFC, 0x991F282, 0x991F2E8, 0xA40BA06,
                              0xA40BA60, 0xA40BABA, ]);
      }
    }
  }


  public void RandomizeDigimonNPC(int option, bool bosses, Random numberGenerator, BinaryWriter writter)
  {
    List<byte> validNPCDigimon = new List<byte>();
    switch (option)
    {
      case 0:
        break;
      case 1:
        for (int i = 66; i < 112; i++)
          validNPCDigimon.Add((byte)i);
        break;
      case 2:
        for (int i = 0; i < digimonData.Count; i++)
        {
          if (i < 114 || (i > 128 && digimonData[i].HasFinisher))
            validNPCDigimon.Add((byte)i);
        }
        validNPCDigimon.Add(115);
        break;

    }

    if (bosses && option > 0)
    {
      List<byte> validDigimon = new List<byte>();
      for (int i = 0; i < digimonData.Count; i++)
      {
        if (digimonData[i].HasFinisher && i < 67 && i > 128)
          validDigimon.Add((byte)i);
      }

      validDigimon.Add(112);
      validDigimon.Add(113);
      validDigimon.Add(115);

      foreach (BossMapData mapData in bossMapData)
      {
        byte randomDigimon = validDigimon[numberGenerator.Next(validDigimon.Count)];
        bin.Position = mapData.scriptOffset1;
        bin.WriteByte(randomDigimon);
        bin.Position = mapData.scriptOffset2;
        bin.WriteByte(randomDigimon);
        bin.Position = mapData.mapOffset;

        int currentOffset = mapData.mapOffset;
        List<int> attackValues = new List<int>();

        for (int i = 0; i < digimonData[randomDigimon].Attacks.Count; i++)
          if (i != digimonData[randomDigimon].finisherLocation) attackValues.Add(i);

        for (int i = 0; i < 4; i++)
        {
          if (i == digimonData[randomDigimon].Attacks.Count)
            break;

          bin.Position = currentOffset + 44 + i * 2;
          if (digimonData[randomDigimon].HasFinisher)
          {
            if (i < 3)
            {
              int value = attackValues[numberGenerator.Next(attackValues.Count)];
              if (digimonData[randomDigimon].Attacks[value] > 56)
                value--;
              writter.Write((short)(0x2E + value));
              attackValues.Remove(value);
              bin.Position = currentOffset + 52 + i * 2;
              writter.Write((short)30);
            }
            else
            {
              writter.Write((short)(0x2E + digimonData[randomDigimon].finisherLocation));
              bin.Position = currentOffset + 52 + i * 2;
              writter.Write((short)10);
            }

          }
          else
          {
            int value = attackValues[numberGenerator.Next(attackValues.Count)];
            writter.Write((short)(0x2E + value));
            attackValues.Remove(value);
            bin.Position = currentOffset + 52 + i * 2;
            writter.Write((short)30);
          }
        }
      }
    }
  }

  public void RandomizeNPCStats(int option, bool bosses, Random numberGenerator, BinaryReader reader, BinaryWriter writter)
  {
    switch (option)
    {
      case 0:
        ShuffleStats(NPCOffsets, numberGenerator, reader, writter);
        break;
      case 1:
        if (isHardcore)
        {
          if (tHardcore)
            RandomizeStats(NPCOffsets, numberGenerator, writter, 850, 150);
          else
            RandomizeStats(NPCOffsets, numberGenerator, writter, 741, 100);
        }
        else
          RandomizeStats(NPCOffsets, numberGenerator, writter, 491, 10);
        break;
      case 2:
        if (isHardcore)
        {
          if (tHardcore)
            RandomizeStats(NPCOffsets, numberGenerator, writter, 1351, 150);
          else
            RandomizeStats(NPCOffsets, numberGenerator, writter, 1101, 100);
        }
        else
          RandomizeStats(NPCOffsets, numberGenerator, writter, 999, 1);
        break;
    }

    if (bosses)
    {
      switch (option)
      {
        case 0:
          ShuffleStats(bossOffsets, numberGenerator, reader, writter);
          break;
        case 1:
          if (isHardcore)
          {
            if (tHardcore)
              RandomizeStats(bossOffsets, numberGenerator, writter, 1301, 200);
            else
              RandomizeStats(bossOffsets, numberGenerator, writter, 1051, 150);
          }
          else
            RandomizeStats(bossOffsets, numberGenerator, writter, 960, 40);
          break;
        case 2:
          if (isHardcore)
          {
            if (tHardcore)
              RandomizeStats(bossOffsets, numberGenerator, writter, 2251, 250);
            else
              RandomizeStats(bossOffsets, numberGenerator, writter, 1301, 200);
          }
          else
            RandomizeStats(bossOffsets, numberGenerator, writter, 1091, 10);
          break;
      }
    }

  }

  public void RandomizeNPCTechs(bool bosses, bool ignoreNPC, Random numberGenerator, BinaryWriter writter)
  {
    if (!bosses && ignoreNPC)
      return;

    if (!ignoreNPC)
      RandomizeTech(NPCOffsets, numberGenerator, writter);

    if (bosses)
      RandomizeTech(bossOffsets, numberGenerator, writter);


  }

  public void RandomizeNPCMoney(int option, bool bosses, Random numberGenerator, BinaryReader reader, BinaryWriter writter)
  {
    if (NPCOffsets.Count == 0)
      return;

    switch (option)
    {
      case 0:
        ShuffleMoney(NPCOffsets, numberGenerator, reader, writter);
        break;
      case 1:
        RandomizeMoney(NPCOffsets, numberGenerator, writter);
        break;
    }

    if (bosses)
    {
      if (bossOffsets.Count == 0)
        return;

      switch (option)
      {
        case 0:
          ShuffleMoney(bossOffsets, numberGenerator, reader, writter);
          break;
        case 1:
          RandomizeMoney(bossOffsets, numberGenerator, writter);
          break;
      }
    }
  }

  public void RandomizeStarter(int option, Random numberGenerator)
  {
    List<byte> possibleDigimon = new List<byte>();

    for (int i = 1; i < 66; i++)
    {
      if (option < 5)
      {
        if (digimonData[i].Level == (option + 1))        
          possibleDigimon.Add((byte)i);        
      }
      else      
        possibleDigimon.Add((byte)i);       
    }

    uint[] digimonOffsets = { 0x14D271C0, 0x14D271B8, 0x14D19DB0, 0x14D19DD4 },
    techOffset = { 0x14CD1D50, 0x14CD1D30, 0x14D19D98, 0x14D19DC0 },
    learnOffset = { 0x14CD1D60, 0x14CD1D40, 0x14D19DA8, 0x14D19DD0 },
    checkOffset = { 0x14CD1D44, 0x14CD1D24 };

    if (option < 2)
    {
      for (int i = 0; i < 4; i++)
      {
        if (possibleDigimon.Count > i)
        {
          bin.Position = digimonOffsets[i];
          bin.WriteByte(possibleDigimon[i]);
          bin.Position = techOffset[i];
          bin.WriteByte(0x2E);
        }
        else
        {
          bin.Position = digimonOffsets[i];
          bin.WriteByte(possibleDigimon[0]);
          bin.Position = techOffset[i];
          bin.WriteByte(0x2E);
        }
      }
    }
    else
    {

      for (int i = 0; i < 4; i++)
      {
        byte selectedDigimon = possibleDigimon[numberGenerator.Next(possibleDigimon.Count)];
        if (i < 2)
        {
          bin.Position = checkOffset[i];
          bin.WriteByte(selectedDigimon);
        }
        bin.Position = digimonOffsets[i];
        bin.WriteByte(selectedDigimon);

        if (digimonData[selectedDigimon].Level > 2)
        {
          int count = 0;
          foreach (byte attack in digimonData[selectedDigimon].Attacks)
          {
            if (attack > 56)
              continue;
            if (techData[attack].Type == digimonData[selectedDigimon].Type1)
            {
              bin.Position = techOffset[i];
              bin.WriteByte((byte)(count + 0x2E));
              bin.Position = learnOffset[i];
              bin.WriteByte(attack);
              break;
            }
            count++;
          }
          if (count == digimonData[selectedDigimon].Attacks.Count)
          {
            bin.Position = learnOffset[i];
            bin.WriteByte(digimonData[selectedDigimon].Attacks[0]);
            bin.Position = techOffset[i];
            bin.WriteByte(0x2E);
          }
        }
        else
        {
          bin.Position = techOffset[i];
          bin.WriteByte(0x2E);
        }

        possibleDigimon.Remove(selectedDigimon);
      }
    }

  }

  public void StarterTech(int option, Random numberGenerator)
  {
    uint[] digimonOffsets = { 0x14D271C0, 0x14D271B8, 0x14D19DB0, 0x14D19DD4 },
    techOffset = { 0x14CD1D50, 0x14CD1D30, 0x14D19D98, 0x14D19DC0 },
    learnOffset = { 0x14CD1D60, 0x14CD1D40, 0x14D19DA8, 0x14D19DD0 };
    for (int i = 0; i < 4; i++)
    {
      bin.Position = digimonOffsets[i];
      byte Digimon = (byte)bin.ReadByte();

      int count = 0;

      if (digimonData[Digimon].Level < 3)
        continue;

      switch (option)
      {
        case 0:
          short currentPower = 1000;
          int selectedAttack = 0;

          foreach (byte attack in digimonData[Digimon].Attacks)
          {
            if (attack < 57 && techData[attack].Power < currentPower && techData[attack].Power > 0)
            {
              selectedAttack = count;
              currentPower = techData[attack].Power;
            }
            count++;
          }
          bin.Position = learnOffset[i];
          bin.WriteByte(digimonData[Digimon].Attacks[selectedAttack]);
          bin.Position = techOffset[i];
          bin.WriteByte((byte)(0x2E + selectedAttack));
          break;
        case 1:
          List<int> validAttacks = new List<int>();
          for (int j = 0; j < digimonData[Digimon].Attacks.Count; j++)
          {
            byte attack = digimonData[Digimon].Attacks[i];
            if (attack < 57 || attack == 21 || attack == 30 || attack == 34 || attack == 41 || attack == 42)
              continue;
              validAttacks.Add(j);
          }
          int randomTech = validAttacks[numberGenerator.Next(validAttacks.Count)];
          bin.Position = learnOffset[i];
          bin.WriteByte(digimonData[Digimon].Attacks[randomTech]);
          bin.Position = techOffset[i];
          bin.WriteByte((byte)(0x2E + randomTech));
          break;
      }
    }
  }

  public void StarterStats(int option, BinaryWriter writter, BinaryReader reader, Random numberGenerator)
  {
    uint offsetStart1 = 0x1407E2CE, offsetStart2 = 0x1407E2F2;
    bin.Position = offsetStart1;
    switch (option)
    {
      case 0:
        if (isHardcore)
        {
          if (tHardcore)
          {
            SetStarterStats(writter, reader, numberGenerator, 91, 40);
            bin.Position = offsetStart2;
            SetStarterStats(writter, reader, numberGenerator, 91, 40);
          }
          else
          {
            SetStarterStats(writter, reader, numberGenerator, 56, 55);
            bin.Position = offsetStart2;
            SetStarterStats(writter, reader, numberGenerator, 56, 55);
          }
        }
        else
        {
          SetStarterStats(writter, reader, numberGenerator, 51, 50);
          bin.Position = offsetStart2;
          SetStarterStats(writter, reader, numberGenerator, 51, 50);
        }
        break;
      case 1:
        if (isHardcore)
        {
          if (tHardcore)
          {
            SetStarterStats(writter, reader, numberGenerator, 180, 1);
            bin.Position = offsetStart2;
            SetStarterStats(writter, reader, numberGenerator, 180, 1);
          }
          else
          {
            SetStarterStats(writter, reader, numberGenerator, 146, 5);
            bin.Position = offsetStart2;
            SetStarterStats(writter, reader, numberGenerator, 146, 5);
          }
        }
        else
        {
          SetStarterStats(writter, reader, numberGenerator, 121, 10);
          bin.Position = offsetStart2;
          SetStarterStats(writter, reader, numberGenerator, 121, 10);
        }

        break;
    }

  }

  public void TournamentNPC(int option, BinaryReader reader, BinaryWriter writer, Random numberGenerator)
  {
    uint[] tournamentPtr = { 0x140A49FC, 0x140A4AFA, 0x140A4BFC, 0x140A4CFE, 0x140A4E00, 0x140A502E, 0x140A529A, 0x140A53D6, 0x140A551E, 0x140A5660, 0x140A57C6,
                                  0x140A5900, 0x140A5A4A, 0x140A5CC8, 0x140A5DFA, 0x140A5F46, 0x140A6080, 0x140A61B8, 0x140A62E8, 0x140A652A, 0x140A665E };

    List<short> jumps = new List<short>();

    foreach (uint ptr in tournamentPtr)
    {
      bin.Position = ptr;
      jumps.Add(reader.ReadInt16());
    }

    numberGenerator.Shuffle(tournamentPtr);

    for (int i = 0; i < tournamentPtr.Length; i++)
    {
      bin.Position = tournamentPtr[i];
      writer.Write(jumps[i]);
    }

    uint bugOffset = 0x1402897E;
    bin.Position = bugOffset;
    switch (option)
    {
      case 0:
        writer.Write((short)11151);
        break;
      case 1:
        writer.Write((short)11217);
        uint[] digimonValues = { 0x140A69A2, 0x140A69B8, 0x140A69FB, 0x140A6A4C, 0x140A6A7A, 0x140A6A92, 0x140A6A96, 0x140A6AA6, 0x140A6AB6, 0x140A6AC6, 0x140A6AD6,
                                 0x140A6B10, 0x140A6B26, 0x140A6B4A, 0x140A6B63, 0x140A6B81, 0x140A6B94, 0x140A6BA3, 0x140A6BAC, 0x140A6BC6, 0x140A6BE1, 0x140A6BF7, 0x140A6C11 };

        List<byte> currentTournament = new List<byte>();
        for (int i = 1; i < 114; i++)
        {
          if (i == 62)
            currentTournament.Add(115);
          else
            currentTournament.Add((byte)i);
        }
        byte[] copyDigimon = currentTournament.ToArray();
        for (int i = 0; i < digimonValues.Length; i++)
        {
          int count = 0;
          while (true)
          {
            bin.Position = digimonValues[i] + count;
            if (bin.ReadByte() > 253)
              break;
            bin.Position = digimonValues[i] + count;
            byte random = currentTournament[numberGenerator.Next(currentTournament.Count)];
            bin.WriteByte(random);
            count++;
            currentTournament.Remove(random);
          }
          currentTournament.Clear();
          currentTournament.AddRange(copyDigimon);
        }
        break;
    }
  }

  public void Recruitments(int option, Random numberGenerator, BinaryWriter writter)
  {
    List<uint[]> recruitTriggers =
    [
        [ 0x13FD6576, 0x13FE909A, 0x1409FC94, 0x140B76F6], //Betamon
        [0x1407870C],//Devimon
        [0x14078708],//Airdramon
        [0x14016CB6, 0x140172B6, 0x1409FFD6, 0x140B8570, 0x140B860E, 0x140B860E],//Tyrannomon
        [0x13FD696A, 0x13FD6998, 0x13FF1A6C, 0x13FF44D8, 0x13FF572E, 0x13FF5740, 0x13FF5886, 0x140A009A, 0x140B7AEA, 0x140B7B18],//Meramon
        [0x13FE1D48, 0x13FE19B2, 0x14059D08, 0x140A104A, 0x14D725F0],//Seadramon
        [0x13FD9780, 0x13FD9794, 0x14073C7C, 0x140503F8, 0x14050CAC, 0x14074630, 0x140A0F72, 0x140BA900, 0x140BA914],//Numemon
        [0x14078704],//MetalGreymon
        [0x13FD6792, 0x13FED052, 0x140A0E94, 0x140B7912],//Mamemon
        [0x13FD8E96, 0x14046696, 0x14061FA8, 0x140BA016],//Monzaemon
        [0x13FD7C5A, 0x140360A2 ,0x140A0B80, 0x140B8DDA],//Gabumon
        [0x13FD7106, 0x1400DD0A, 0x140A045E, 0x140B8286],//Elecmon
        [0x13FD797E, 0x14029508, 0x140B8AFE],//Kabuterimon
        [0x140209F6, 0x14020E84],//Angemon
        [0x13FD767C, 0x13FD769C ,0x1401F636, 0x140A0236, 0x140B87FC, 0x140B881C],//Garurumon
        [0x13FD7EE2, 0x14040D64, 0x140A0152, 0x140B9062],//Frigimon
        [0x13FD6DE0, 0x1400BA8C, 0x1409A90A, 0x140B7F60],//SkullGreymon
        [0x13FD8322, 0x13FD9982, 0x1404CF3E, 0x140A1824, 0x140B94A2, 0x140BAB02],//MetalMamemon
        [0x1407BDD8, 0x1407BE1E, 0x1407C320, 0x140A1908],//Vademon
        [0x13FD715E, 0x1400E6D6, 0x140A072E, 0x140B82DE],//Patamon
        [0x13FD627A, 0x13FD6294, 0x13FD62B2, 0x13FDDEA8, 0x13FDE354, 0x140A22A6, 0x140B73FA, 0x140B7414, 0x140B7432],//Kunemon
        [0x13FD9922, 0x1407B436, 0x1407B9AE, 0x140A0654, 0x140BAAA2],//Unimon
        [0x13FD689E, 0x13FF1972, 0x13FF0F98, 0x140B7A1E],//Ogremon
        [0x13FF8018, 0x13FD6A34, 0x1409FD2E, 0x140B7BB4, 0x14D725CC],//Bakemon
        [0x13FD69B8, 0x13FF6080, 0x13FF6092, 0x13FF6456, 0x140A0A9C, 0x140B7B38],//Drimogemon
        [0x14057792, 0x1405779C],//Sukamon
        [0x1404F994, 0x1404FAD8],//Andromon
        [0x13FD84B8, 0x14052A7A, 0x140A1240, 0x140B9638],//Giromon
        [0x13FD630C, 0x13FD6348, 0x13FD6362, 0x13FDF1CC, 0x13FDF210, 0x13FDF54E, 0x140A1776, 0x140B748C, 0x140B74C8, 0x140B74E2],//Etemon
        [0x13FD71AC, 0x1400EFFC, 0x1400F148, 0x1400F292, 0x1400F50C, 0x1400F914, 0x140A0732, 0x140B832C],//Biyomon           
        [0x13FD6D72, 0x14000C52, 0x140A05B0, 0x140B7EF2],//Monochromon
        [0x13FD72F4, 0x14012928, 0x140122D6, 0x140B8474],//Leomon
        [0x13FE08F2, 0x13FE0398, 0x13FE03EE, 0x13FE056A, 0x13FE0908, 0x1409FBF6],//Coelamon
        [0x13FD7B84, 0x14032EE4, 0x14032EF4, 0x14032FA4, 0x14059D20, 0x14059F00, 0x140A050E, 0x140B8D04],//Kokatorimon
        [0x13FD79A8, 0x1402A742, 0x140B8B28],//Kuwagamon
        [0x1403CD1C, 0x1403CE84, 0x1403D04A, 0x1403D1B2, 0x1403DF1C, 0x1403E084, 0x140A0DD4],//Mojyamon
        [0x13FFFADC, 0x14019D7A, 0x140304F2, 0x1404BD10, 0x14074852],//Nanimon       
        [0x13FD9B62, 0x14081AA4, 0x14081CC4, 0x140BACE2],//Megadramon
        [0x13FD649E, 0x13FE6B52, 0x140A16B6, 0x140B761E],//Piximon
        [0x13FD9860, 0x1407737E, 0x1407750A, 0x140A1DFE, 0x140BA9E0],//Digitamamon
        [0x1403F860, 0x13FD7E36, 0x14095A7E, 0x14095DD2, 0x14096844, 0x140A09CC, 0x140B8FB6],//Penguinmon
        [0x13FD63CE, 0x13FD63E2, 0x13FE4494, 0x13FE4B16, 0x140A0C5A, 0x140B754E, 0x140B7562],//Ninjamon
        [0x14010A84],//Phoenixmon Arena
        [0x14010D6C],//H-Kabuterimon Arena
    ];

    List<short> triggers = new List<short>() {204, 206, 207, 208, 209, 210, 211, 212, 213, 214, 217, 218, 219, 220, 222, 223, 226, 227, 228, 231, 232, 233, 234, 237, 238, 239,
                                              240, 241, 242, 245, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260};

    uint[] prosperityOffsets = { 0x13FDE359, 0x13FDEB91, 0x13FDF553, 0x13FE08F7, 0x13FE1D4D, 0x13FE4B1B, 0x13FE6B57, 0x13FE88E5, 0x13FE909F, 0x13FEB641, 0x13FED04F, 0x13FF1977,
                                 0x13FF5733, 0x13FF645B, 0x13FF801D, 0x13FFFAE1, 0x14000C4F, 0x1400BA91, 0x1400DD0F, 0x1400E6DB, 0x1400F919, 0x1401292D, 0x140172BB, 0x1401F63B,
                                 0x14020E89, 0x1402950D, 0x1402A747, 0x1402BAA7, 0x140304F7, 0x140360A7, 0x140396B5, 0x1403AAFB, 0x1403CE8D, 0x14040D69, 0x140450B3, 0x1404776D,
                                 0x1404CF43, 0x1404FADD, 0x14052A7F, 0x140577A1, 0x1405B8CD, 0x14074635, 0x1407750F, 0x14078711, 0x140788F7, 0x1407B9B3, 0x1407C325, 0x14081CC9, 0x14096849 };

    if (isHardcore)
    {
      prosperityOffsets[3] = 0x13FE0891; //Coelamon
      prosperityOffsets[9] = 0x13FEB63F; //Centarumon
      prosperityOffsets[14] = 0x13FF8017; //Bakemon

      recruitTriggers[23][0] = 0x13FF8012; //Bakemon
      recruitTriggers[33][0] = 0x13FE088C; //Coelamon
      recruitTriggers[40][0] = 0x1403F868; //Penguinmon
    }

    switch (option)
    {
      case 0:
        List<byte> currentProsperity = new List<byte>();
        foreach (uint offset in prosperityOffsets)
        {
          bin.Position = offset;
          currentProsperity.Add((byte)bin.ReadByte());
        }

        byte[] shuffledProsperity = currentProsperity.ToArray();
        numberGenerator.Shuffle(shuffledProsperity);
        SetProsperity(prosperityOffsets, shuffledProsperity, isHardcore);
        break;
      case 1:
        byte[] prosperity = { 10, 7, 5, 5, 5, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        numberGenerator.Shuffle(prosperity);
        SetProsperity(prosperityOffsets, prosperity, isHardcore);
        triggers.AddRange([203, 205, 221, 224, 225, 235, 246, 261, 236]);
        recruitTriggers.AddRange(
          [[0x13FD79D2, 0x13FFF39C, 0x13FFF3B8, 0x1402BAA2, 0x1402BFDE, 0x140B8B52], // Agumon
            [0x1405B446, 0x1405B8C8, 0x1409E4BA, 0x1409E4D6, 0x140A092C] , // Greymon
            [0x1403A5A2, 0x1403AAF6, 0x140A0306, 0x14D725C6], // Birdramon
            [0x13FD7E6C, 0x1403E676, 0x140441D4, 0x14044250, 0x140450AE, 0x14059D10, 0x14059F08, 0x1406193E, 0x140B8FEC], // Whamon
            [0x13FE7AD8, 0x13FE7BF2, 0x13FE88E0, 0x1405ACC8, 0x1405AD10, 0x1405AF5A, 0x1409FDD0], // Vegiemon
            [0x13FD6B56, 0x13FD7D4C, 0x13FFA106, 0x13FFF3C4, 0x140396B0, 0x140A03A6, 0x140B7CD6, 0x140B8ECC], // Shellmon    
            [0x13FD62D6, 0x13FDEB8C, 0x140A21C0, 0x140B7456],//Palmon        
            [0x1404591C]]); // MegaSeadramon Arena

        if (isHardcore)
          recruitTriggers.Add([0x13FE9770, 0x13FE9FC2, 0x13FEA132, 0x13FEA3D4, 0x13FEA49A, 0x13FEA560, 0x13FEA626, 0x13FEA710, 0x13FEA92A, 0x13FEB63A, 0x13FEB986, 0x13FEBDDE,
          0x13FEAA28, 0x13FEAB12, 0x13FEAC10, 0x13FEAD0E, 0x13FEAE0C, 0x13FEAEDE, 0x13FEAFDC, 0x13FEB1DE, 0x13FEB2CC, 0x13FEB3BA, 0x13FEB4AC, 0x13FEB7F8, 0x13FEBC52, 0x1409FB24]);
        else
          recruitTriggers.Add([0x13FE9770, 0x13FE9FC2, 0x13FEA132, 0x13FEA3D4, 0x13FEA49A, 0x13FEA560, 0x13FEA626, 0x13FEA710, 0x13FEA92A, 0x13FEB63C, 0x13FEB988, 0x13FEBDE0,
          0x13FEAA28, 0x13FEAB12, 0x13FEAC10, 0x13FEAD0E, 0x13FEAE0C, 0x13FEAEDE, 0x13FEAFDC, 0x13FEB1DE, 0x13FEB2CC, 0x13FEB3BA, 0x13FEB4AC, 0x13FEB7F8, 0x13FEBC52, 0x1409FB24]);

        break;
    }

    short[] ShuffledTriggers = triggers.ToArray();
    numberGenerator.Shuffle(ShuffledTriggers);

    for (int i = 0; i < recruitTriggers.Count; i++)
    {
      for (int j = 0; j < recruitTriggers[i].Length; j++)
      {
        bin.Position = recruitTriggers[i][j];
        writter.Write(ShuffledTriggers[i]);
      }
    }

  }


  void SetStarterStats(BinaryWriter writter, BinaryReader reader, Random numberGenerator, int maxRNG, int minValue)
  {
    short HP = (short)((numberGenerator.Next(maxRNG) + minValue) * 10);
    short MP = (short)((numberGenerator.Next(maxRNG) + minValue) * 10);
    for (int i = 0; i < 8; i++)
    {
      if (i < 4)
        writter.Write((short)(numberGenerator.Next(maxRNG) + minValue));
      else if (i == 4 || i == 6)
        writter.Write(HP);
      else
        writter.Write(MP);
      reader.ReadInt16();
    }
  }

  void SetProsperity(uint[] offsets, byte[] prosperity, bool hardcore)
  {
    for (int i = 0; i < prosperity.Length; i++)
    {
      bin.Position = offsets[i];
      bin.WriteByte(prosperity[i]);
      if (i == 9) //Centarumon
      {
        if (hardcore)
        {
          bin.Position = 0x13FEB98B;
          bin.WriteByte(prosperity[i]);
          bin.Position = 0x13FEBDE5;
          bin.WriteByte(prosperity[i]);
        }
        else
        {
          bin.Position = 0x13FEB98D;
          bin.WriteByte(prosperity[i]);
          bin.Position = 0x13FEBDE7;
          bin.WriteByte(prosperity[i]);
        }
      }
      else if (i == 15)//Nanimon
      {
        bin.Position = 0x14019D7F;
        bin.WriteByte(prosperity[i]);
        bin.Position = 0x140304F7;
        bin.WriteByte(prosperity[i]);
        bin.Position = 0x1404BD15;
        bin.WriteByte(prosperity[i]);
        bin.Position = 0x14074857;
        bin.WriteByte(prosperity[i]);
      }
      else if (i == 32) //Mojyamon
      {
        bin.Position = 0x1403D1BB;
        bin.WriteByte(prosperity[i]);
        bin.Position = 0x1403E08D;
        bin.WriteByte(prosperity[i]);
      }
    }
  }

  void ShuffleMoney(List<int> currentData, Random numberGenerator, BinaryReader reader, BinaryWriter writter)
  {
    List<short> money = new List<short>();
    foreach (int offset in currentData)
    {
      bin.Position = offset + 38;
      CheckIfECC();
      money.Add(reader.ReadInt16());
    }
    short[] moneyShuffled = money.ToArray();
    numberGenerator.Shuffle(moneyShuffled);
    for (int i = 0; i < moneyShuffled.Length; i++)
    {
      bin.Position = currentData[i] + 38;
      CheckIfECC();
      writter.Write(moneyShuffled[i]);
    }
  }

  void RandomizeMoney(List<int> currentData, Random numberGenerator, BinaryWriter writter)
  {
    foreach (int offset in currentData)
    {
      bin.Position = offset + 38;
      CheckIfECC();
      if (isHardcore)
      {
        if (tHardcore)
          writter.Write((short)((numberGenerator.Next(135) + 15) * 100));
        else
          writter.Write((short)((numberGenerator.Next(110) + 10) * 100));
      }
      else
        writter.Write((short)((numberGenerator.Next(99) + 1) * 100));
    }
  }

  void RandomizeTech(List<int> currentData, Random numberGenerator, BinaryWriter writter)
  {
    foreach (int offset in currentData)
    {
      int currentOffset = offset, digimonID;
      List<int> attackValues = new List<int>();
      bin.Position = currentOffset;
      digimonID = bin.ReadByte();

      if (digimonID == 40)
        continue;

      for (int i = 0; i < digimonData[digimonID].Attacks.Count; i++)
        if (i != digimonData[digimonID].finisherLocation) attackValues.Add(i);

      for (int i = 0; i < 4; i++)
      {
        if (i == digimonData[digimonID].Attacks.Count)
          break;

        bin.Position = currentOffset + 44 + i * 2;
        CheckIfECC();

        if (digimonData[digimonID].HasFinisher)
        {
          if (i < 3)
          {
            int value = attackValues[numberGenerator.Next(attackValues.Count)];
            writter.Write((short)(0x2E + value));
            attackValues.Remove(value);
            bin.Position = currentOffset + 52 + i * 2;
            CheckIfECC();
            writter.Write((short)30);
          }
          else
          {
            writter.Write((short)(0x2E + digimonData[digimonID].finisherLocation));
            bin.Position = currentOffset + 52 + i * 2;
            CheckIfECC();
            writter.Write((short)10);
          }

        }
        else
        {
          int value = attackValues[numberGenerator.Next(attackValues.Count)];
          writter.Write((short)(0x2E + value));
          attackValues.Remove(value);
          bin.Position = currentOffset + 52 + i * 2;
          CheckIfECC();
          writter.Write((short)30);
        }
      }
    }
  }

  void ShuffleStats(List<int> currentData, Random numberGenerator, BinaryReader reader, BinaryWriter writter)
  {
    List<digimonStatsData> allStats = new List<digimonStatsData>();
    short tempHP, tempMP, tempCHP, tempCMP, tempOff, tempDef, tempSpd, tempBrn;
    foreach (int offset in currentData)
    {
      bin.Position = offset + 22;
      CheckIfECC();
      tempCHP = reader.ReadInt16();
      CheckIfECC();
      tempCMP = reader.ReadInt16();
      CheckIfECC();
      tempHP = reader.ReadInt16();
      CheckIfECC();
      tempMP = reader.ReadInt16();
      CheckIfECC();
      tempOff = reader.ReadInt16();
      CheckIfECC();
      tempDef = reader.ReadInt16();
      CheckIfECC();
      tempSpd = reader.ReadInt16();
      CheckIfECC();
      tempBrn = reader.ReadInt16();

      allStats.Add(new digimonStatsData(tempCHP, tempCMP, tempHP, tempMP,
       tempOff, tempDef, tempSpd, tempBrn));

    }
    digimonStatsData[] statsShuffled = allStats.ToArray();
    numberGenerator.Shuffle(statsShuffled);
    for (int i = 0; i < statsShuffled.Length; i++)
    {
      bin.Position = currentData[i] + 22;
      CheckIfECC();
      writter.Write(statsShuffled[i].currentHP);
      CheckIfECC();
      writter.Write(statsShuffled[i].currentMP);
      CheckIfECC();
      writter.Write(statsShuffled[i].maxHP);
      CheckIfECC();
      writter.Write(statsShuffled[i].maxMP);
      CheckIfECC();
      writter.Write(statsShuffled[i].offense);
      CheckIfECC();
      writter.Write(statsShuffled[i].defense);
      CheckIfECC();
      writter.Write(statsShuffled[i].speed);
      CheckIfECC();
      writter.Write(statsShuffled[i].brains);
    }
  }

  void RandomizeStats(List<int> currentData, Random numberGenerator, BinaryWriter writter, int maxValue, int minValue)
  {
    foreach (int offset in currentData)
    {
      bin.Position = offset + 18;
      CheckIfECC();
      short HP = (short)((numberGenerator.Next(maxValue) + minValue) * 10), MP = (short)((numberGenerator.Next(maxValue) + minValue) * 10);
      int tempMaxValue = maxValue;
      if (maxValue - minValue > 1500)
        tempMaxValue = 1501 - minValue;
      if (HP == 9990)
        HP = 9999;
      if (MP == 9990)
        MP = 9999;
      writter.Write(HP);
      CheckIfECC();
      writter.Write(MP);
      CheckIfECC();
      writter.Write(HP);
      CheckIfECC();
      writter.Write(MP);
      CheckIfECC();
      writter.Write((short)(numberGenerator.Next(maxValue) + minValue));
      CheckIfECC();
      writter.Write((short)(numberGenerator.Next(tempMaxValue) + minValue));
      CheckIfECC();
      writter.Write((short)(numberGenerator.Next(tempMaxValue) + minValue));
      CheckIfECC();
      writter.Write((short)(numberGenerator.Next(tempMaxValue) + minValue));
    }
  }

  void CheckIfECC()
  {
    int position = (int)bin.Position;
    position = position - 24;
    position = position % 0x930;

    if (position >= 0x800)
      bin.Position = bin.Position + 0x130;
  }
  
  /*void CheckIfECC(int pos)
  {   
    pos = pos - 24;
    pos = pos % 0x930;

    if (pos >= 0x800)
      bin.Position = bin.Position + 0x130;
  }*/
}
