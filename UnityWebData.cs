// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

namespace Kaitai
{
    /// <summary>
    /// UnityWebData are the Unity game engine file format used to store the
    /// game data and resources for WebGL build.
    /// </summary>
    /// <remarks>
    /// Reference: <a href="https://forum.xentax.com/viewtopic.php?f=21&amp;p=187239">Source</a>
    /// </remarks>
    public partial class UnityWebData : KaitaiStruct
    {
        public static UnityWebData FromFile(string fileName)
        {
            return new UnityWebData(new KaitaiStream(fileName));
        }

        public UnityWebData(
            KaitaiStream p__io,
            KaitaiStruct p__parent = null,
            UnityWebData p__root = null
        ) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }

        private void _read()
        {
            _magic = System.Text.Encoding
                .GetEncoding("utf-8")
                .GetString(m_io.ReadBytesTerm(0, false, true, true));
            _beginOffset = m_io.ReadU4le();
            _files = new List<FileEntry>();
            {
                var i = 0;
                FileEntry M_;
                do
                {
                    M_ = new FileEntry(m_io, this, m_root);
                    _files.Add(M_);
                    i++;
                } while (!(M_Io.Pos == BeginOffset));
            }
        }

        public partial class FileEntry : KaitaiStruct
        {
            public static FileEntry FromFile(string fileName)
            {
                return new FileEntry(new KaitaiStream(fileName));
            }

            public FileEntry(
                KaitaiStream p__io,
                UnityWebData p__parent = null,
                UnityWebData p__root = null
            ) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_data = false;
                _read();
            }

            private void _read()
            {
                _fileOffset = m_io.ReadU4le();
                _fileSize = m_io.ReadU4le();
                _filenameLength = m_io.ReadU4le();
                _filename = System.Text.Encoding
                    .GetEncoding("utf-8")
                    .GetString(m_io.ReadBytes(FilenameLength));
            }

            private bool f_data;
            private byte[] _data;
            public byte[] Data
            {
                get
                {
                    if (f_data)
                        return _data;
                    KaitaiStream io = M_Root.M_Io;
                    long _pos = io.Pos;
                    io.Seek(FileOffset);
                    _data = io.ReadBytes(FileSize);
                    io.Seek(_pos);
                    f_data = true;
                    return _data;
                }
            }
            private uint _fileOffset;
            private uint _fileSize;
            private uint _filenameLength;
            private string _filename;
            private UnityWebData m_root;
            private UnityWebData m_parent;
            public uint FileOffset
            {
                get { return _fileOffset; }
            }
            public uint FileSize
            {
                get { return _fileSize; }
            }
            public uint FilenameLength
            {
                get { return _filenameLength; }
            }
            public string Filename
            {
                get { return _filename; }
            }
            public UnityWebData M_Root
            {
                get { return m_root; }
            }
            public UnityWebData M_Parent
            {
                get { return m_parent; }
            }
        }

        private string _magic;
        private uint _beginOffset;
        private List<FileEntry> _files;
        private UnityWebData m_root;
        private KaitaiStruct m_parent;

        /// <summary>
        /// The file identifier are consist of 16 char `UnityWebData1.0`.
        /// </summary>
        public string Magic
        {
            get { return _magic; }
        }

        /// <summary>
        /// The offset value where the first file offset reside.
        /// </summary>
        public uint BeginOffset
        {
            get { return _beginOffset; }
        }
        public List<FileEntry> Files
        {
            get { return _files; }
        }
        public UnityWebData M_Root
        {
            get { return m_root; }
        }
        public KaitaiStruct M_Parent
        {
            get { return m_parent; }
        }
    }
}
