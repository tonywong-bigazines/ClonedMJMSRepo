<template>
  <div class="liquidation">
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span>麻將臺</span>
      </div>
      <div class="card-body" v-if="tables">
         <el-table
           class="mahjong-tables"
          :data="tables">
          <el-table-column type="expand">
            <template slot-scope="scope">              
               <el-table
                  class="seat-tables"
                  :data="scope.row.seats">                  
                  <el-table-column
                    prop="position"
                    label="位置"
                    width="80">
                  </el-table-column>
                  <el-table-column
                    prop="playerCardId"
                    label="客戶卡"
                    width="100">
                  </el-table-column>
                  <el-table-column
                    prop="staffCardId"
                    label="員工卡"
                    width="180">
                  </el-table-column>
                  <el-table-column
                    prop="playerType"
                    label="玩家類型"
                    width="180">                    
                  </el-table-column>
                  <el-table-column                      
                      label="未收水錢">
                      <template  slot-scope="scope">
                        <span v-if="scope.row.playerCard">
                          {{scope.row.playerCard.commission}}
                        </span>
                      </template>
                  </el-table-column>
                </el-table>
            </template>
          </el-table-column>
          <el-table-column
            prop="id"
            label="臺號"
            width="80">
          </el-table-column>
          <el-table-column
            prop="name"
            label="臺名"
            width="100">
          </el-table-column>
          <el-table-column
            prop="description"
            label="描述"
            width="180">
          </el-table-column>
          <el-table-column
            label="金額"
            width="180">
             <template slot-scope="scope">
                {{scope.row.minAmount}} - {{scope.row.maxAmount}}
              </template>
          </el-table-column>
          <el-table-column
              prop="unpaidCommissionTotal"
              label="未收水錢">
            </el-table-column>
        </el-table>
      </div>
    </el-card>
  </div>
</template>


<script>
export default {
  name: "tables",
  data() {
    return {
      tables:[]
    };
  },
  methods: {
    
  },
  mounted() {
    this.axios
      .get("http://mjms-core.azurewebsites.net/api/services/app/Table/GetAllWithSeats")
      .then(response => {
        console.log(response.data);
        this.tables = response.data.result.items;
    });
  },
  computed: {
    
  }
};
</script>

<style lang="scss">
  .mahjong-tables table{
    width:100%!important;
    .el-table__expanded-cell:hover,
    .el-table__expanded-cell{
      background:#f2f2f2!important;
      padding-top:10px;
      padding-bottom:10px;
    }
  }
  .seat-tables{
    th, td{
        padding:5px 0;
        background:#f2f2f2;
    }
  }
</style>