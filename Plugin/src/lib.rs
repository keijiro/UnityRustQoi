#[no_mangle]
pub unsafe extern fn decode_header(data: *const u8, data_size: usize, output: *mut rapid_qoi::Qoi) {
    let src = std::slice::from_raw_parts(data, data_size);
    *output = rapid_qoi::Qoi::decode_header(src).unwrap();
}

#[no_mangle]
pub unsafe extern fn decode(data: *const u8, data_size: usize, output: *mut u8, output_size: usize) {
    let src = std::slice::from_raw_parts(data, data_size);
    let dst = std::slice::from_raw_parts_mut(output, output_size);
    rapid_qoi::Qoi::decode(src, dst).unwrap();
}
