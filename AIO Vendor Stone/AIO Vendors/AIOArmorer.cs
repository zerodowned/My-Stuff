using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class AIOArmorer : BaseAIOVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public AIOArmorer() : base( "the armorer" )
		{
			CantWalk = true;
			SetSkill( SkillName.ArmsLore, 64.0, 100.0 );
			SetSkill( SkillName.Blacksmith, 60.0, 83.0 );
		}
		
		public override void InitSBInfo()
		{
			switch ( Utility.Random( 4 ))
			{
				case 0:
				{
					m_SBInfos.Add( new SBLeatherArmor() );
					m_SBInfos.Add( new SBStuddedArmor() );
					m_SBInfos.Add( new SBMetalShields() );
					m_SBInfos.Add( new SBPlateArmor() );
					m_SBInfos.Add( new SBHelmetArmor() );
					m_SBInfos.Add( new SBChainmailArmor() );
					m_SBInfos.Add( new SBRingmailArmor() );
					//m_SBInfos.Add( new SBSellAll() );
					break;
				}
				case 1:
				{
					m_SBInfos.Add( new SBStuddedArmor() );
					m_SBInfos.Add( new SBLeatherArmor() );
					m_SBInfos.Add( new SBMetalShields() );
					m_SBInfos.Add( new SBHelmetArmor() );
					//m_SBInfos.Add( new SBSellAll() );
					break;
				}
				case 2:
				{
					m_SBInfos.Add( new SBMetalShields() );
					m_SBInfos.Add( new SBPlateArmor() );
					m_SBInfos.Add( new SBHelmetArmor() );
					m_SBInfos.Add( new SBChainmailArmor() );
					m_SBInfos.Add( new SBRingmailArmor() );
					//m_SBInfos.Add( new SBSellAll() );
					break;
				}
				case 3:
				{
					m_SBInfos.Add( new SBMetalShields() );
					m_SBInfos.Add( new SBHelmetArmor() );
					//m_SBInfos.Add( new SBSellAll() );
					break;
				}
			}
			if ( IsTokunoVendor )
			{
				m_SBInfos.Add( new SBSELeatherArmor() );	
				m_SBInfos.Add( new SBSEArmor() );
			
			}
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.Boots; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.HalfApron( Utility.RandomYellowHue() ) );
			AddItem( new Server.Items.Bascinet() );
		}

		public AIOArmorer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}