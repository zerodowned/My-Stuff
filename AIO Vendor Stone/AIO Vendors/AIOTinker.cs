using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class AIOTinker : BaseAIOVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public AIOTinker() : base( "the tinker" )
		{

			CantWalk = true;
			SetSkill( SkillName.Lockpicking, 60.0, 83.0 );
			SetSkill( SkillName.RemoveTrap, 75.0, 98.0 );
			SetSkill( SkillName.Tinkering, 64.0, 100.0 );
		}
		
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBTinker() );
			//m_SBInfos.Add( new SBSellAll() );
		}

		public AIOTinker( Serial serial ) : base( serial )
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