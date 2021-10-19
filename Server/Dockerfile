FROM python:3.8
 
WORKDIR /Server
COPY . /Server
 
RUN pip install -r requirements.txt
 
ENTRYPOINT ["python"]
CMD ["Server.py"]