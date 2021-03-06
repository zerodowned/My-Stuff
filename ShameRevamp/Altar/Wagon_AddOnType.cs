
using System;
using Server;
using Server.Multis;
using Server.Mobiles;
using Server.Items;
using Server.Network;



namespace Server.Items
{
	public class Wagon_AddOnType : Item
	{
		
		private Mobile m_Owner;
		int mOwnerBodyValue;
		
		 private StonePaversDark m_FrontWall;
		 private StonePaversDark m_BackWall;
		
		int mfrontX, mfrontY, mfrontZ, mbackX, mbackY, mbackZ;
		
		int mID;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
        {
            get { return this.m_Owner; }
            set { this.m_Owner = value; }
        }

		[CommandProperty(AccessLevel.GameMaster)]
        public int OwnerBodyValue 
		{
			get { return mOwnerBodyValue; } 
			set { mOwnerBodyValue = value;  } 
		}
		
		
		[CommandProperty(AccessLevel.GameMaster)]
        public int ID { get; set; }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public StonePaversDark FrontWall { get; set; }
		[CommandProperty( AccessLevel.GameMaster )]
		public StonePaversDark BackWall { get; set; }
		
		
	
		[Constructable]
		public Wagon_AddOnType( ) : base(  )
		{
			Name = "Wagon_AddOnType";
			ItemID = 1295;
			Timer.DelayCall(TimeSpan.Zero, AddItems);
			
		}
		
		public void AddItems()
		{
			this.GetWorldLocation();
			
			StonePaversDark first = new StonePaversDark();
			first.MoveToWorld(new Point3D(this.X, this.Y - 1, this.Z), this.Map);
			FrontWall = first;
			first.Hue = 10;
			
			StonePaversDark second = new StonePaversDark();
            second.MoveToWorld(new Point3D(this.X, this.Y + 1, this.Z), this.Map);
			BackWall = second;
		}
		
		
		public override bool HandlesOnMovement
        {
            get  { return true;  }
        }
	
		public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);
			
			// null check, prevents crashes
			if (m_Owner == null) { return;}
			
			// is the owner moves, this item's location will change to the location of the owner
			if( m_Owner.Location != this.Location )
			{
				this.Location = m_Owner.Location;
				this.Map = m_Owner.Map;
				
				if(Owner.Direction == Direction.West) {
					FrontWall.X = m_Owner.X - 1; FrontWall.Y = m_Owner.Y; FrontWall.Z = m_Owner.Z;
					BackWall.X = m_Owner.X + 1; BackWall.Y = m_Owner.Y; BackWall.Z = m_Owner.Z;
				}
				else if(Owner.Direction == Direction.South) {
					FrontWall.X = m_Owner.X; FrontWall.Y = m_Owner.Y + 1; FrontWall.Z = m_Owner.Z;
					BackWall.X = m_Owner.X ; BackWall.Y = m_Owner.Y - 1; BackWall.Z = m_Owner.Z;
				}
				else if(Owner.Direction == Direction.East) {
					FrontWall.X = m_Owner.X + 1; FrontWall.Y = m_Owner.Y; FrontWall.Z = m_Owner.Z;
					BackWall.X = m_Owner.X - 1; BackWall.Y = m_Owner.Y; BackWall.Z = m_Owner.Z;
				}
				else if(Owner.Direction == Direction.North) {
					FrontWall.X = m_Owner.X; FrontWall.Y = m_Owner.Y - 1; FrontWall.Z = m_Owner.Z;
					BackWall.X = m_Owner.X; BackWall.Y = m_Owner.Y + 1; BackWall.Z = m_Owner.Z;
				}
				else if(Owner.Direction == Direction.Right) {
					FrontWall.X = m_Owner.X + 1; FrontWall.Y = m_Owner.Y - 1; FrontWall.Z = m_Owner.Z;
					BackWall.X = m_Owner.X -1; BackWall.Y = m_Owner.Y + 1; BackWall.Z = m_Owner.Z;
				}
				else if(Owner.Direction == Direction.Left) {
					FrontWall.X = m_Owner.X - 1; FrontWall.Y = m_Owner.Y + 1; FrontWall.Z = m_Owner.Z;
					BackWall.X = m_Owner.X + 1; BackWall.Y = m_Owner.Y - 1; BackWall.Z = m_Owner.Z;
				}
				else if(Owner.Direction == Direction.Mask) {
					FrontWall.X = m_Owner.X - 1; FrontWall.Y = m_Owner.Y - 1; FrontWall.Z = m_Owner.Z;
					BackWall.X = m_Owner.X + 1; BackWall.Y = m_Owner.Y + 1; BackWall.Z = m_Owner.Z;
				}
				else if(Owner.Direction == Direction.Down) {
					FrontWall.X = m_Owner.X + 1; FrontWall.Y = m_Owner.Y + 1; FrontWall.Z = m_Owner.Z;
					BackWall.X = m_Owner.X - 1; BackWall.Y = m_Owner.Y - 1; BackWall.Z = m_Owner.Z;
				}
			}
			
			if ( m_Owner.Location != Location ) { return; }
			
		}
		
	

		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;
			

				// sets owner on doubleclick
				if( m_Owner == null )
				{
					this.Owner = from;
					OwnerBodyValue = from.Body;
					from.BodyMod = 803;
					from.Send(SpeedControl.WalkSpeed);
   
					return;
				}
				// removes owner
				else if( this.Owner == from )
				{
					from.Send(new UpdateStatueAnimation(pm, 0, 0, 0));
					m_Owner = null;
					from.BodyMod = 0;
					from.Send(SpeedControl.Disable);
				}
				
				else
				{
					pm.SendMessage( "You cannot use that right now." );
				}
		
		}
		
		
		public override bool OnDragLift( Mobile from )
		{
			return ( from.AccessLevel >= AccessLevel.Counselor );
		}
		
		public void RemoveBodyMod ( Mobile m )
		{
			m.BodyMod = 0;
		}
	
		public override void Delete( )
		{
			
			if( Owner != null ) { RemoveBodyMod( Owner ); }
			if( FrontWall != null ) { FrontWall.Delete(); }
			if( BackWall != null ) { BackWall.Delete(); }
			
			base.Delete( );
		}




		public Wagon_AddOnType( Serial serial ) : base( serial ) {}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )0 ); // version

			writer.Write( ( Mobile )m_Owner);
			
			writer.Write((Item)m_FrontWall);
			writer.Write((Item)m_BackWall);
			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt( );

			m_Owner = ( Mobile )reader.ReadMobile( );
			
			m_FrontWall = ( StonePaversDark )reader.ReadItem( );
			m_BackWall = ( StonePaversDark )reader.ReadItem( );
		}
		
	
	}
		
}