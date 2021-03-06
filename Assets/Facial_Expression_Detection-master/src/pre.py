import cv2
import os
            
def facecrop(image):  
    facedata = "models/haarcascade_frontalface_alt.xml"
    cascade = cv2.CascadeClassifier(facedata)

    img = cv2.imread("images/"+image)

    try:
    
        minisize = (img.shape[1],img.shape[0])
        miniframe = cv2.resize(img, minisize)
        
        faces = cascade.detectMultiScale(miniframe)

        for f in faces:
            x, y, w, h = [ v for v in f ]
            cv2.rectangle(img, (x,y), (x+w,y+h), (0,255,0), 2)

            sub_face = img[y:y+h, x:x+w]

            f_name = image.split('/')
            f_name = f_name[-1]
            
            cv2.imwrite("images/CROPPED"+f_name, sub_face)
            #print ("Writing: " + image)
            return 1

    except:
        print("Error")
        return 0

    #cv2.imshow(image, img)


#if __name__ == '__main__':
    #images = os.listdir("E:/data/vids/a")
    #i = 0
    #for img in images:
 #       file = input("Enter name of file : ")
        #print (i)
  #      facecrop(file)
        #i += 1


