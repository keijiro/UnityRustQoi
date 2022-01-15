#[no_mangle]
pub unsafe extern fn decode_header(data: *const u8, data_size: usize, output: *mut rapid_qoi::Qoi) -> bool {
    let src = std::slice::from_raw_parts(data, data_size);
    let res = rapid_qoi::Qoi::decode_header(src);
    if res.is_err() { return false; }
    *output = res.unwrap();
    true
}

#[no_mangle]
pub unsafe extern fn decode(data: *const u8, data_size: usize, output: *mut u8, output_size: usize) -> bool {
    let src = std::slice::from_raw_parts(data, data_size);
    let dst = std::slice::from_raw_parts_mut(output, output_size);
    rapid_qoi::Qoi::decode(src, dst).is_ok()
}
