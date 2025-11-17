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

public class MapData
{
  public List<int> digimonsMapData { get; set; }
  public List<int> scriptOffsets { get; set; }

  public MapData(List<int> digimons, List<int> offsets)
  {
    digimonsMapData = digimons;
    scriptOffsets = offsets;
  }
}
public class AreaData
{
  public List<MapData> digimons { get; set; }

  public AreaData(List<MapData> digimon)
  {
    digimons = digimon;
  }
}

public class BossMapData
{
  public int scriptOffset1 { get; set; }
  public int scriptOffset2 { get; set; }
  public int mapOffset { get; set; }

  public BossMapData(int off1, int off2, int map)
  {
    scriptOffset1 = off1;
    scriptOffset2 = off2;
    mapOffset = map;
  }
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

  bool hardcoreMode = false, trueHardcoreMode = false;

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
                  0x7541946, 0x75A623E, 0x75A629E, 0x75A62FE, 0x76B0F7A, 0x76B0FD4, 0x77489B2, 0x7748A0C, 0x7AFA3C2, 0x7AFA41C, 0x7AFA476, 0x7C6E44E, 0x7D0A3AE, 0x7DF0324,
                  0x7DF037E, 0x7DF03DE, 0x7DF044A, 0x7EA7F7A, 0x7EA7FDA, 0x7EA8040, 0x7EA80B2, 0x7EA8112, 0x7EA8178, 0x7F6E396, 0x7F6E3F6, 0x7F6E462, 0x7F6E4BC, 0x7F6E51C,
                  0x7F6E588, 0x800E866, 0x800E8CC, 0x800E92C, 0x800E992, 0x80D4BDC, 0x80D4D6C, 0x80D4DD8, 0x80D4E44, 0x80D4EA4, 0x80D4F10, 0x819AB92, 0x819ABF2, 0x819AC52,
                  0x819ACB2, 0x819AD12, 0x819AD72, 0x8312968, 0x83129C2, 0x8312A1C, 0x8312A76, 0x8312AD6, 0x86BCF7E, 0x86BCFD8, 0x86BD038, 0x86BD092, 0x87700E6, 0x8770146,
                  0x87701A6, 0x8770206, 0x8836646, 0x88366B2, 0x883671E, 0x883678A, 0x88E959C, 0x88E9602, 0x899CB36, 0x899CB9C, 0x899CC02, 0x899CC68, 0x899CCCE, 0x899CD34,
                  0x8B16304, 0x8B16370, 0x8B163CA, 0x8B16436, 0x8B846D2, 0x8B84738, 0x8B846D4, 0x8B8479E, 0x8B847F8, 0x8B8485E, 0x8D464D2, 0x8D46538, 0x8D46598, 0x8D465F2,
                  0x8D46658, 0x8D466B8, 0x8F3E146, 0x8F3E1B2, 0x8F3E34E, 0x8F3E3BA, 0x9004E22, 0x9004E8E, 0x9004EF4, 0x9004F60, 0x90B824E, 0x90B82B4, 0x90B831A, 0x90B8386,
                  0x90B83EC, 0x90B8452, 0x919D74A, 0x919D7AA, 0x919D80A, 0x919D864, 0x919D8C4, 0x919D924, 0x92475CE, 0x924762E, 0x9247694, 0x92476F4, 0x9303D68, 0x9303DCE,
                  0x9303E2E, 0x9303E94, 0x93BFFF6, 0x93C0056, 0x93C00BC, 0x93C011C, 0x94F2E2A, 0x94F2EA2, 0x94F303E, 0x963383E, 0x9633898, 0x96338F2, 0x96CA9A2, 0x96CAA14,
                  0x96CAA7A, 0x96CAAE6, 0x9774054, 0x97740AE, 0x9774108, 0x97C191E, 0x97C1984, 0x991EF12, 0x991EF6C, 0x991F0FC, 0x991F162, 0x991F1C8, 0x991F222, 0x991F282,
                  0x991F2E8, 0x9C9B44E, 0x9C9B4AE, 0x9C9B69E, 0x9C9B6FE, 0x9EEBC16, 0x9EEBC76, 0x9EEBCD0, 0x9EEBD30, 0x9EEBD90, 0x9EEBDEA, 0x9FAF9BC, 0x9FAFA22, 0x9FAFA88,
                  0x9FAFAE8, 0x9FAFB4E, 0x9FAFCDE, 0xA073802, 0xA073992, 0xA0739F2, 0xA073A52, 0xA073AB8, 0xA073B18, 0xA073B7E, 0xA1376B2, 0xA137712, 0xA137772, 0xA1377D2,
                  0xA137832, 0xA346156, 0xA3461B6, 0xA346216, 0xA346276, 0xA4F0BA8, 0xA5B5E3A, 0xA5B5E94, 0xA5B5EEE, 0xA7EEAD0, 0xA7EEB36, 0xA7EEB96, 0xA7EEBFC, 0xA86D5FE,
                  0xA86D658, 0xA86D6B2, 0xA86D70C, 0xA86D896, 0xA86D8F0, 0xA86D94A, 0xA86D9A4},
  bossOffsets =
  new List<int>(){0x14D3A70, 0x157DE1C, 0x164339A, 0x17C609E, 0x18D6692, 0x1C01852, 0x2133FCA, 0x2134024, 0x213407E, 0x21340D8, 0x2134132, 0x254ABC2, 0x26D5A72, 0x2929254,
                  0x2A3286A, 0x2AF131C, 0x2E3C072, 0x2EA2E6A, 0x2EA2EC4, 0x2EA2F1E, 0x3085C02, 0x3085C5C, 0x314D97A, 0x314D9D4, 0x3214FBA, 0x3215014, 0x32DC392, 0x32DC3EC,
                  0x33A37DA, 0x33A3834, 0x346AF4A, 0x346AFA4, 0x35328BA, 0x3532914, 0x35F9D02, 0x35F9D5C, 0x36C1472, 0x36C14CC, 0x3789012, 0x378919C, 0x3850EBA, 0x3850F14,
                  0x3918E2A, 0x3918E84, 0x4F4D9AE, 0x4F4DA0E, 0x517BBF6, 0x558834A, 0x59E5B6A, 0x5EBEA00, 0x60AC7FE, 0x61EF0C2, 0x8EEBF2E, 0x68440E4, 0x684413E, 0x6844198,
                  0x69CECCA, 0x6FA261C, 0x7195B30, 0x7195B8A, 0x7195BE4, 0x7351FCA, 0x76268AE, 0x7626908, 0x7626962, 0x76269BC, 0x76B102E, 0x76B1088, 0x76B10E2, 0x76B113C,
                  0x780E4D2, 0x7D6A690, 0x800E7FA, 0x831290E, 0x8836592, 0x88365EC, 0x8B162AA, 0x8E50416, 0x8EEBE7A, 0x9303D0E, 0x9303EF4, 0x9858822, 0x9E286E0, 0x9E28740,
                  0xA07377E, 0xA5B5F48, 0xA5B5FA2, 0xA5B5FFC, 0xA668D76, 0xA7EEA76,
                  0x63429FA, 0x6342A54, 0x6342AAE, 0x57EE0C6, 0x57EE120, 0x5860516, 0x5860570, 0x59201CE, 0x5920228 }; //extra maps

  int[] digimonWeights = [57036, 22092, 22904 ,72012, 65948, 93408, 88448, 71416, 89436, 87832, 62548, 84928, 89828, 82544, 52224, 21932, 14984, 72456, 80868, 96288, 91336,
                          84652, 95816, 62504, 39724, 62060, 85392, 91684, 54320, 20232, 34192, 84296, 78064, 90740, 75632, 64976, 96160, 84620, 88516, 70080, 89956, 50228,
                          97820, 24104, 42396, 79840, 70356, 96224, 85916, 85144, 95412, 96352, 87728, 75336, 75444, 76956, 85652, 86248, 81324, 97504, 95964, 65688, 79774,
                          81608, 74804, 98070, 67566, 66320, 40976, 47888, 40896, 43176, 47352, 42320, 44400, 52924, 40084, 39752, 44316, 49928, 55620, 37544, 42228, 40728,
                          44944, 56428, 44712, 51912, 54656, 45368, 21888, 40076, 55800, 44752, 38972, 54304, 49832, 45572, 44944, 39752, 48864, 45940, 61456, 48332, 45404,
                          56720, 44672, 49340, 44472, 37856, 53408, 56724, 38740, 49572, 00000, 79774, 22100, 33176, 27764, 53124, 29824, 30044, 16292, 17676, 25544, 31828,
                          28040, 08148, 46800, 51948, 54856, 41564, 55112, 50508, 21956, 56376, 59040, 47928, 17424, 42264, 33576, 65760, 39476, 50444, 55264, 25040, 16868,
                          24216, 50752, 54220, 21804, 54036, 55740, 27276, 59220, 30056, 25996, 26188, 63380, 28012, 31764, 30500, 60656, 24788, 48176, 32596, 41316, 32996,
                          59688, 37104, 32572, 24408, 45440, 42824, 45260, 49012, 30164, 35148, 24740, 28824];
  List<AreaData> NPCMapData;
  List<MapData> SpecialNPCData;
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
      digimonData[i].dataWeight = digimonWeights[i];
    }

    techData = new List<TechNecessaryData>();

    dataOffset = 0x14D66DF8;
    currentOffset = dataOffset;

    for (int i = 0; i < 57; i++)
    {
      techData.Add(new TechNecessaryData(currentOffset, reader, bin));
      currentOffset = currentOffset + 0x10;
    }

       NPCMapData = new List<AreaData>();

    NPCMapData.AddRange(
    [
      new AreaData([new MapData([0x9C9B69E, 0x9C9B6FE], [0x140B8B77, 0x140B8B7B, 0x140B8B75])]), //MAYO00
      new AreaData([new MapData([0x9C9B44E, 0x9C9B4AE], [0x140B8B85, 0x140B8B89, 0x140B8B83])]), //MAYO00 NIGHT
      new AreaData([new MapData([0x1420C5C, 0x1420CC2, 0x1420DFA], [0x140B73C7, 0x140B73C3, 0x140B73D3, 0x140B73BF]),
      new MapData([0x1420D22, 0x1420D8E], [0x140B73CB, 0x140B73CF, 0x140B73C1])]), //MAYO01
      new AreaData([new MapData([0x1420E66, 0x1420ECC, 0x1420F38], [0x140B73DD, 0x140B73E1, 0x140B73E5, 0x140B73DB])]), //MAYO01 NIGHT
      new AreaData([new MapData([0x14D3A16, 0x14D3ACA], [0x140B741F, 0x140B7423, 0x140B741D])]), //MAYO02
      new AreaData([new MapData([0x14D3B24, 0x14D3B7E], [0x140B743D, 0x140B7441, 0x140B743B])]), //MAYO02 NIGHT
      new AreaData([new MapData([0x157DD56, 0x157DDB6, 0x157DE76, 0x157DED0], [0x140B746B, 0x140B746F, 0x140B7473, 0x140B7477, 0x140B7469])]), //MAYO03
      new AreaData([new MapData([0x16433F4], [0x140B74D3, 0x140B74D1]), new MapData([0x164344E], [0x140B74AF, 0x140B74AD])]), //MAYO04A
      new AreaData([new MapData([0x16434AE, 0x1643508], [0x140B74B9, 0x140B74EB, 0x140B74B7])]), //MAYO04A NIGHT
      new AreaData([new MapData([0x18D6626], [0x140B7583, 0x140B7581])]), //MAYO08A
      new AreaData([new MapData([0x18D66EC], [0x140B758D, 0x140B758B])]), //MAYO08A NIGHT
      new AreaData([new MapData([0x199D106, 0x199D16C], [0x140B75B5, 0x140B75B9, 0x140B75B1]), new MapData([0x199D1D8], [0x140B75BD, 0x140B75B3])]), //MAYO08B
      new AreaData([new MapData([0x199D232, 0x199D298, 0x199D304], [0x140B75C7, 0x140B75CB, 0x140B75CF, 0x140B75C5])]), //MAYO08B NIGHT

      new AreaData([new MapData([0x1BA6246, 0x1BA62AC], [0x140B75F5, 0x140B75F9, 0x140B75F3])]), //TROP00
      new AreaData([new MapData([0x1BA6306, 0x1BA636C], [0x140B7603, 0x140B7607, 0x140B7601])]), //TROP00 NIGHT
      new AreaData([new MapData([0x1C018B2, 0x1C0190C], [0x140B7653, 0x140B7657, 0x140B7651])]), //TROP01, different from vanilla
      new AreaData([new MapData([0x1C01978, 0x1C019D2], [0x140B7661, 0x140B7665, 0x140B765F])]), //TROP01 NIGHT, different from vanilla
      new AreaData([new MapData([0x1CAB7EA, 0x1CAB85C, 0x1CAB8C8], [0x140B768B, 0x140B768F, 0x140B7693, 0x140B7689])]), //TROP02
      new AreaData([new MapData([0x1CAB922], [0x140B769F, 0x140B769B]), new MapData([0x1CABAC4, 0x1CABB30], [0x140B76A3, 0x140B76A7, 0x140B769D])]), //TROP02 NIGHT
      new AreaData([new MapData([0x1D68A54, 0x1D68B08], [0x140B76CD, 0x140B76D1, 0x140B76CB])]), //TROP03
      new AreaData([new MapData([0x1D68B62], [0x140B76DD, 0x140B76D9]), new MapData([0x1D68BBC], [0x140B76E1, 0x140B76DB])]), //TROP03 NIGHT
      new AreaData([new MapData([0x6580D9C], [0x140B771B, 0x140B7719])]), //TROP04, different from vanilla
      new AreaData([new MapData([0x6580E02], [0x140B7725, 0x140B7723])]), //TROP04 NIGHT
      new AreaData([new MapData([0x6633BA6, 0x6633C06], [0x140B787B, 0x140B787F, 0x140B7879]), new MapData([0x6633C72], [0x140B7885, 0x140B7883])]), //TROP05
      new AreaData([new MapData([0x6633CDE, 0x6633D3E, 0x6633DAA], [0x140B788F, 0x140B7893, 0x140B7897, 0x140B788D])]), //TROP05 NIGHT

      new AreaData([new MapData([0x67AC99A], [0x140B7A0B, 0x140B7A09])]), //TUNN01 
      new AreaData([new MapData([0x6843EA6, 0x6844030, 0x684408A], [0x140B7A2F, 0x140B7A33, 0x140B7A37, 0x140B7A2D])]), //TUNN02

      new AreaData([new MapData([0x6B4270E, 0x6B4276E, 0x6B427DA, 0x6B42964], [0x140B7B7B, 0x140B7B7F, 0x140B7B83, 0x140B7B87, 0x140B7B79])]), //DGHA01
      new AreaData([new MapData([0x6B429BE, 0x6B42A1E], [0x140B7B93, 0x140B7B97, 0x140B7B8F]), new MapData([0x6B42A8A, 0x6B42AE4], [0x140B7B9B, 0x140B7B9F, 0x140B7B91])]), //DGHA01 NIGHT
      new AreaData([new MapData([0x6C090E6, 0x6C09146, 0x6C091B2], [0x140B7BFB, 0x140B7BFF, 0x140B7C03, 0x140B7BF9])]), //DGHA02 Soulmon

      new AreaData([new MapData([0x77489B2, 0x7748A0C], [0x140B7F1D, 0x140B7F21, 0x140B7F1B])]), //YAKA02 
      new AreaData([new MapData([0x7AFA3C2, 0x7AFA41C, 0x7AFA476], [0x140B7F97, 0x140B7F9B, 0x140B7F9F, 0x140B7F95])]), //YAKA14
      new AreaData([new MapData([0x7C6E44E], [0x140B7FC1, 0x140B7FBF])]), //YAKA16
      new AreaData([new MapData([0x7D0A3AE], [0x140B7FD5, 0x140B7FD3])]), //YAKA17

      new AreaData([new MapData([0x6CCE592, 0x6CCE5F8], [0x140B7C29, 0x140B7C2D, 0x140B7C27])]), //GCAN01 
      new AreaData([new MapData([0x6CCE658, 0x6CCE6BE], [0x140B7C37, 0x140B7C3B, 0x140B7C35])]), //GCAN01 NIGHT
      new AreaData([new MapData([0x6D9CA06, 0x6D9CA7E, 0x6D9CAF6], [0x140B7C61, 0x140B7C65, 0x140B7C69, 0x140B7C5F])]), //GCAN02 
      new AreaData([new MapData([0x6D9CB6E, 0x6D9CBE6, 0x6D9CC5E], [0x140B7C73, 0x140B7C77, 0x140B7C7B, 0x140B7C71])]), //GCAN02 NIGHT
      new AreaData([new MapData([0x6E0EF4E], [0x140B7CA3, 0x140B7C9F]), new MapData([0x6E0EFBA, 0x6E0F01A], [0x140B7CA7, 0x140B7CAB, 0x140B7CA1])]), //GCAN03 
      new AreaData([new MapData([0x6E0F07A, 0x6E0F0E6], [0x140B7CB5, 0x140B7CB9, 0x140B7CB3])]), //GCAN03 NIGHT
      new AreaData([new MapData([0x7070BC6, 0x7070C2C], [0x140B7D2F, 0x140B7D33, 0x140B7D2D]), new MapData([0x7070C86], [0x140B7D37, 0x140B7D2B])]), //GCAN06 
      new AreaData([new MapData([0x7070CE6, 0x7070D4C], [0x140B7D43, 0x140B7D47, 0x140B7D3F]), new MapData([0x7070DA6], [0x140B7D4B, 0x140B7D41])]), //GCAN06 NIGHT
      new AreaData([new MapData([0x70FE5D6, 0x70FE630], [0x140B7D73, 0x140B7D77, 0x140B7D6F]), new MapData([0x70FE696], [0x140B7D7B, 0x140B7D71])]), //GCAN07 
      new AreaData([new MapData([0x70FE6F0, 0x70FE74A], [0x140B7D87, 0x140B7D8B, 0x140B7D83]), new MapData([0x70FE7B0], [0x140B7D8F, 0x140B7D85])]), //GCAN07 NIGHT
      new AreaData([new MapData([0x7195A22, 0x7195A7C, 0x7195AD6], [0x140B7DB7, 0x140B7DBB, 0x140B7DBF, 0x140B7DB5])]), //GCAN08_1
      new AreaData([new MapData([0xA5B5E3A, 0xA5B5E94, 0xA5B5EEE], [0x140B8EFB, 0x140B8EFF, 0x140B8F03, 0x140B8EF9])]), //GCAN08_2

      new AreaData([new MapData([0x8B16304, 0x8B16370], [0x140B84BD, 0x140B84C1, 0x140B84BB])]), //KODA00 
      new AreaData([new MapData([0x8B163CA, 0x8B16436], [0x140B84CB, 0x140B84CF, 0x140B84C9])]), //KODA00 NIGHT
      new AreaData([new MapData([0x8B846D2, 0x8B8479E, 0x8B8485E, 0x8B848C4], [0x140B84F7, 0x140B84FF, 0x140B850B, 0x140B850F, 0x140B84E1]),
      new MapData([0x8B84738, 0x8B847F8], [0x140B84FB, 0x140B8507, 0x140B84E3])]), //KODA01 (had to mix both day and night)
      new AreaData([new MapData([0x8D464D2, 0x8D46538], [0x140B85A1, 0x140B85A5, 0x140B859D]), new MapData([0x8D46598], [0x140B85A9, 0x140B859F])]), //KODA04 
      new AreaData([new MapData([0x8D465F2, 0x8D46658], [0x140B85B5, 0x140B85B9, 0x140B85B1]), new MapData([0x8D466B8], [0x140B85BD, 0x140B85B3])]), //KODA04 NIGHT
      new AreaData([new MapData([0x8F3E146, 0x8F3E1B2], [0x140B863F, 0x140B8643, 0x140B863D])]), //KODA08 
      new AreaData([new MapData([0x8F3E34E, 0x8F3E3BA], [0x140B864D, 0x140B8651, 0x140B864B])]), //KODA08 NIGHT

      new AreaData([new MapData([0x615FE1E, 0x615FE78], [0x140B78CD, 0x140B78D1, 0x140B78CB]), new MapData([0x6160002, 0x6160062], [0x140B78D7, 0x140B78DB, 0x140B78D5])]), //MIHA00 
      new AreaData([new MapData([0x61600C2, 0x616011C], [0x140B78E5, 0x140B78E9, 0x140B78E3]), new MapData([0x6160176, 0x61601D6], [0x140B78EF, 0x140B78F3, 0x140B78ED])]), //MIHA00 NIGHT 
      new AreaData([new MapData([0x61EF122, 0x61EF182, 0x61EF1E2], [0x140B7927, 0x140B792B, 0x140B792F, 0x140B7925])]), //MIHA01
      new AreaData([new MapData([0x62A221E, 0x62A22F0], [0x140B7943, 0x140B7947, 0x140B7941]), new MapData([0x62A2284, 0x62A235C], [0x140B794D, 0x140B7951, 0x140B794B])]), //MIHA02
      new AreaData([new MapData([0x6407EFA, 0x6407F66], [0x140B7987, 0x140B798B, 0x140B7983]), new MapData([0x6407FD8, 0x640803E], [0x140B798F, 0x140B7993, 0x140B7985])]), //MIHA04A 
      new AreaData([new MapData([0x64080B0, 0x640811C], [0x140B799D, 0x140B79A1, 0x140B799B]), new MapData([0x640818E, 0x64081F4], [0x140B79A7, 0x140B79AB, 0x140B79A5])]), //MIHA04A NIGHT 
      new AreaData([new MapData([0x64BADB6, 0x64BAE22], [0x140B79D3, 0x140B79D7, 0x140B79CF]), new MapData([0x64BAE94, 0x64BAEFA], [0x140B79DB, 0x140B79DF, 0x140B79D1])]), //MIHA04B  
      new AreaData([new MapData([0x64BB09C, 0x64BB108], [0x140B79E9, 0x140B79ED, 0x140B79E7]), new MapData([0x64BB17A, 0x64BB1E0], [0x140B79F3, 0x140B79F7, 0x140B79F1])]), //MIHA04B NIGHT 

      new AreaData([new MapData([0x7541886, 0x75418E6, 0x7541946], [0x140B7E19, 0x140B7E1D, 0x140B7E21, 0x140B7E17])]), //OGRE00
      new AreaData([new MapData([0x75A623E, 0x75A629E], [0x140B7E43, 0x140B7E47, 0x140B7E3F]), new MapData([0x75A62FE], [0x140B7E4B, 0x140B7E41])]), //OGRE01

      new AreaData([new MapData([0x7DF0324, 0x7DF037E], [0x140B8219, 0x140B821D, 0x140B8217])]), //GIAS00 
      new AreaData([new MapData([0x7DF03DE], [0x140B8229, 0x140B8225]), new MapData([0x7DF044A], [0x140B822D, 0x140B8227])]), //GIAS00 NIGHT
      new AreaData([new MapData([0x7EA7F7A, 0x7EA7FDA], [0x140B8253, 0x140B8257, 0x140B8251]), new MapData([0x7EA8040], [0x140B825D, 0x140B825B])]), //GIAS01 
      new AreaData([new MapData([0x7EA80B2, 0x7EA8112], [0x140B8267, 0x140B826B, 0x140B8265]), new MapData([0x7EA8178], [0x140B8271, 0x140B826F])]), //GIAS01 NIGHT 
      new AreaData([new MapData([0x7F6E396, 0x7F6E3F6], [0x140B82AD, 0x140B82B1, 0x140B82AB]), new MapData([0x7F6E462], [0x140B82B5, 0x140B82A9])]), //GIAS02 
      new AreaData([new MapData([0x7F6E4BC, 0x7F6E51C], [0x140B82C1, 0x140B82C5, 0x140B82BD]), new MapData([0x7F6E588], [0x140B82C9, 0x140B82BF])]), //GIAS02 NIGHT 
      new AreaData([new MapData([0x800E866], [0x140B8305, 0x140B8301]), new MapData([0x800E8CC], [0x140B8309, 0x140B8303])]), //GIAS03
      new AreaData([new MapData([0x800E92C, 0x800E992], [0x140B8313, 0x140B8317, 0x140B8311])]), //GIAS03 NIGHT
      new AreaData([new MapData([0x80D4BDC, 0x80D4D6C], [0x140B8357, 0x140B835B, 0x140B8353]), new MapData([0x80D4DD8], [0x140B835F, 0x140B8355])]), //GIAS04 
      new AreaData([new MapData([0x80D4E44, 0x80D4EA4], [0x140B836B, 0x140B836F, 0x140B8367]), new MapData([0x80D4F10], [0x140B8373, 0x140B8369])]), //GIAS04 NIGHT 
      new AreaData([new MapData([0x819AB92, 0x819ABF2], [0x140B839B, 0x140B839F, 0x140B8397]), new MapData([0x819AC52], [0x140B83A3, 0x140B8399])]), //GIAS05 
      new AreaData([new MapData([0x819ACB2, 0x819AD12], [0x140B83AF, 0x140B83B3, 0x140B83AB]), new MapData([0x819AD72], [0x140B83B7, 0x140B83AD])]), //GIAS05 NIGHT 
      new AreaData([new MapData([0x88E959C, 0x88E9602], [0x140B8417, 0x140B841B, 0x140B8415])]), //GIAS07
      new AreaData([new MapData([0x899CB36], [0x140B8443, 0x140B843F]), new MapData([0x899CB9C, 0x899CC02], [0x140B8447, 0x140B844B, 0x140B8441])]), //GIAS08
      new AreaData([new MapData([0x899CC68], [0x140B8457, 0x140B8453]), new MapData([0x899CCCE, 0x899CD34], [0x140B845B, 0x140B845F, 0x140B8455])]), //GIAS08 NIGHT

      new AreaData([new MapData([0x2D4BF48], [0x140B96ED, 0x140B96E9]), new MapData([0x2D4BFA2, 0x2D4C008], [0x140B96F1, 0x140B96F5, 0x140B96EB])]), //GOMI01
      new AreaData([new MapData([0x2D4C062], [0x140B9701, 0x140B96FF]), new MapData([0x2D4C0BC, 0x2D4C122], [0x140B9705, 0x140B9709, 0x140B96FD])]), //GOMI01 NIGHT

      new AreaData([new MapData([0x2497646, 0x24976A6, 0x249770C], [0x140B9093, 0x140B9097, 0x140B909B, 0x140B9091])]), //STIC01
      new AreaData([new MapData([0x249776C, 0x24977CC, 0x2497832], [0x140B90A5, 0x140B90A9, 0x140B90AD, 0x140B90A3])]), //STIC01 NIGHT
      new AreaData([new MapData([0x254AA4E, 0x254AAA8], [0x140B90EF, 0x140B90F3, 0x140B90ED])]), //STIC02 
      new AreaData([new MapData([0x254AB02, 0x254AB62], [0x140B90FD, 0x140B9101, 0x140B90FB])]), //STIC02 NIGHT

      new AreaData([new MapData([0x86BCF7E], [0x140B8679, 0x140B8675]), new MapData([0x86BCFD8], [0x140B867D, 0x140B8677])]), //FRZL01
      new AreaData([new MapData([0x86BD038, 0x86BD092], [0x140B8687, 0x140B868B, 0x140B8685])]), //FRZL01 NIGHT
      new AreaData([new MapData([0x87700E6, 0x8770146], [0x140B86B1, 0x140B86B5, 0x140B86AF])]), //FRZL02
      new AreaData([new MapData([0x87701A6, 0x8770206], [0x140B86BF, 0x140B86C3, 0x140B86BD])]), //FRZL02 NIGHT
      new AreaData([new MapData([0x9004E22, 0x9004E8E], [0x140B86E9, 0x140B86ED, 0x140B86E7])]), //FRZL03
      new AreaData([new MapData([0x9004EF4, 0x9004F60], [0x140B86F7, 0x140B86FB, 0x140B86F5])]), //FRZL03 NIGHT
      new AreaData([new MapData([0x90B824E, 0x90B82B4], [0x140B8723, 0x140B8727, 0x140B8721]), new MapData([0x90B831A], [0x140B872B, 0x140B871F])]), //FRZL04 
      new AreaData([new MapData([0x90B8386, 0x90B83EC, 0x90B8452], [0x140B8735, 0x140B8739, 0x140B873D, 0x140B8733])]), //FRZL04 NIGHT
      new AreaData([new MapData([0x919D74A, 0x919D7AA, 0x919D80A], [0x140B8771, 0x140B8775, 0x140B8779, 0x140B876F])]), //FRZL06 
      new AreaData([new MapData([0x919D864, 0x919D8C4], [0x140B8785, 0x140B8789, 0x140B8781]), new MapData([0x919D924], [0x140B878D, 0x140B8783])]), //FRZL06 NIGHT 
      new AreaData([new MapData([0x92475CE, 0x924762E], [0x140B87B3, 0x140B87B7, 0x140B87B1])]), //FRZL07
      new AreaData([new MapData([0x9247694, 0x92476F4], [0x140B87C1, 0x140B87C5, 0x140B87BF])]), //FRZL07 NIGHT
      new AreaData([new MapData([0x9303D68, 0x9303DCE], [0x140B8845, 0x140B8849, 0x140B8843])]), //FRZL08
      new AreaData([new MapData([0x9303E2E, 0x9303E94], [0x140B8853, 0x140B8857, 0x140B8851])]), //FRZL08 NIGHT
      new AreaData([new MapData([0x93BFFF6, 0x93C0056], [0x140B887D, 0x140B8881, 0x140B887B])]), //FRZL12
      new AreaData([new MapData([0x93C00BC, 0x93C011C], [0x140B888B, 0x140B888F, 0x140B8889])]), //FRZL12 NIGHT
      new AreaData([new MapData([0xA346156, 0xA3461B6], [0x140B8F5F, 0x140B8F63, 0x140B8F5D])]), //FRZL13
      new AreaData([new MapData([0xA346216, 0xA346276], [0x140B8F6D, 0x140B8F71, 0x140B8F6B])]), //FRZL13 NIGHT
      new AreaData([new MapData([0x1E0A2C4], [0x140B900F, 0x140B900D])]), //FRZL16
      new AreaData([new MapData([0x1E0A378], [0x140B9019, 0x140B9017])]), //FRZL16 NIGHT

      new AreaData([new MapData([0x9EEBC16, 0x9EEBC76], [0x140B8C6D, 0x140B8C71, 0x140B8C69]), new MapData([0x9EEBCD0], [0x140B8C75, 0x140B8C6B])]), //MIST01 
      new AreaData([new MapData([0x9EEBD30, 0x9EEBD90], [0x140B8C81, 0x140B8C85, 0x140B8C7D]), new MapData([0x9EEBDEA], [0x140B8C89, 0x140B8C7F])]), //MIST01 NIGHT 
      new AreaData([new MapData([0x9FAF956, 0x9FAF9BC, 0x9FAFA22], [0x140B8CAD, 0x140B8CB1, 0x140B8CB5, 0x140B8CAB])]), //MIST02 FOG
      new AreaData([new MapData([0x9FAFA88, 0x9FAFAE8], [0x140B8CCF, 0x140B8CD3, 0x140B8CCD])]), //MIST02
      new AreaData([new MapData([0x9FAFB4E, 0x9FAFCDE], [0x140B8CDD, 0x140B8CE1, 0x140B8CDB])]), //MIST02 NIGHT
      new AreaData([new MapData([0xA073802, 0xA073992, 0xA0739F2], [0x140B8D2B, 0x140B8D2F, 0x140B8D33, 0x140B8D29])]), //MIST03 FOG
      new AreaData([new MapData([0xA073A52, 0xA073AB8], [0x140B8D4D, 0x140B8D51, 0x140B8D4B])]), //MIST03
      new AreaData([new MapData([0xA073B18, 0xA073B7E], [0x140B8D5B, 0x140B8D5F, 0x140B8D59])]), //MIST03 NIGHT
      new AreaData([new MapData([0xA1376B2], [0x140B8D83, 0x140B8D81])]), //MIST04 FOG
      new AreaData([new MapData([0xA137712], [0x140B8D9F, 0x140B8D9B]), new MapData([0xA137772], [0x140B8DA3, 0x140B8D9D])]), //MIST04
      new AreaData([new MapData([0xA1377D2, 0xA137832], [0x140B8DAD, 0x140B8DB1, 0x140B8DAB])]), //MIST04 NIGHT
      new AreaData([new MapData([0xA7EEAD0, 0xA7EEB36], [0x140B8DFF, 0x140B8E03, 0x140B8DFD])]), //MIST06 
      new AreaData([new MapData([0xA7EEB96], [0x140B8E0F, 0x140B8E0B]), new MapData([0xA7EEBFC], [0x140B8E13, 0x140B8E0D])]), //MIST06 NIGHT
      new AreaData([new MapData([0xA86D5FE, 0xA86D658, 0xA86D6B2], [0x140B8E37, 0x140B8E3B, 0x140B8E3F, 0x140B8E35])]), //MIST07 FOG
      new AreaData([new MapData([0xA86D70C, 0xA86D896], [0x140B8E5B, 0x140B8E5F, 0x140B8E57]), new MapData([0xA86D8F0], [0x140B8E63, 0x140B8E59])]), //MIST07
      new AreaData([new MapData([0xA86D94A, 0xA86D9A4], [0x140B8E6D, 0x140B8E71, 0x140B8E6B])]), //MIST07 NIGHT

      new AreaData([new MapData([0x26198D6, 0x2619936], [0x140B946D, 0x140B9471, 0x140B946B])]), //FACT01
      new AreaData([new MapData([0x2619990, 0x2619B20], [0x140B947B, 0x140B947F, 0x140B9479])]), //FACT01 NIGHT
      new AreaData([new MapData([0x26D58FE, 0x26D595E], [0x140B94C7, 0x140B94CB, 0x140B94C5])]), //FACT02
      new AreaData([new MapData([0x26D59B8, 0x26D5A18], [0x140B94D5, 0x140B94D9, 0x140B94D3])]), //FACT02 NIGHT
      new AreaData([new MapData([0x279B38A], [0x140B9501, 0x140B94FD]), new MapData([0x279B3F0, 0x279B450], [0x140B9505, 0x140B9509, 0x140B94FF])]), //FACT03
      new AreaData([new MapData([0x279B4AA], [0x140B9515, 0x140B9511]), new MapData([0x279B510, 0x279B570], [0x140B9519, 0x140B951D, 0x140B9513])]), //FACT03 NIGHT      
      new AreaData([new MapData([0x2860346, 0x28603A0, 0x2860406], [0x140B95A5, 0x140B95A9, 0x140B95AD, 0x140B95A3])]), //FACT04
      new AreaData([new MapData([0x286046C, 0x28604C6, 0x286052C], [0x140B95B7, 0x140B95BB, 0x140B95BF, 0x140B95B5])]), //FACT04 NIGHT 
      new AreaData([new MapData([0x2860592, 0x28605E6], [0x140B958B, 0x140B9587, 0x140B9585])]), //FACT04 GUARDROMON
      new AreaData([new MapData([0x29924EE, 0x2992548], [0x140B9609, 0x140B960D, 0x140B9607]), new MapData([0x29925AE], [0x140B9611, 0x140B9605])]), //FACT07
      new AreaData([new MapData([0x2992608, 0x2992662, 0x29926C8], [0x140B961B, 0x140B961F, 0x140B9623, 0x140B9619])]), //FACT07 NIGHT 
      new AreaData([new MapData([0x2B7EC76, 0x2B7ED30], [0x140B9681, 0x140B9689, 0x140B967D]), new MapData([0x2B7ECD0], [0x140B9685, 0x140B967F])]), //FACT09
      new AreaData([new MapData([0x2B7ED8A, 0x2B7EDEA, 0x2B7EE50], [0x140B9693, 0x140B9697, 0x140B969B, 0x140B9691])]), //FACT09 NIGHT

      new AreaData([new MapData([0x23FA8A4, 0x23FA8FE, 0x23FA958], [0x140B9271, 0x140B9275, 0x140B9279, 0x140B926F])]), //OMOC05
      new AreaData([new MapData([0x2449580, 0x24495DA], [0x140B92A3, 0x140B92A7, 0x140B92A1])]), //OMOC06
      new AreaData([new MapData([0x2DF66A4, 0x2DF66FE, 0x2DF6758], [0x140B9401, 0x140B9405, 0x140B9409, 0x140B93FF])]), //OMOC07

      new AreaData([new MapData([0x94F2E2A, 0x94F2EA2], [0x140B88B3, 0x140B88B7, 0x140B88AF]), new MapData([0x94F303E], [0x140B88BB, 0x140B88B1])]), //ICSA02
      new AreaData([new MapData([0x963383E, 0x9633898], [0x140B88DF, 0x140B88E3, 0x140B88DB]), new MapData([0x96338F2], [0x140B88E7, 0x140B88DD])]), //ICSA04
      new AreaData([new MapData([0x96CA9A2, 0x96CAA14], [0x140B88FD, 0x140B8901, 0x140B88F9]), new MapData([0x96CAA7A, 0x96CAAE6], [0x140B8905, 0x140B8909, 0x140B88FB])]), //ICSA05
      new AreaData([new MapData([0x9774054], [0x140B891F, 0x140B891B]), new MapData([0x97740AE, 0x9774108], [0x140B8923, 0x140B8927, 0x140B891D])]), //ICSA06
      new AreaData([new MapData([0x97C191E, 0x97C1984], [0x140B893B, 0x140B893F, 0x140B8939])]), //ICSA07

      new AreaData([new MapData([0x2F3084E, 0x2F308A8, 0x2F3095C], [0x140B9741, 0x140B9745, 0x140B974D, 0x140B973D]),
      new MapData([0x2F30902, 0x2F309B6], [0x140B9749, 0x140B9751, 0x140B973F])]), //MGEN01
      new AreaData([new MapData([0x2FBE59E, 0x2FBE5FE, 0x2FBE6CA], [0x140B9767, 0x140B976B, 0x140B9773, 0x140B9763]),
      new MapData([0x2FBE66A, 0x2FBE724], [0x140B976F, 0x140B9777, 0x140B9765])]), //MGEN02
      new AreaData([new MapData([0x4FD240E], [0x140BA0AD, 0x140BA0AB]), new MapData([0x4FD246E, 0x4FD2522, 0x4FD257C], [0x140BA0B1, 0x140BA0B5, 0x140BA0B9, 0x140BA0A9])]), //MGEN03
      new AreaData([new MapData([0x506015E, 0x5060212], [0x140BA0FB, 0x140BA103, 0x140BA0F7]), new MapData([0x50601B8, 0x50602C6], [0x140BA0FF, 0x140BA107, 0x140BA0F9])]), //MGEN04
      new AreaData([new MapData([0x50EDEAE], [0x140BA8DF, 0x140BA8DB]), new MapData([0x50EDF0E, 0x50EDF68], [0x140BA8E3, 0x140BA8E7, 0x140BA8DD])]), //MGEN05
      new AreaData([new MapData([0x5EBE83E, 0x5EBE94C], [0x140BAC37, 0x140BAC43, 0x140BAC33]),
      new MapData([0x5EBE898, 0x5EBE8F2, 0x5EBE9A6], [0x140BAC3B, 0x140BAC3F, 0x140BAC47, 0x140BAC35])]), //MGEN06
      new AreaData([new MapData([0x5F02DCE, 0x5F02E28, 0x5F02E82], [0x140BAC5B, 0x140BAC5F, 0x140BAC63, 0x140BAC59])]), //MGEN07
      new AreaData([new MapData([0x5F90B1E, 0x5F90B78], [0x140BAC79, 0x140BAC7D, 0x140BAC75]), new MapData([0x5F90BD2, 0x5F90C2C], [0x140BAC81, 0x140BAC85, 0x140BAC77])]), //MGEN08
      new AreaData([new MapData([0x601E86E, 0x601E928], [0x140BACC5, 0x140BACCD, 0x140BACC1]), new MapData([0x601E8C8], [0x140BACC9, 0x140BACC3])]), //MGEN09
      new AreaData([new MapData([0x60AC5BE, 0x60AC678, 0x60AC738], [0x140BACF7, 0x140BACFF, 0x140BAD07, 0x140BACF5]),
      new MapData([0x60AC618, 0x60AC6D2, 0x60AC798], [0x140BACFB, 0x140BAD03, 0x140BAD0B, 0x140BACF3])]), //MGEN10

      new AreaData([new MapData([0x56448EE, 0x5644948], [0x140BAA05, 0x140BAA09, 0x140BAA01]), new MapData([0x56449A2, 0x56449FC], [0x140BAA0D, 0x140BAA11, 0x140BAA03])]), //MGEN11
      new AreaData([new MapData([0x56D263E, 0x56D2698], [0x140BAA27, 0x140BAA2B, 0x140BAA23]), new MapData([0x56D26F2, 0x56D274C], [0x140BAA2F, 0x140BAA33, 0x140BAA25])]), //MGEN12
      new AreaData([new MapData([0x576038E, 0x57603E8], [0x140BAA49, 0x140BAA4D, 0x140BAA45]), new MapData([0x5760442, 0x576049C], [0x140BAA51, 0x140BAA55, 0x140BAA47])]), //MGEN13      
    ]); 

    bossMapData = new List<BossMapData>();

    bossMapData.AddRange([new BossMapData(0x140B7637, 0x140B7639, 0x1C01852), new BossMapData(0x140B94AB, 0x140B94AD, 0x26D5A72),
    new BossMapData(0x140B791B, 0x140B791D, 0x61EF0C2), new BossMapData(0x140B7D05, 0x140B7D07, 0x6FA261C), new BossMapData(0x140B745F, 0x140B7461, 0x157DE1C),
    new BossMapData(0x140B8495, 0x140B84A3, 0x8B162AA), new BossMapData(0x140B82E7, 0x140B82E9, 0x800E7FA), new BossMapData(0x140B87E3, 0x140B87E5, 0x9303D0E),
    new BossMapData(0x140B8DE3, 0x140B8DE5, 0xA7EEA76), new BossMapData(0x140B8D0D, 0x140B8D0F, 0xA07377E), new BossMapData(0x140B81F1, 0x140B81F3, 0x831290E),
    new BossMapData(0x140BACEB, 0x140BACED, 0x60AC7FE), new BossMapData(0x140BA973, 0x140BA975, 0x517BBF6), new BossMapData(0x140BA9ED, 0x140BA9EF, 0x59E5B6A),
    new BossMapData(0x140B85DD, 0x140B85DF, 0x8E50416)]);


    bin.Position = 0x6580D42;
    if (bin.ReadByte() == 0x80)
      return;
    else
    {
      hardcoreMode = true;
      bossOffsets.AddRange([0x1D68AAE, 0x2E3C0CC, 0x4F4DA6E, 0x5A8FA32, 0x5B399C6, 0x6580D42, 0x66F9FCA, 0x66FA024, 0x66FA07E, 0x6C08F5A, 0x6ED3D42, 0x76B1196, 0x7F6E32A,
                            0x80D4B82, 0x8A6330E, 0x8EEBED4, 0x94851F6, 0x9AA152A, 0x9AA1584, 0x9AA15DE, 0x9B3AAC6, 0x9B3AB20, 0x9B3AB7A, 0x9BBF4FE]);

      bin.Position = 0x723F7EE;
      if (bin.ReadByte() != 0x2F)
        return;
      else
      {
        trueHardcoreMode = true;
        bossOffsets.AddRange([0x1EC08C6, 0x1F404D2, 0x20C7328, 0x23A3066, 0x723F7EE, 0x740DEC6, 0x740DF20, 0x7D6A636, 0x825FCA2, 0x825FCFC, 0x991F282, 0x991F2E8, 0xA40BA06,
                              0xA40BA60, 0xA40BABA]);
      }
    }

    int monoOffset1, monoOffset2;
    if (trueHardcoreMode)
    {
      monoOffset1 = 0x140B7DE5;
      monoOffset2 = 0x140B7DE9;
    }
    else
    {
      monoOffset1 = 0x140B7DE7;
      monoOffset2 = 0x140B7DEB;
    }

    SpecialNPCData = new List<MapData>();
    SpecialNPCData.AddRange(
    [
      new MapData([0x6C08FBA, 0x6C0901A, 0x6C0907A], [0x140B7BE1, 0x140B7BE5, 0x140B7BE9, 0x140B7BDF]), //DGHA02 Tsukaimon
      new MapData([0x6ED3D9C], [0x140B7CE7, 0x140B7CE5]), //GCAN04
      new MapData([0xA4F0BA8], [0x140B8ED9, 0x140B8EBD]), //GCAN04_2
      new MapData([0x6CCE592, 0x6CCE5F8], [0x140B7DE3, monoOffset1, monoOffset2, 0x140B7DE5]), //GCAN09 
      new MapData([0x723F728, 0x723F788], [0x140B7DF5, 0x140B7DF9, 0x140B7DF3]), //GCAN09 NIGHT
      new MapData([0x8836646, 0x88366B2], [0x140B83DD, 0x140B83E1, 0x140B83DB]), //GIAS06A 
      new MapData([0x883671E, 0x883678A], [0x140B83EB, 0x140B83EF, 0x140B83E9]), //GIAS06A NIGHT 
      new MapData([0x991EF12, 0x991EF6C, 0x991F0FC], [0x140B8981, 0x140B8985, 0x140B8989, 0x140B897D]), //BETL01 Tentomon
      //new MapData([0x991F162, 0x991F1C8, 0x991F222], [0x140B898D, 0x140B8AC9, 0x140B8ACD, 0x140B8997]), //BETL01 Dokunemon
      new MapData([0x2C15F42, 0x2C15F9C, 0x2C15FF6], [0x140B96AD, 0x140B96AF, 0x140B96B3, 0x140B96B7]), //FACT10
      new MapData([0x8312968, 0x83129C2, 0x8312A1C, 0x8312A76, 0x8312AD6], [0x140B81D1, 0x140B81D5, 0x140B81D9, 0x140B81DD, 0x140B81E1, 0x140B81CF]), //SAIB02      
    ]);
   
  }


  public void RandomizeDigimonNPC(int option, bool bosses, Random numberGenerator, BinaryWriter writter)
  {
    List<byte> validNPCDigimon = new List<byte>();
    List<byte> extraNPCDigimon = new List<byte>();
    switch (option)
    {
      case 0:
        for (int i = 66; i < 112; i++)
          validNPCDigimon.Add((byte)i);
        break;
      case 1:
        for (int i = 1; i < digimonData.Count; i++)
        {
          if (i < 114 || (i > 128 && digimonData[i].HasFinisher))
          {
            if (i == 62)
                continue;
            validNPCDigimon.Add((byte)i);
            extraNPCDigimon.Add((byte)i);
          }
        }
        extraNPCDigimon.Add(115);
        break;
    }

    foreach (AreaData mapData in NPCMapData)
    {
      if (mapData.digimons.Count == 1)
      {
        byte randomDigimon;
        if (option == 1 && mapData.digimons[0].digimonsMapData.Count < 2)
          randomDigimon = extraNPCDigimon[numberGenerator.Next(extraNPCDigimon.Count)];
        else
          randomDigimon = validNPCDigimon[numberGenerator.Next(validNPCDigimon.Count)];
        RandomizeMapDigi(mapData.digimons[0].digimonsMapData, randomDigimon, numberGenerator, writter, mapData.digimons[0].digimonsMapData.Count != 1);
        RandomizeScriptDigi(mapData.digimons[0].scriptOffsets, randomDigimon);
      }
      else if (mapData.digimons.Count == 2)
      {
        byte randomDigimon = validNPCDigimon[numberGenerator.Next(validNPCDigimon.Count)];
        int totalSize = digimonData[randomDigimon].dataWeight;
        byte randomDigimon2 = validNPCDigimon[numberGenerator.Next(validNPCDigimon.Count)];
        if (totalSize + digimonData[randomDigimon2].dataWeight > 152000)
        {
          while (totalSize + digimonData[randomDigimon2].dataWeight > 152000)
            randomDigimon2 = validNPCDigimon[numberGenerator.Next(validNPCDigimon.Count)];
        }

        RandomizeMapDigi(mapData.digimons[0].digimonsMapData, randomDigimon, numberGenerator, writter, true);
        RandomizeScriptDigi(mapData.digimons[0].scriptOffsets, randomDigimon);
        RandomizeMapDigi(mapData.digimons[1].digimonsMapData, randomDigimon2, numberGenerator, writter, true);
        RandomizeScriptDigi(mapData.digimons[1].scriptOffsets, randomDigimon2);
      }
      else
        continue;

    }

    foreach (MapData mapData in SpecialNPCData)
    {     
      byte randomDigimon = validNPCDigimon[numberGenerator.Next(validNPCDigimon.Count)];
      if (digimonData[randomDigimon].dataWeight > 54000)
      {
        while (digimonData[randomDigimon].dataWeight > 54000)
          randomDigimon = validNPCDigimon[numberGenerator.Next(validNPCDigimon.Count)];
      }  
      RandomizeMapDigi(mapData.digimonsMapData, randomDigimon, numberGenerator, writter, mapData.digimonsMapData.Count != 1);
      RandomizeScriptDigi(mapData.scriptOffsets, randomDigimon);
    }
    

    if (bosses)
    {
      List<byte> validDigimon = new List<byte>();
      for (int i = 0; i < digimonData.Count; i++)
      {
        if (digimonData[i].HasFinisher && (i < 67 || i > 128))
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
        bin.WriteByte(randomDigimon);

        RandomizeTechSingle(mapData.mapOffset, numberGenerator, writter, false);
      }

      List<MapData> extraBosses = [new MapData([0x57EE0C6, 0x57EE120],[0x140187A1, 0x140187B3, 0x140187B9]), new MapData([0x5860516, 0x5860570],[0x140223DD, 0x140223EF, 0x140223F5]),
      new MapData([0x59201CE, 0x5920228],[0x1401A339, 0x1401A34B, 0x1401A351]),
      new MapData([0x3085C02, 0x314D97A, 0x3214FBA, 0x32DC392, 0x33A37DA, 0x346AF4A, 0x35328BA, 0x35F9D02, 0x36C1472, 0x3789012, 0x3850EBA, 0x3918E2A],[0x1405B46D, 0x1405B46F]),
      new MapData([0x3085C5C, 0x314D9D4, 0x3215014, 0x32DC3EC, 0x33A3834, 0x346AFA4, 0x3532914, 0x35F9D5C, 0x36C14CC, 0x378919C, 0x3850F14, 0x3918E84],[0x1405B8DB, 0x1405B8DD]) ];

      foreach (MapData mapData in extraBosses)
      {
        byte randomDigimon = validDigimon[numberGenerator.Next(validDigimon.Count)];
        RandomizeMapDigi(mapData.digimonsMapData, randomDigimon, numberGenerator, writter, false);
        RandomizeScriptDigi(mapData.scriptOffsets, randomDigimon);
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
            byte attack = digimonData[Digimon].Attacks[j];
            if (attack < 57 && attack != 21 && attack != 30 && attack != 34 && attack != 41 && attack != 42)
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
        [0x13FD73E0, 0x13FD73F0, 0x13FD748E, 0x14016CB6, 0x140172B6, 0x1409FFD6, 0x140B8560, 0x140B8570, 0x140B860E],//Tyrannomon
        [0x13FD696A, 0x13FD6998, 0x13FF1A6C, 0x13FF44D8, 0x13FF572E, 0x13FF5740, 0x13FF5886, 0x140A009A, 0x140B7AEA, 0x140B7B18],//Meramon
        [0x13FE1D48, 0x13FE19B2, 0x14059D08, 0x140A104A, 0x14D725F0],//Seadramon
        [0x13FD9780, 0x13FD9794, 0x14073C7C, 0x140503F8, 0x14050CAC, 0x14074630, 0x140A0F72, 0x140BA900, 0x140BA914],//Numemon
        [0x14078704],//MetalGreymon
        [0x13FD6792, 0x13FED052, 0x140A0E94, 0x140B7912],//Mamemon
        [0x13FD8E96, 0x14046696, 0x14047768, 0x14061FA8, 0x140BA016],//Monzaemon
        [0x13FD7C5A, 0x140360A2, 0x140A0B80, 0x140B8DDA],//Gabumon
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
        [0x13FD689E, 0x13FE2BC6, 0x13FF1972, 0x13FF0F98, 0x13FEFDA6, 0x13FEFD12, 0x140B7A1E],//Ogremon
        [0x13FF8018, 0x13FD6A34, 0x1409FD2E, 0x140B7BB4, 0x14D725CC],//Bakemon
        [0x13FD69B8, 0x13FE2BB6, 0x13FEFD96, 0x13FF6080, 0x13FF6092, 0x13FF6456, 0x140A0A9C, 0x140B7B38],//Drimogemon
        [0x14057792, 0x1405779C],//Sukamon
        [0x1404F994, 0x1404FAD8],//Andromon
        [0x13FD84B8, 0x14052A7A, 0x140A1240, 0x140B9638],//Giromon
        [0x13FD630C, 0x13FD6348, 0x13FD6362, 0x13FDF1CC, 0x13FDF210, 0x13FDF54E, 0x140A1776, 0x140B748C, 0x140B74C8, 0x140B74E2],//Etemon
        [0x13FD71AC, 0x1400EFFC, 0x140150C0, 0x1400F148, 0x1400F292, 0x1400F50C, 0x1400F914, 0x140A0732, 0x140B832C],//Biyomon           
        [0x13FD6D72, 0x13FFFC7C, 0x13FFFC96, 0x14000C52, 0x140A05B0, 0x140B7EF2, ],//Monochromon
        [0x13FD72F4, 0x13FD7AA2, 0x14012928, 0x140122D6, 0x140B8474, 0x140B8C22],//Leomon
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
        [0x14010D70],//H-Kabuterimon Arena
    ];

    List<short> triggers = new List<short>() {204, 206, 207, 208, 209, 210, 211, 212, 213, 214, 217, 218, 219, 220, 222, 223, 226, 227, 228, 231, 232, 233, 234, 237, 238, 239,
                                              240, 241, 242, 245, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260};

    uint[] prosperityOffsets = { 0x13FDE359, 0x13FDEB91, 0x13FDF553, 0x13FE08F7, 0x13FE1D4D, 0x13FE4B1B, 0x13FE6B57, 0x13FE88E5, 0x13FE909F, 0x13FEB641, 0x13FED04F, 0x13FF1977,
                                 0x13FF5733, 0x13FF645B, 0x13FF801D, 0x13FFFAE1, 0x14000C4F, 0x1400BA91, 0x1400DD0F, 0x1400E6DB, 0x1400F919, 0x1401292D, 0x140172BB, 0x1401F63B,
                                 0x14020E89, 0x1402950D, 0x1402A747, 0x1402BAA7, 0x140304F7, 0x140360A7, 0x140396B5, 0x1403AAFB, 0x1403CE8D, 0x14040D69, 0x140450B3, 0x1404776D,
                                 0x1404CF43, 0x1404FADD, 0x14052A7F, 0x140577A1, 0x1405B8CD, 0x14074635, 0x1407750F, 0x140788F7, 0x1407B9B3, 0x1407C325, 0x14081CC9, 0x14096849 };


    if (hardcoreMode)
    {
      prosperityOffsets[3] = 0x13FE0891; //Coelamon
      prosperityOffsets[9] = 0x13FEB63F; //Centarumon
      prosperityOffsets[14] = 0x13FF8017; //Bakemon

      recruitTriggers[23][0] = 0x13FF8012; //Bakemon
      recruitTriggers[33][0] = 0x13FE088C; //Coelamon
      if (trueHardcoreMode)
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
        SetProsperity(prosperityOffsets, shuffledProsperity, hardcoreMode);
        break;
      case 1:
        byte[] prosperity = { 10, 7, 5, 5, 5, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        numberGenerator.Shuffle(prosperity);
        SetProsperity(prosperityOffsets, prosperity, hardcoreMode);
        triggers.AddRange([203, 205, 221, 224, 225, 235, 246, 261, 236]);
        recruitTriggers.AddRange(
          [[0x13FD79D2, 0x13FFF39C, 0x13FFF3B8, 0x1402BAA2, 0x1402BFDE, 0x140B8B52], // Agumon
            [0x1405B446, 0x1405B8C8, 0x1409E4BA, 0x1409E4D6, 0x140A092C] , // Greymon
            [0x1403A5A2, 0x1403AAF6, 0x140A0306, 0x14D725C6], // Birdramon
            [0x13FD7E6C, 0x1403E676, 0x140441D4, 0x14044250, 0x140450AE, 0x14059D10, 0x14059F08, 0x1406193E, 0x140B8FEC], // Whamon
            [0x13FE7AD8, 0x13FE7BF2, 0x13FE88E0, 0x1405ACC8, 0x1405AD10, 0x1405AF5A, 0x1409FDD0], // Vegiemon
            [0x13FD6B56, 0x13FD7D4C, 0x13FFA106, 0x13FFF3C4, 0x140396B0, 0x140A03A6, 0x140B7CD6, 0x140B8ECC], // Shellmon    
            [0x13FD62D6, 0x13FDEB8C, 0x140A21C0, 0x140B7456],//Palmon        
            [0x14045932]]); // MegaSeadramon Arena

        if (hardcoreMode)
          recruitTriggers.Add([0x13FE9770, 0x13FE9FC2, 0x13FEA132, 0x13FEA3D4, 0x13FEA49A, 0x13FEA560, 0x13FEA626, 0x13FEA710, 0x13FEA92A, 0x13FEB63A, 0x13FEB986, 0x13FEBDE0,
          0x13FEAA28, 0x13FEAB12, 0x13FEAC10, 0x13FEAD0E, 0x13FEAE0C, 0x13FEAEDE, 0x13FEAFDC, 0x13FEB1DE, 0x13FEB2CC, 0x13FEB3BA, 0x13FEB4AC, 0x13FEB7F8, 0x13FEBC52, 0x1409FB24]);
        else
          recruitTriggers.Add([0x13FE9770, 0x13FE9FC2, 0x13FEA132, 0x13FEA3D4, 0x13FEA49A, 0x13FEA560, 0x13FEA626, 0x13FEA710, 0x13FEA92A, 0x13FEB63C, 0x13FEB988, 0x13FEBDE2,
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

  void RandomizeTechSingle(int currentData, Random numberGenerator, BinaryWriter writter, bool AllRange)
  {
      int currentOffset = currentData, digimonID;
      List<int> attackValues = new List<int>();
      bin.Position = currentOffset;
      digimonID = bin.ReadByte();

    for (int i = 0; i < digimonData[digimonID].Attacks.Count; i++)
    {
      if (i != digimonData[digimonID].finisherLocation)
      {
        if (!AllRange)
          attackValues.Add(i);
        else if (digimonData[digimonID].Attacks[i] != 25)
          attackValues.Add(i);
      }
    }

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
      bin.Position = offset + 22;
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

  void RandomizeMapDigi(List<int> data, byte digimon, Random numberGenerator, BinaryWriter writter, bool allRange)
  {
    foreach (int off in data)
    {
      bin.Position = off;
      bin.WriteByte(digimon);
      RandomizeTechSingle(off, numberGenerator, writter, allRange);
    }
  }

  void RandomizeScriptDigi(List<int> data, byte digimon)
  {
    foreach (int off in data)
    {
      bin.Position = off;
      bin.WriteByte(digimon);
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
