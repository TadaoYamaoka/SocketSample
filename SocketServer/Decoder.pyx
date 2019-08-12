# distutils: sources = ["Decoder_impl.cpp"]
# distutils: language = c++

from libcpp.string cimport string

import numpy as np
cimport numpy as np

cdef extern from "Decoder.h":
	void __decode(const char* data, char* a, char* b)

def decode(np.ndarray data, np.ndarray a, np.ndarray b):
	return __decode(data.data, a.data, b.data)