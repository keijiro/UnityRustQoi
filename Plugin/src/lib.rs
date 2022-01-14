#[no_mangle]

pub unsafe extern fn read_header(src: *const u8, size: usize, out: *mut rapid_qoi::Qoi) {
    let bytes = std::slice::from_raw_parts(src, size);
    *out = rapid_qoi::Qoi::decode_header(&bytes).unwrap();
}
