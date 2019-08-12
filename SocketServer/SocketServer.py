import sys
import socket
import numpy as np

import Decoder

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind(('127.0.0.1', 50007))
    s.listen(1)
    conn, addr = s.accept()
    with conn:
        while True:
            size = int.from_bytes(conn.recv(4, socket.MSG_WAITALL), sys.byteorder)
            if not size:
                break

            data = np.empty(size, dtype=np.uint8)
            conn.recv_into(data, size, socket.MSG_WAITALL)

            a = np.empty(1, dtype=np.int32)
            b = np.empty((size - 8) // 4, dtype=np.int32)

            Decoder.decode(data, a, b)
            print(size, data)
            print(a, b)

            res = np.array([1, 2, 3])
            conn.sendall(len(res).to_bytes(4, sys.byteorder))
            conn.sendall(res.tobytes())
