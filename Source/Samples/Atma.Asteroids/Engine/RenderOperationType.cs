using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Engine
{
    /// <summary>
    ///		Types for determining which render operation to do for a series of vertices.
    /// </summary>
    public enum RenderOperationType
    {
        /// <summary>
        ///		Render the vertices as individual points.
        /// </summary>
        PointList = 1,

        /// <summary>
        ///		Render the vertices as a series of individual lines.
        /// </summary>
        LineList,

        /// <summary>
        ///		Render the vertices as a continuous line.
        /// </summary>
        LineStrip,

        /// <summary>
        ///		Render the vertices as a series of individual triangles.
        /// </summary>
        TriangleList,

        /// <summary>
        ///		Render the vertices as a continous set of triangles in a zigzag type fashion.
        /// </summary>
        TriangleStrip,

        /// <summary>
        ///		Render the vertices as a set of trinagles in a fan like formation.
        /// </summary>
        TriangleFan
    }
}
