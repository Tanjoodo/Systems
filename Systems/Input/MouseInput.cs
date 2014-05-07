using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Systems.Input
{
	/// <summary>
	/// Class that handles mouse input
	/// </summary>
	public class MouseInput
	{
		const int NUMBER_OF_BUFFERS = 3;

		Vector2 mouse_position;
		Vector2 prev_mouse_position;
		MouseState prev_mouse_state;

		public enum Buttons { LeftButton, RightButton, MiddleButton }

		List<Vector2> left_button_buffer = new List<Vector2>();
		List<Vector2> right_button_buffer = new List<Vector2>();
		List<Vector2> middle_button_buffer = new List<Vector2>();
		/// <summary>
		/// Creates a new instance of MouseInput
		/// </summary>
		//TODO: don't record mouse events if window is not in focus
		public MouseInput(bool OnlyWorkWhenInFocus = true)
		{
			mouse_position = Vector2.Zero; 
		}

		/// <summary>
		/// Gets the absolute mouse position since the last Update().
		/// </summary>
		/// <returns>Returns a Vector2 containing the mouse position.</returns>
		public Vector2 GetMousePosition()
		{
			return mouse_position;
		}

		/// <summary>
		/// Gets the relative mouse position from a specific point.
		/// </summary>
		/// <param name="Point">A point in the form of a Vector2.</param>
		/// <returns>Returns a Vector2 containing the relative position between the mouse cursor and the specified point.</returns>
		public Vector2 GetRelativePosition(Vector2 Point)
		{
			return new Vector2(mouse_position.X - Point.X, mouse_position.Y - Point.Y);
		}

		/// <summary>
		/// Gets the relative mouse position from the center of a bounding box.
		/// </summary>
		/// <param name="BoundingBox">Bounding box of an object in the form of a Rectangle.</param>
		/// <returns>Returns a Vector2 containing the relative position between the mouse cursor and the center of the specified bounding box.</returns>
		public Vector2 GetRelativePosition(Rectangle BoundingBox)
		{
			return new Vector2(mouse_position.X - BoundingBox.Center.X, mouse_position.Y - BoundingBox.Center.Y);
		}

		/// <summary>
		/// Gets the delta mouse position since last update.
		/// </summary>
		/// <returns>Returns a Vector2 that contains the delta position of mouse.</returns>
		public Vector2 GetDeltaPosition()
		{
			//TODO: Delta mouse position
			return new Vector2(mouse_position.X - prev_mouse_position.X, mouse_position.Y - prev_mouse_position.Y);
		}


		public bool MouseMoved()
		{
			return mouse_position == prev_mouse_position;
		}

		public void Update(MouseState CurrentMouseState)
		{
			//TODO: Handle mouse button hold. 
			this.prev_mouse_position = mouse_position;
			this.mouse_position = new Vector2((float)CurrentMouseState.X, (float)CurrentMouseState.Y);

			//Handle left click buffer, if mouse is clicked, add its coordinates to the buffer.
			if ((CurrentMouseState.LeftButton == ButtonState.Pressed) && (prev_mouse_state.LeftButton == ButtonState.Released))
			{
				left_button_buffer.Add(new Vector2(mouse_position.X, mouse_position.Y));
			}

			//Handle right click buffer, if mouse is clicked, add its coordinates to the buffer.
			if ((CurrentMouseState.RightButton == ButtonState.Pressed) && (prev_mouse_state.RightButton == ButtonState.Released))
			{
				right_button_buffer.Add(new Vector2(mouse_position.X, mouse_position.Y));				
			}

			//Handle middle click buffer, same as above.
			if ((CurrentMouseState.MiddleButton == ButtonState.Pressed) && (prev_mouse_state.LeftButton == ButtonState.Released))
			{
				middle_button_buffer.Add(new Vector2(mouse_position.X, mouse_position.Y));
			}
			
		}

		/// <summary>
		/// Removes the first item in a buffer, hopefully after it has been processed and responded to.
		/// </summary>
		/// <param name="Button">The button that the buffer belongs to</param>
		public void RemoveFirstFromBuffer(Buttons Button)
		{
			switch (Button)
			{
				case (Buttons.LeftButton):
					left_button_buffer.RemoveAt(left_button_buffer.Count - 1);
					break;
				case (Buttons.RightButton):
					right_button_buffer.RemoveAt(right_button_buffer.Count - 1);
					break;
				case (Buttons.MiddleButton):
					middle_button_buffer.RemoveAt(middle_button_buffer.Count - 1);
					break;
				default:
					throw new ArgumentOutOfRangeException();
				
			}
		}

		//DO: Fix summaries.
		/// <summary>
		/// Gets a specific buffer specified by the buffer identifier.
		/// Each buffer has its own unique identifier, the left button identifier is 0, right button buffer is 1 and middle button identifier is 2
		/// </summary>
		/// <param name="BufferIdentifier">Unique buffer identifier</param>
		/// <returns>Returns the requested buffer in the form of a Vector2 list containing the locations of clicks.</returns>
		public List<Vector2> GetBuffer(Buttons Button)
		{
			switch (Button)
			{
				case Buttons.LeftButton:
					return left_button_buffer;
				case Buttons.RightButton:
					return right_button_buffer;
				case Buttons.MiddleButton:
					return middle_button_buffer;
				default:
					throw new ArgumentOutOfRangeException();
			} 
		}


		/// <summary>
		/// Gets all buffer in a list of lists, each item in the containing list is a buffer. Access buffers by their index which is the same
		/// as their identifiers.
		/// 
	 
		/// Each buffer has its own unique identifier, the left button identifier is 0, right button buffer is 1 and middle button identifier is 2
		/// </summary>
		/// <returns>Returns a list of buffer as a list of Vector2 lists.</returns>
		public List<List<Vector2>> GetAllBuffers()
		{
			//TD: Cleaner way of populating return_list
			List<List<Vector2>> return_list = new List<List<Vector2>>();
			return_list.Add(left_button_buffer); return_list.Add(right_button_buffer); return_list.Add(middle_button_buffer);
			return return_list;
		}
		

		


	}
}
