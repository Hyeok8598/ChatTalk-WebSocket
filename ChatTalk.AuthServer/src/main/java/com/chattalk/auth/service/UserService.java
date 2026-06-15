package com.chattalk.auth.service;

import com.chattalk.auth.dto.request.ChangeRequest;
import com.chattalk.auth.dto.request.LoginRequest;
import com.chattalk.auth.dto.request.SearchRequset;
import com.chattalk.auth.dto.request.SignUpRequest;
import com.chattalk.auth.dto.response.ChangeResponse;
import com.chattalk.auth.dto.response.LoginResponse;
import com.chattalk.auth.dto.response.SearchResponse;
import com.chattalk.auth.dto.response.SignUpResponse;
import com.chattalk.auth.mapper.UserMapper;
import com.chattalk.auth.mapper.dto.Insert001InDto;
import com.chattalk.auth.mapper.dto.SelectOne001InDto;
import com.chattalk.auth.mapper.dto.SelectOne001OutDto;
import com.chattalk.auth.mapper.dto.Update001InDto;
import org.springframework.stereotype.Service;

@Service
public class UserService {
    private final UserMapper mapper;

    public UserService(UserMapper mapper) {
        this.mapper = mapper;
    }

    public SignUpResponse signUp(SignUpRequest request) {
        SelectOne001InDto selectOne001InDto = new SelectOne001InDto();
        SelectOne001OutDto selectOne001OutDto;
        Insert001InDto insertInDto = new Insert001InDto();
        SignUpResponse result = new SignUpResponse();
        int success = 0;

        if(request.getUserId() == null) {
            throw new IllegalArgumentException("사용자 ID는 필수입니다.");
        }

        if(request.getPassword() == null) {
            throw new IllegalArgumentException("패스워드는 필수입니다.");
        }

        if(request.getUserName() == null) {
            throw new IllegalArgumentException("사용자명은 필수입니다.");
        }

        selectOne001InDto.setUserId(request.getUserId());
        selectOne001OutDto = mapper.selectOne001(selectOne001InDto);

        if(selectOne001OutDto != null) {
            throw new IllegalArgumentException("유저 ID가 존재합니다.");
        }

        insertInDto.setUserId(request.getUserId());
        insertInDto.setPassword(request.getPassword());
        insertInDto.setUserName(request.getUserName());

        success = mapper.insert001(insertInDto);

        result.setSuccess(success);
        return result;
    }

    public LoginResponse login(LoginRequest request) {
        LoginResponse result = new LoginResponse();
        SelectOne001InDto inDto = new SelectOne001InDto();
        inDto.setUserId(request.getUserId());

        SelectOne001OutDto outDto = mapper.selectOne001(inDto);

        if(outDto == null) {
            throw new RuntimeException("존재하지 않는 사용자입니다.");
        }

        if(!outDto.getPassword().equals(request.getPassword())) {
            throw new IllegalArgumentException("비밀번호가 일치하지 않습니다.");
        }

        result.setUserId(outDto.getUserId());
        result.setUserName(outDto.getUserName());
        return result;
    }

    public ChangeResponse change(ChangeRequest request) {
        ChangeResponse result = new ChangeResponse();
        int success = 0;

        SelectOne001InDto selectOne001InDto = new SelectOne001InDto();
        SelectOne001OutDto selectOne001OutDto;
        Update001InDto inDto = new Update001InDto();

        selectOne001InDto.setUserId(request.getUserId());
        selectOne001InDto.setPassword(request.getBeforePassword());

        selectOne001OutDto = mapper.selectOne001(selectOne001InDto);

        if(selectOne001OutDto == null) {
            throw new IllegalArgumentException("사용자 정보가 일치하지 않습니다.");
        }

        if(selectOne001OutDto.getPassword().equals(request.getAfterPassword())) {
            throw new IllegalArgumentException("이전 비밀번호와 일치합니다.");
        }

        inDto.setUserId(request.getUserId());
        inDto.setUserName(request.getUserName());
        inDto.setPassword(request.getAfterPassword());

        success = mapper.update001(inDto);

        result.setSuccess(success);
        return result;
    }

    public SearchResponse search(SearchRequset requset) {
        SearchResponse result = new SearchResponse();
        SelectOne001InDto inDto = new SelectOne001InDto();
        SelectOne001OutDto outDto;

        if(requset.getUserId().isEmpty()) {
            throw new IllegalArgumentException("사용자 ID는 필수입니다.");
        }

        inDto.setUserId(requset.getUserId());
        outDto = mapper.selectOne001(inDto);

        result.setUserId(outDto.getUserId());
        result.setUserName(outDto.getUserName());

        return result;
    }
}
