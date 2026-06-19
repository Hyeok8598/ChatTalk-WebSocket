package com.chattalk.auth.service;

import com.chattalk.auth.common.util.CommonUtil;
import com.chattalk.auth.common.util.CryptoUtil;
import com.chattalk.auth.common.DataMap;
import com.chattalk.auth.mapper.UsersMapper;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

@Slf4j
@Service
public class UserService {
    private final UsersMapper mapper;

    public UserService(UsersMapper mapper) {
        this.mapper = mapper;
    }

    public int signUp(DataMap dataMap) {
        DataMap insertMap;

        String userId   = dataMap.getParam("userId");
        String password = CryptoUtil.encode(dataMap.getParam("password"));
        String userName = dataMap.getParam("userName");

        log.info("[password]={}", password);

        if(CommonUtil.isEmpty(userId)) {
            throw new IllegalArgumentException("사용자 ID는 필수입니다.");
        }

        if(CommonUtil.isEmpty(password)) {
            throw new IllegalArgumentException("패스워드는 필수입니다.");
        }

        if(CommonUtil.isEmpty(userName)) {
            throw new IllegalArgumentException("사용자명은 필수입니다.");
        }

        DataMap result = mapper.usersSelectOne002(dataMap);

        if(!CommonUtil.isEmpty(result)) {
            throw new IllegalArgumentException("유저 ID가 존재합니다.");
        }

        insertMap = dataMap;
        insertMap.setParam("password", password);

        return mapper.usersInsert001(dataMap);
    }

    public DataMap login(DataMap dataMap) {
        DataMap result = mapper.usersSelectOne001(dataMap);
        String inputPw = dataMap.getParam("password");
        String savedPw = "";

        if(CommonUtil.isEmpty(result)) {
            throw new RuntimeException("존재하지 않는 사용자입니다.");
        }

        savedPw = result.getParam("password");

        if(!CryptoUtil.matches(inputPw, savedPw)) {
            throw new IllegalArgumentException("비밀번호가 일치하지 않습니다.");
        }

        return result;
    }

    public int change(DataMap dataMap) {
        DataMap result, insertMap;
        String bfPw = "";
        String afPw = dataMap.getParam("password");
        String encodePw = CryptoUtil.encode(afPw);

        result = mapper.usersSelectOne001(dataMap);

        if(CommonUtil.isEmpty(dataMap)) {
            throw new IllegalArgumentException("사용자 정보가 일치하지 않습니다.");
        }

        bfPw = result.getParam("password");

        if(CryptoUtil.matches(bfPw, afPw)) {
            throw new IllegalArgumentException("이전 비밀번호와 일치합니다.");
        }

        insertMap = result;
        insertMap.setParam("password", encodePw);

        return mapper.usersUpdate001(insertMap);
    }

    public DataMap search(DataMap dataMap) {
        DataMap result;
        String userId = dataMap.getParam("userId");

        if(CommonUtil.isEmpty(userId)) {
            throw new IllegalArgumentException("사용자 ID는 필수입니다.");
        }

        result = mapper.usersSelectOne002(dataMap);

        return result;
    }
}
