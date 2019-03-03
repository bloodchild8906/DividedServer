using System;
using System.Collections.Generic;
using System.Text;

namespace ServerLibrary.Helpers
{
    internal class HandleBuffer
    {
        List<byte> _buffer;
        byte[] _readBuff;
        int _readpos;
        bool _buffUpdate = false;

        public HandleBuffer()
        {
            _buffer = new List<byte>();
            _readpos = 0;
        }

        public int GetReadPos()
        {
            return _readpos;
        }

        public byte[] ToArray()
        {
            return _buffer.ToArray();
        }

        public int Count()
        {
            return _buffer.Count;
        }

        public int Length()
        {
            return Count() - _readpos;
        }

        public void Clear()
        {
            _buffer.Clear();
            _readpos = 0;
        }
        #region"Write Data"
        public void WriteByte(byte Inputs)
        {
            _buffer.Add(Inputs);
            _buffUpdate = true;
        }

        public void WriteBytes(byte[] Input)
        {
            _buffer.AddRange(Input);
            _buffUpdate = true;
        }

        public void WriteShort(short Input)
        {
            _buffer.AddRange(BitConverter.GetBytes(Input));
            _buffUpdate = true;
        }

        public void WriteInteger(int Input)
        {
            _buffer.AddRange(BitConverter.GetBytes(Input));
            _buffUpdate = true;
        }

        public void WriteFloat(float Input)
        {
            _buffer.AddRange(BitConverter.GetBytes(Input));
            _buffUpdate = true;
        }

        public void WriteString(string Input)
        {
            _buffer.AddRange(BitConverter.GetBytes(Input.Length));
            _buffer.AddRange(Encoding.ASCII.GetBytes(Input));
            _buffUpdate = true;
        }
        #endregion

        #region "Read Data"
        public string ReadString(bool Peek = true)
        {
            int len = ReadInteger(true);
            if (_buffUpdate)
            {
                _readBuff = _buffer.ToArray();
                _buffUpdate = false;
            }

            string ret = Encoding.ASCII.GetString(_readBuff, _readpos, len);
            if (Peek & _buffer.Count > _readpos)
            {
                if (ret.Length > 0)
                {
                    _readpos += len;
                }
            }
            return ret;
        }

        public byte ReadByte(bool Peek = true)
        {
            if (_buffer.Count > _readpos)
            {
                if (_buffUpdate)
                {
                    _readBuff = _buffer.ToArray();
                    _buffUpdate = false;
                }

                byte ret = _readBuff[_readpos];
                if (Peek & _buffer.Count > _readpos)
                {
                    _readpos += 1;
                }
                return ret;
            }
            else
            {
                throw new IndexOutOfRangeException("Byte Buffer Past Limit!");
            }
        }

        public byte[] ReadBytes(int Length, bool Peek = true)
        {
            if (_buffUpdate)
            {
                _readBuff = _buffer.ToArray();
                _buffUpdate = false;
            }

            byte[] ret = _buffer.GetRange(_readpos, Length).ToArray();
            if (Peek)
            {
                _readpos += Length;
            }
            return ret;
        }

        public float ReadFloat(bool Peek = true)
        {
            if (_buffer.Count > _readpos)
            {
                if (_buffUpdate)
                {
                    _readBuff = _buffer.ToArray();
                    _buffUpdate = false;
                }

                float ret = BitConverter.ToSingle(_readBuff, _readpos);
                if (Peek & _buffer.Count > _readpos)
                {
                    _readpos += 4;
                }
                return ret;
            }

            else
            {
                throw new IndexOutOfRangeException("Byte Buffer is Past its Limit!");
            }
        }

        public int ReadInteger(bool Peek = true)
        {
            if (_buffer.Count > _readpos)
            {
                if (_buffUpdate)
                {
                    _readBuff = _buffer.ToArray();
                    _buffUpdate = false;
                }

                int ret = BitConverter.ToInt32(_readBuff, _readpos);
                if (Peek & _buffer.Count > _readpos)
                {
                    _readpos += 4;
                }
                return ret;
            }

            else
            {
                throw new IndexOutOfRangeException("Byte Buffer is Past its Limit!");
            }
        }
        #endregion

        private bool disposedValue = false;

        //IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    _buffer.Clear();
                }

                _readpos = 0;
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
