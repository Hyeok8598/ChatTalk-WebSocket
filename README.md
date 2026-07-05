## ChatTalk

Vue + Spring Boot + .NET 기반의 실시간 채팅 플랫폼

## 📖 프로젝트 소개

ChatTalk는 실시간 채팅 서비스를 구현하기 위한 개인 포트폴리오 프로젝트입니다.

인증 서버(Spring Boot), WebSocket 서버(.NET), Web Client(Vue)를 분리하여 실제 서비스와 유사한 구조로 개발하였습니다.

## 🏗 프로젝트 구조

```
ChatTalk
│
├── Auth Server
│   ├── Spring Boot
│   ├── MyBatis
│   ├── PostgreSQL
│   └── File API
│
├── Socket Server
│   ├── .NET 8
│   ├── WebSocket
│   ├── Dapper
│   └── PostgreSQL
│
└── Web Client
    ├── Vue3
    ├── Vite
    ├── TailwindCSS
    └── Axios
```

## 🚀 기술 스택

### Backend
Java 17
Spring Boot
MyBatis
PostgreSQL

### WebSocket Server
.NET 8
WebSocket
Dapper

### Frontend
Vue3
Vite
Tailwind CSS
Axios

## 📌 주요 기능
### 인증
회원가입
로그인
### 채팅
전체 채팅
접속자 목록
귓속말

### 파일
파일 업로드
파일 다운로드
파일 메시지 전송

## 📂 프로젝트 구조

```
frontend/
backend-auth/
backend-socket/
```

## 📸 화면

(스크린샷 추가 예정)

## 📝 Release

### v0.1.0
### Auth Server
로그인
회원가입
파일 업로드 API
파일 다운로드 API

### Socket Server
WebSocket 연결
실시간 채팅
귓속말
파일 메시지

### Web Client
로그인 화면
채팅 화면
파일 업로드
파일 다운로드
파일 미리보기

## 🔨 앞으로 구현 예정

채팅 내역 조회
채팅방 기능
이미지 미리보기
이모지
UI 리팩토링
