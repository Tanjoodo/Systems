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
		Point _mousePosition;
		Point _prevMousePosition;
		MouseState _prevMouseState;

		public enum Button { Left, Right, Middle }

        Dictionary<Button, List<Point>> _buffers = new Dictionary<Button, List<Point>>
        {
            {Button.Left, new List<Point>()},
            {Button.Right, new List<Point>() },
            {Button.Middle, new List<Point>() }
        };

        public Dictionary<Button, List<Point>> Buffers => _buffers;

		/// <summary>
		/// Creates a new instance of MouseInput
		/// </summary>
		public MouseInput(bool onlyWorkWhenInFocus = true)
		{
            //TODO: don't record mouse events if window is not in focus
            _mousePosition = Point.Zero; 
		}

		/// <summary>
		/// Gets the absolute mouse position since the last Update().
		/// </summary>
		/// <returns>Returns a Vector2 containing the mouse position.</returns>
		public Point MousePosition => _mousePosition;

		/// <summary>
		/// Gets the relative mouse position from a specific point.
		/// </summary>
		/// <param name="point">A point in the form of a Vector2.</param>
		/// <returns>Returns a Vector2 containing the relative position between the mouse cursor and the specified point.</returns>
		public Vector2 GetRelativePosition(Point point)
		{
            return (_mousePosition - point).ToVector2();
		}

		/// <summary>
		/// Gets the relative mouse position from the center of a bounding box.
		/// </summary>
		/// <param name="boundingBox">Bounding box of an object in the form of a Rectangle.</param>
		/// <returns>Returns a Vector2 containing the relative position between the mouse cursor and the center of the specified bounding box.</returns>
		public Vector2 GetRelativePosition(Rectangle boundingBox)
		{
            return (_mousePosition - boundingBox.Center).ToVector2();
		}

		/// <summary>
		/// Gets the delta mouse position since last update.
		/// </summary>
		/// <returns>Returns a Vector2 that contains the delta position of mouse.</returns>
		public Vector2 GetDeltaPosition()
		{
            //TODO: Delta mouse position
            return (_mousePosition - _prevMousePosition).ToVector2();
		}


		public bool MouseMoved()
		{
			return _mousePosition == _prevMousePosition;
		}

		public void Update(MouseState currentMouseState)
		{
            //TODO: Handle mouse button hold. 
            _prevMousePosition = _mousePosition;
            _mousePosition = currentMouseState.Position;

			// Handle left click buffer, if mouse is clicked, add its coordinates to the buffer.
			if ((currentMouseState.LeftButton == ButtonState.Pressed) && (_prevMouseState.LeftButton == ButtonState.Released))
			{
				Buffers[Button.Left].Add(_mousePosition);
			}

			// Handle right click buffer, if mouse is clicked, add its coordinates to the buffer.
			if ((currentMouseState.RightButton == ButtonState.Pressed) && (_prevMouseState.RightButton == ButtonState.Released))
			{
				Buffers[Button.Right].Add(_mousePosition);				
			}

			// Handle middle click buffer, if mouse is clicked, add its coordinates to the buffer.
			if ((currentMouseState.MiddleButton == ButtonState.Pressed) && (_prevMouseState.LeftButton == ButtonState.Released))
			{
				Buffers[Button.Middle].Add(_mousePosition);
			}
		}

		/// <summary>
		/// Removes the first item in a buffer.
		/// </summary>
		/// <param name="button">The button that the buffer belongs to. </param>
		public void RemoveFirstFromBuffer(Button button)
		{
            Buffers[button].RemoveAt(Buffers[button].Count - 1);
        }

        //DO: Fix summaries.
        /// <summary>
        /// Gets a specific buffer specified by the buffer identifier.
        /// Each buffer has its own unique identifier, the left button identifier is 0, right button buffer is 1 and middle button identifier is 2
        /// </summary>
        /// <param name="BufferIdentifier">Unique buffer identifier</param>
        /// <returns>Returns the requested buffer in the form of a Vector2 list containing the locations of clicks.</returns>
        public List<Point> GetBuffer(Button button)
		{
            return Buffers[button];
		}
	}
}
