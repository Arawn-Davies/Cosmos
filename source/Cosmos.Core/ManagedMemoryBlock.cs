﻿using System;

namespace Cosmos.Core
{
    /// <summary>
    /// ManagedMemoryBlock class. Used to read and write a managed memory block.
    /// </summary>
    public unsafe class ManagedMemoryBlock
    {
        private byte[] memory;

        /// <summary>
        /// Offset.
        /// </summary>
        public UInt32 Offset;
        /// <summary>
        /// Size.
        /// </summary>
        public UInt32 Size;

        /// <summary>
        /// Create a new buffer with the given size, not aligned
        /// </summary>
        /// <param name="size">Size of buffer</param>
        public ManagedMemoryBlock(UInt32 size)
            : this(size, 1, false)
        { }

        /// <summary>
        /// Create a new buffer with the given size, aligned on the byte boundary specified
        /// </summary>
        /// <param name="size">Size of buffer</param>
        /// <param name="alignment">Byte Boundary alignment</param>
        public ManagedMemoryBlock(UInt32 size, byte alignment)
            : this(size, alignment, true)
        { }

        /// <summary>
        /// Create a new buffer with the given size, and aligned on the byte boundary if align is true
        /// </summary>
        /// <param name="size">Size of buffer</param>
        /// <param name="alignment">Byte Boundary alignment</param>
        /// <param name="align">true if buffer should be aligned, false otherwise</param>
        public ManagedMemoryBlock(UInt32 size, byte alignment, bool align)
        {
            memory = new byte[size + alignment - 1];
            fixed (byte* bodystart = memory)
            {
                Offset = (UInt32)bodystart;
                Size = size;
            }
            if (align == true)
            {
                while (this.Offset % alignment != 0)
                {
                    this.Offset++;
                }
            }
        }

        /// <summary>
        /// Get or set the byte at the given offset
        /// </summary>
        /// <param name="offset">Address Offset</param>
        /// <returns>Byte value at given offset</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown on invalid offset.</exception>
        public byte this[uint offset]
        {
            get
            {
                if (offset > Size)
                    throw new ArgumentOutOfRangeException("offset");
                return *(byte*)(this.Offset + offset);
            }
            set
            {
                if (offset < 0 || offset > Size)
                    throw new ArgumentOutOfRangeException("offset");
                (*(byte*)(this.Offset + offset)) = value;
            }
        }

        /// <summary>
        /// Read 16-bit from the memory block.
        /// </summary>
        /// <param name="offset">Data offset.</param>
        /// <returns>UInt16 value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if offset if bigger than memory block size.</exception>
        public UInt16 Read16(uint offset)
        {
            if (offset > Size)
                throw new ArgumentOutOfRangeException("offset");
            return *(UInt16*)(this.Offset + offset);
        }

        /// <summary>
        /// Write 16-bit to the memory block.
        /// </summary>
        /// <param name="offset">Data offset.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if offset if bigger than memory block size or smaller than 0.</exception>
        public void Write16(uint offset, UInt16 value)
        {
            if (offset < 0 || offset > Size)
                throw new ArgumentOutOfRangeException("offset");
            (*(UInt16*)(this.Offset + offset)) = value;
        }

        /// <summary>
        /// Read 32-bit from the memory block.
        /// </summary>
        /// <param name="offset">Data offset.</param>
        /// <returns>UInt32 value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if offset if bigger than memory block size.</exception>
        public UInt32 Read32(uint offset)
        {
            if (offset > Size)
                throw new ArgumentOutOfRangeException("offset");
            return *(UInt32*)(this.Offset + offset);
        }

        /// <summary>
        /// Write 32-bit to the memory block.
        /// </summary>
        /// <param name="offset">Data offset.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if offset if bigger than memory block size or smaller than 0.</exception>
        public void Write32(uint offset, UInt32 value)
        {
            if (offset < 0 || offset > Size)
                throw new ArgumentOutOfRangeException("offset");
            (*(UInt32*)(this.Offset + offset)) = value;
        }
    }
}
