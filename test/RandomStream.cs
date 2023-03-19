namespace Test
{
    using System;
    using System.IO;
    /// <summary>
    /// Random stream create chat grpc
    /// </summary>
    public class RandomStream : Stream
    {
        private readonly long _length;
        private long _position;
        private readonly Random _random;

        public RandomStream(long length)
        {
            _length = length;
            _position = 0;
            _random = new Random();
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length => _length;

        public override long Position
        {
            get => _position;
            set
            {
                if (value < 0 || value > _length)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Position is out of range.");
                }

                _position = value;
            }
        }

        public override void Flush()
        {
            // Нічого не робити, оскільки запис не підтримується
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_position >= _length)
            {
                return 0;
            }

            if (count + _position > _length)
            {
                count = (int)(_length - _position);
            }

            byte[] randomBytes = new byte[count];
            Array.Copy(randomBytes, 0, buffer, offset, count);

            _position += count;
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset cannot be negative.");
            }

            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = Length - offset;
                    break;
                default:
                    throw new ArgumentException("Invalid seek origin.");
            }

            return Position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Setting stream length is not supported.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Writing to stream is not supported.");
        }
    }

}
