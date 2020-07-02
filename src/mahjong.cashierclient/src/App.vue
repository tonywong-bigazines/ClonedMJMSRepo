<template>
  <div id="app">
    <el-container>
      <el-container>
        <el-aside width="300px">
          <el-menu
            default-active="2"
            class="side-menu"
            background-color="#545c64"
            text-color="#fff"
            active-text-color="#ffd04b"
            :router="true"
          >  
            <el-menu-item index="/cashier/index.html">
              <i class="el-icon-s-home"></i>
              <span slot="title">Dashboard</span>
            </el-menu-item>
            <el-menu-item index="/cashier/index.html/tables">
              <i class="el-icon-menu"></i>
              <span slot="title">Tables</span>
            </el-menu-item>
            <el-menu-item index="/about">
              <i class="el-icon-postcard"></i>
              <span slot="title">Cards</span>
            </el-menu-item>
            <el-menu-item index="/about">
              <i class="el-icon-user"></i>
              <span slot="title">Clients</span>
            </el-menu-item>
            <el-menu-item index="/about">
              <i class="el-icon-date"></i>
              <span slot="title">Reports</span>
            </el-menu-item>
            <el-menu-item index="/about">
              <i class="el-icon-setting"></i>
              <span slot="title">Settings</span>
            </el-menu-item>
          </el-menu>
        </el-aside>
        <el-container>
          <el-main>
            <router-view />
          </el-main>
          <el-footer> Copyright All Rights Reserved © 2020 </el-footer>
        </el-container>
      </el-container>
    </el-container>
  </div>
</template>

<style lang="scss">
html{
  height:100%;
}
body {
  margin: 0;
  padding: 0;
  height:100%;
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  height:100%;
  .el-container{
    height:100%;
  }
  .side-menu{
    text-align:left;
    height:100%;
  }
  .el-footer{
    background:#eaeaea;
    height:auto;
    padding:20px;
    text-align:center;
    color:#888;
    font-size:14px;
  }
}


</style>
