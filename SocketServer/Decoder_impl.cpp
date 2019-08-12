#include "Decoder.h"

void __decode(const std::string& str, char* a, char* b) {
	const char* data = str.data();

	const int& in_a = (const int&)(*data);
	const int& size = (const int&)(*(data + 4));
	const int* in_b = (const int*)(data + 8);

	int* out_a = (int*)a;
	int* out_b = (int*)b;

	*out_a = in_a;
	for (int i = 0; i < size; i++) {
		out_b[i] = in_b[i];
	}
}
