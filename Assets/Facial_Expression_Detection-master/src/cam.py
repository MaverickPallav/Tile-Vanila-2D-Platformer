import cv2

def takeSnap():
    print("Press spacebar to take a snap :) ")
    vid = cv2.VideoCapture(0)
    if not vid.isOpened():
        raise IOError("Cannot open webcam")
        exit(0)
    
    while True:
        ret, frame = vid.read()
        

        frame = cv2.resize(frame, None, fx=1, fy=1, interpolation=cv2.INTER_AREA)
        cv2.imshow('Input', frame)
        c=cv2.waitKey(1)
        if(c==32):
            break
    c = cv2.waitKey(2000)
    cv2.imwrite("images/snap.jpg",frame)
    vid.release()
    cv2.destroyAllWindows()
