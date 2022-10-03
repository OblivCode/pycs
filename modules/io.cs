using static pycs.pycs;
using System.IO;
using System.Threading.Tasks;

namespace pycs.modules
{
    public class io
    {
        public T open<T>(string filename, string mode)
        {
            return pycs.open<T>(filename, mode);
        }
        public class IOBase
        {
            protected Stream _stream;

            public bool closed = false;
            public bool readable() => _stream.CanRead; 
            public bool seekable() => _stream.CanSeek;
            public bool writeable() => _stream.CanWrite;
            public int tell() => (int)_stream.Position;

            public IOBase truncate(int size)
            {
                _stream.SetLength(size);
                return this;
            }
            /// <summary>
            /// Seek a position within the stream
            /// </summary>
            /// <param name="offset"></param>
            /// <param name="whence"></param>
            /// <returns></returns>
            public IOBase seek(int offset, int whence = 0)
            {
                SeekOrigin origin = whence == 0 ? SeekOrigin.Begin : whence == 1 ? SeekOrigin.Current : SeekOrigin.End;
                _stream.Seek(offset, origin);
                return this;
            }
            /// <summary>
            /// Write to stream.
            /// </summary>
            /// <param name="data">Byte to be written</param>
            /// <returns></returns>
            public IOBase write(byte data)
            {
                _stream.WriteByte(data);
                return this;
            }
            /// <summary>
            /// Write to stream.
            /// </summary>
            /// <param name="data">Bytes to be written</param>
            public IOBase write(bytes data)
            {
                _stream.Write(data.ToStandard(), 0, len(data));
                return this;
            }
            /// <summary>
            /// Write to stream asynchronously.
            /// </summary>
            /// <param name="data">Bytes to be written</param>
            public async Task<IOBase> write_async(bytes data)
            {
                await _stream.WriteAsync(data.ToStandard(), 0, len(data));
                return this;
            }
            /// <summary>
            /// Read bytes from stream.
            /// </summary>
            /// <param name="size">Amount of bytes to read</param>
            public byte[] read(int size = -1)
            {
                byte[] buffer = size == -1 ? new byte[0] : new byte[size];
                long bytes_left = _stream.Length - _stream.Position;

                if (bytes_left < size)
                    buffer = new byte[bytes_left];

                _stream.Read(buffer, 0, buffer.Length);

                return buffer;
            }
            /// <summary>
            /// Read bytes from stream asynchronously.
            /// </summary>
            /// <param name="size">Amount of bytes to read</param>
            public async Task<byte[]> read_async(int size = -1)
            {
                byte[] buffer = size == -1 ? new byte[0] : new byte[size];
                long bytes_left = _stream.Length - _stream.Position;

                if(bytes_left < size)
                    buffer = new byte[bytes_left];

                await _stream.ReadAsync(buffer, 0, buffer.Length);
                
                return buffer;
            }
            /// <summary>
            /// Flush stream.
            /// </summary>
            public IOBase flush()
            {
                _stream.Flush();
                return this;
            }
            /// <summary>
            /// Flush stream asynchronously.
            /// </summary>
            public async Task flush_async() => await _stream.FlushAsync();
            /// <summary>
            /// Close and dispose stream.
            /// </summary>
            public void close()
            {
                _stream.Dispose();
                closed = true;
            }
        }

        public class BytesIO : IOBase
        {
            public BytesIO(byte[] bytes)
            {
                _stream = new MemoryStream(bytes);
            }

            public byte[] getvalue()
            {
                byte[] buffer = new byte[_stream.Length];
                _stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }

            public async Task<byte[]> getvalue_async()
            {
                byte[] buffer = new byte[_stream.Length];
                await _stream.ReadAsync(buffer, 0, buffer.Length);
                return buffer;

            }
        }
        public class StringIO : IOBase
        {
            public StringIO(string str)
            {
                _stream = new MemoryStream(b(str));
            }

            public string read()
            {
                byte[] buffer = new byte[_stream.Length];
                _stream.Read(buffer, 0, buffer.Length);
                return buffer.ToString();
            }
            public StringIO write(string str)
            {
                byte[] buffer = b(str);
                _stream.Write(buffer, 0, str.Length);
                return this;
            }
            public async Task<StringIO> write_async(string str)
            {
                byte[] buffer = b(str);
                await _stream.WriteAsync(buffer,0,str.Length);
                return this;
            }
        }
    }
}
