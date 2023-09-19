meta:
  id: unity_web_data
  title: UnityWebData file
  application: Unity WebGL
  file-extension: data
  tags: [unity, unity2d, unity3d, unity_webgl]
  license: CC-BY-NC-SA-4.0
  ks-version: 0.10
  encoding: utf-8
  endian: le

doc: |
  UnityWebData are the Unity game engine file format used to store the
  game data and resources for WebGL build.
doc-ref: https://forum.xentax.com/viewtopic.php?f=21&p=187239

seq:
  - id: magic
    type: strz
    doc: The file identifier are consist of 16 char `UnityWebData1.0`.
  - id: begin_offset
    type: u4
    doc: The offset value where the first file offset reside.
  - id: files
    type: file_entry
    repeat: until
    repeat-until: _io.pos == begin_offset

types:
  file_entry:
    seq:
      - id: file_offset
        type: u4
      - id: file_size
        type: u4
      - id: filename_length
        type: u4
      - id: filename
        type: str
        size: filename_length
    instances:
      body:
        io: _root._io
        pos: file_offset
        size: file_size
