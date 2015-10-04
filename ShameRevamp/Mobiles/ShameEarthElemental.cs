using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	 [CorpseName( "an earth elemental corpse" )]
	 public class ShameEarthElemental : BaseCreature
	 {
		  //public override double DispelDifficulty{ get{ return 117.5; } }
		  //public override double DispelFocus{ get{ return 45.0; } }

		  [Constructable]
		  public ShameEarthElemental () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
			  {
				   Name = "an earth elemental";
				   Body = 14;
				   BaseSoundID = 268;

				   SetStr( 130, 155 );
				   SetDex( 65, 75 );
				   SetInt( 75, 95 );

				   SetHits( 330, 350 );

				   SetDamage( 11, 13 );

				   SetDamageType( ResistanceType.Physical, 100 );

				   SetResistance( ResistanceType.Physical, 30, 35 );
				   SetResistance( ResistanceType.Fire, 25, 30 );
				   SetResistance( ResistanceType.Cold, 25, 30 );
				   SetResistance( ResistanceType.Poison, 25, 30 );
				   SetResistance( ResistanceType.Energy, 20, 25 );

				   SetSkill( SkillName.MagicResist, 65.0, 85.0 );
				   SetSkill( SkillName.Tactics, 65.0, 90.0 );
				   SetSkill( SkillName.Wrestling, 80.0, 85.0 );

				   Fame = 3500;
				   Karma = -3500;

				   VirtualArmor = 34;
				   //ControlSlots = 2;

				   //PackItem( new FertileDirt( Utility.RandomMinMax( 1, 4 ) ) );
				   //PackItem( new IronOre( 3 ) ); // TODO: Five small iron ore
				   //PackItem( new MandrakeRoot() );
				   // shame crystal
			  }

		  public override void GenerateLoot()
			  {
			   AddLoot( LootPack.Average );
			   AddLoot( LootPack.Meager );
			   AddLoot( LootPack.Gems );
			  }
			  
	/*	public override void OnDeath(Container c)
			{
				base.OnDeath(c);
 
					if (Utility.RandomDouble() < 0.30)
						{
						switch (Utility.Random(1))
							{
							case 0: c.DropItem(new ShameCrystal()); break;
 
							}
						}
			}  
	*/
		  public override bool BleedImmune{ get{ return true; } }
		  public override int TreasureMapLevel{ get{ return 1; } }

		  public ShameEarthElemental ( Serial serial ) : base( serial )
			  {
			  }

		  public override void Serialize( GenericWriter writer )
			  {
			   base.Serialize( writer );
			   writer.Write( (int) 0 );
			  }

		  public override void Deserialize( GenericReader reader )
			  {
			   base.Deserialize( reader );
			   int version = reader.ReadInt();
			  }
	 }
}