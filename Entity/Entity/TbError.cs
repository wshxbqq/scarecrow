/*
insert license info here
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed  class TbError
	{

		#region Private Members
		private bool m_isChanged;

		private long m_error_id; 
		private long m_error_task_id; 
		private string m_error_img; 
		private int m_error_x; 
		private int m_error_y; 
		private DateTime m_error_time; 		
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// </summary>
		public TbError()
		{
			m_error_id = 0; 
			m_error_task_id = 0; 
			m_error_img = String.Empty; 
			m_error_x = 0; 
			m_error_y = 0; 
			m_error_time = DateTime.MinValue; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor


		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public long ErrorId
		{
			get { return m_error_id; }
			set
			{
				m_isChanged |= ( m_error_id != value ); 
				m_error_id = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public long ErrorTaskId
		{
			get { return m_error_task_id; }
			set
			{
				m_isChanged |= ( m_error_task_id != value ); 
				m_error_task_id = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public string ErrorImg
		{
			get { return m_error_img; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for ErrorImg", value, "null");
				
				if(  value.Length > 1073741823)
					throw new ArgumentOutOfRangeException("Invalid value for ErrorImg", value, value.ToString());
				
				m_isChanged |= (m_error_img != value); m_error_img = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		public int ErrorX
		{
			get { return m_error_x; }
			set
			{
				m_isChanged |= ( m_error_x != value ); 
				m_error_x = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public int ErrorY
		{
			get { return m_error_y; }
			set
			{
				m_isChanged |= ( m_error_y != value ); 
				m_error_y = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public DateTime ErrorTime
		{
			get { return m_error_time; }
			set
			{
				m_isChanged |= ( m_error_time != value ); 
				m_error_time = value;
			}

		}
			
		/// <summary>
		/// Returns whether or not the object has changed it's values.
		/// </summary>
		public  bool IsChanged
		{
			get { return m_isChanged; }
		}
				
		#endregion 
	}
}
